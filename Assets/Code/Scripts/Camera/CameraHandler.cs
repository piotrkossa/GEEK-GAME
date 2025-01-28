using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraHandler : MonoBehaviour
{
    private Camera _mainCamera;
    [SerializeField] private GlobalVariables _globalVariables;
    private CameraTransition _cameraTransition;

    private void Start()
    {
        _mainCamera = Camera.main;
        _cameraTransition = this.gameObject.GetComponent<CameraTransition>();
    }

    public void LeftClick(InputAction.CallbackContext context) {
        if (!context.started) return;

        Transform clickedObject = GetClickedObject();
        if (clickedObject == null) return;

        switch (clickedObject.tag) {
            case "Level":
                int number = int.Parse(clickedObject.name[^1].ToString());
                SetCam(number);
                break;
        }
    }

    public void Escape(InputAction.CallbackContext context) {
        if (_globalVariables.CurrentLevelView != 0) {
            _cameraTransition.ChangeCam(0);
            _globalVariables.UpdateCurrentLevelView(0);
        }
    }

    private Transform GetClickedObject() {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(_mainCamera.ScreenPointToRay(Input.mousePosition));
        if (!rayHit.collider) return null;

        return rayHit.transform;
    }

    private void SetCam(int cam) {
        if (_globalVariables.CurrentLevelView == 0) {
            _cameraTransition.ChangeCam(cam);
            _globalVariables.UpdateCurrentLevelView(cam);
        }
    }
}
