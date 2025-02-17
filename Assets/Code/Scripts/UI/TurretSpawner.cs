using System.Collections.Generic;
using UnityEngine;


public class TurretSpawner : MonoBehaviour
{
    private Dictionary<TurretTypes, GameObject> _turretProps = new Dictionary<TurretTypes, GameObject> {};
    [Header("TurretProps")]
    [SerializeField] private GameObject blueProp;
    [SerializeField] private GameObject redProp;
    [SerializeField] private GameObject yellowProp;
    [SerializeField] private GameObject greenProp;

    private Dictionary<TurretTypes, GameObject> _turrets = new Dictionary<TurretTypes, GameObject> {};
    [Header("Turrets")]
    [SerializeField] private GameObject blueTurret;
    [SerializeField] private GameObject redTurret;
    [SerializeField] private GameObject yellowTurret;
    [SerializeField] private GameObject greenTurret;

    [Header("Turrets grid")]
    [SerializeField] private LayerMask _placementBlockMask;
    private Vector2 boxSize = new Vector2(1.15f, 1.15f);
    [SerializeField] private SpriteRenderer _placementIndicator;


    private bool isPlacing = false;
    private GameObject objectBeingPlaced = null;
    private TurretTypes objectBeingPlacedType = TurretTypes.None;

    private void Start()
    {
        InputManager.Instance.OnLeftMouse.AddListener(Accept);
        InputManager.Instance.OnRightMouse.AddListener(Escape);
        InputManager.Instance.OnEsc.AddListener(Escape);
        
        AssignTurretsToDict();
        AssignTurretPropsToDict();
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

        objectBeingPlaced.transform.position = mouseWorldPos;

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

            Instantiate(_turrets[objectBeingPlacedType]).transform.position = mouseWorldPos;
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

    public void TurretChoosen(TurretTypes turretType)
    {
        isPlacing = true;
        objectBeingPlacedType = turretType;
        objectBeingPlaced = Instantiate(_turretProps[turretType], this.transform.parent);

        _placementIndicator.gameObject.SetActive(true);
    }

    private void AssignTurretPropsToDict() {
        _turretProps[TurretTypes.Blue] = blueProp;
        _turretProps[TurretTypes.Green] = greenProp;
        _turretProps[TurretTypes.Red] = redProp;
        _turretProps[TurretTypes.Yellow] = yellowProp;
    }

    private void AssignTurretsToDict() {
        _turrets[TurretTypes.Blue] = blueTurret;
        _turrets[TurretTypes.Green] = greenTurret;
        _turrets[TurretTypes.Red] = redTurret;
        _turrets[TurretTypes.Yellow] = yellowTurret;
    }
}