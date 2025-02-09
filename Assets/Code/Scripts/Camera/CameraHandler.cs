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

        InputManager.Instance.OnLeftMouse.AddListener(LeftClick);
        InputManager.Instance.OnEsc.AddListener(Escape);
    }

    private void LeftClick() {
        Transform clickedObject = InputManager.Instance.GetClickedObject();
        if (clickedObject == null) return;

        switch (clickedObject.tag) {
            case "Level":
                int number = int.Parse(clickedObject.name[^1].ToString());
                 _cameraTransition.SetCam(number);
                break;
        }
    }

    private void Escape() {
        _cameraTransition.SetCam(0);
    }
}
