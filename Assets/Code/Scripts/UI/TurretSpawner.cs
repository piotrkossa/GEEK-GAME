using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class TurretSpawner : MonoBehaviour
{
    [Header("Slots")]
    [SerializeField] private List<Button> _slots;
    private List<Image> _icons = new List<Image> {};

    [Header("Icons")]
    [SerializeField] private List<Sprite> _sprites;

    [Header("Turrets")]
    [SerializeField] private List<GameObject> _turrets;


    private InputAction Accept;
    private InputAction Cancel;

    private bool isPlacing = false;
    private GameObject objectBeingPlaced = null;

    private void Start() {
        Accept = InputSystem.actions.FindAction("Accept");
        Cancel = InputSystem.actions.FindAction("Cancel");

        foreach (Button slot in _slots) {
            _icons.Add(slot.transform.GetChild(0).GetComponent<Image>());
        }
        _icons[0].GetComponent<Image>().sprite = _sprites[0];
    }

    private void Update() {
        if (isPlacing) {
            objectBeingPlaced.transform.position = Input.mousePosition;
            IsTurretPlaced();
        }
    }

    public void TurretChoosen(int slot) {
        objectBeingPlaced = Instantiate(_icons[slot].gameObject, _icons[slot].transform.parent);
        objectBeingPlaced.GetComponent<Image>().sprite = _icons[slot].sprite;
        isPlacing = true;
    }

    private void IsTurretPlaced() {
        if (Accept.IsPressed()) {
            isPlacing = false;
            Destroy(objectBeingPlaced);

            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousePosition.z = 0;

            Instantiate(_turrets[0]).transform.position = mousePosition;
        }
        else if (Cancel.IsPressed()) {
            isPlacing = false;
            Destroy(objectBeingPlaced);
        }
    }
}
