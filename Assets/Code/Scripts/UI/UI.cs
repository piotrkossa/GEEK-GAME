using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    [SerializeField] GlobalVariables _globalVariables;
    [SerializeField] private TransitionManager _transitionManager;

    [SerializeField] private GameObject _turretBar;


    private void Start() {
        _globalVariables.onViewChanged.AddListener(ManageTurretBar);
        _turretBar.SetActive(false);
    }

    private void ManageTurretBar() {
        int cl = _globalVariables.CurrentLevelView;

        if (cl == 1 || cl == 2 || cl == 3) {
            _turretBar.SetActive(true);
        }
        else {
            _turretBar.SetActive(false);
        }
    }
}
