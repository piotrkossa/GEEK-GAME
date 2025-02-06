using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEditor.Search;

public class TurretSpawner : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private List<Button> _slots;
    private List<Image> _icons = new List<Image>();

    [Header("Icons")]
    [SerializeField] private List<Sprite> _sprites;

    [Header("Turrets")]
    [SerializeField] private List<GameObject> _turrets;

    [Header("Turrets grid")]
    [SerializeField] private LayerMask _placementBlockMask;
    private Vector2 boxSize = new Vector2(1.15f, 1.15f);
    [SerializeField] private SpriteRenderer _placementIndicator;

    private InputAction Accept;
    private InputAction Cancel;

    private bool isPlacing = false;
    private GameObject objectBeingPlaced = null;

    private void Start()
    {
        Accept = InputSystem.actions.FindAction("Accept");
        Cancel = InputSystem.actions.FindAction("Cancel");

        foreach (Button slot in _slots)
        {
            _icons.Add(slot.transform.GetChild(0).GetComponent<Image>());
        }
        _icons[0].sprite = _sprites[0];
    }

    private void Update()
    {
        if (isPlacing)
        {
            Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mouseWorldPos.z = 0;

            if (objectBeingPlaced != null)
                objectBeingPlaced.transform.position = Input.mousePosition;

            if (_placementIndicator != null)
            {
                _placementIndicator.gameObject.SetActive(true);
                _placementIndicator.transform.position = mouseWorldPos;

                Collider2D hit = Physics2D.OverlapBox(mouseWorldPos, boxSize, 0, _placementBlockMask);

                _placementIndicator.color = (hit != null) ? Color.red : Color.green;

            }

            if (Accept.WasPressedThisFrame())
            {
                Collider2D hit = Physics2D.OverlapBox(mouseWorldPos, boxSize, 0, _placementBlockMask);
                if (hit == null)
                {
                    isPlacing = false;
                    Destroy(objectBeingPlaced);
                    if (_placementIndicator != null)
                        _placementIndicator.gameObject.SetActive(false);

                    Instantiate(_turrets[0]).transform.position = mouseWorldPos;
                }
                else
                {
                    Debug.Log("Nie mo¿esz postawiæ dzia³ki w tym miejscu!");
                    isPlacing = false;
                    Destroy(objectBeingPlaced);
                    if (_placementIndicator != null)
                        _placementIndicator.gameObject.SetActive(false);
                }
            }
            else if (Cancel.WasPressedThisFrame())
            {
                isPlacing = false;
                Destroy(objectBeingPlaced);
                if (_placementIndicator != null)
                    _placementIndicator.gameObject.SetActive(false);
            }
        }
    }

    public void TurretChoosen(int slot)
    {
        objectBeingPlaced = Instantiate(_icons[slot].gameObject, _icons[slot].transform.parent);
        objectBeingPlaced.GetComponent<Image>().sprite = _icons[slot].sprite;
        isPlacing = true;
    }
}