using System.Collections.Generic;
using UnityEngine;


public class TurretSpawner : MonoBehaviour
{
    [SerializeField] private GlobalVariables _globalVariables;
    AudioManager audioManager;

    private Dictionary<TurretTypes, GameObject> _turretProps = new Dictionary<TurretTypes, GameObject> {};
    [Header("TurretProps")]
    [SerializeField] private GameObject blueProp;
    [SerializeField] private GameObject redProp;
    [SerializeField] private GameObject yellowProp;
    [SerializeField] private GameObject greenProp;

    private Dictionary<TurretTypes, GameObject> _turrets = new Dictionary<TurretTypes, GameObject> {};
    [Header("Turrets Prefabs")]
    [SerializeField] private GameObject blueTurret;
    [SerializeField] private GameObject redTurret;
    [SerializeField] private GameObject yellowTurret;
    [SerializeField] private GameObject greenTurret;

    [Header("Turrets grid")]
    [SerializeField] private LayerMask _placementBlockMask;
    [SerializeField] private SpriteRenderer _placementIndicator;
    private Vector2 boxSize = new Vector2(1.15f, 1.15f);

    [Header("Levels")]
    [SerializeField] private Transform[] _levels = new Transform [3];


    private bool isPlacing = false;
    private GameObject objectBeingPlaced = null;
    private TurretTypes objectBeingPlacedType = TurretTypes.None;

    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }

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
        if (hit == null && _globalVariables.CanPlaceTurret(_globalVariables.CurrentLevelView))
        {
            isPlacing = false;
            Destroy(objectBeingPlaced);
            
            _placementIndicator.gameObject.SetActive(false);

            audioManager.PlaySFX(audioManager.placing);

            Instantiate(_turrets[objectBeingPlacedType], _levels[_globalVariables.CurrentLevelView-1]).transform.position = mouseWorldPos;
            _globalVariables.UpdateTurrets(objectBeingPlacedType, -1);
            _globalVariables.UpdateMonitorTurretCount();
            
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