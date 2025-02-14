using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

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


    private bool isPlacing = false;
    private GameObject objectBeingPlaced = null;

    private void Start()
    {
        InputManager.Instance.OnLeftMouse.AddListener(Accept);
        InputManager.Instance.OnRightMouse.AddListener(Escape);
        InputManager.Instance.OnEsc.AddListener(Escape);

        foreach (Button slot in _slots)
        {
            _icons.Add(slot.transform.GetChild(0).GetComponent<Image>());
        }
        _icons[0].sprite = _sprites[0];
    }

    private Vector3 mouseWorldPos;

    private void Update()
    {
        if (!isPlacing) return;

        if (InputManager.Instance.isInputBlocked) {
            Escape();
            return;
        }

        mouseWorldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouseWorldPos.z = 0;

        objectBeingPlaced.transform.position = Input.mousePosition;

        _placementIndicator.transform.position = mouseWorldPos;
        Collider2D hit = Physics2D.OverlapBox(mouseWorldPos, boxSize, 0, _placementBlockMask);
        _placementIndicator.color = (hit != null) ? Color.red : Color.green;
    }

    private void Accept() {
        if (!isPlacing) return;

        Collider2D hit = Physics2D.OverlapBox(mouseWorldPos, boxSize, 0, _placementBlockMask);
        if (hit == null)
        {
            isPlacing = false;
            Destroy(objectBeingPlaced);
            
            _placementIndicator.gameObject.SetActive(false);

            Instantiate(_turrets[0]).transform.position = mouseWorldPos;
        }
        else
        {
            Debug.Log("Nie mozesz postawic dzialka w tym miejscu!");
            isPlacing = false;
            Destroy(objectBeingPlaced);

            _placementIndicator.gameObject.SetActive(false);
        }
    }

    private void Escape() {
        if (!isPlacing) return;

        Destroy(objectBeingPlaced);

        _placementIndicator.gameObject.SetActive(false);
        isPlacing = false;
    }

    public void TurretChoosen(int slot)
    {
        isPlacing = true;
        objectBeingPlaced = Instantiate(_icons[slot].gameObject, _icons[slot].transform.parent);
        objectBeingPlaced.GetComponent<Image>().sprite = _icons[slot].sprite;

        _placementIndicator.gameObject.SetActive(true);
    }
}