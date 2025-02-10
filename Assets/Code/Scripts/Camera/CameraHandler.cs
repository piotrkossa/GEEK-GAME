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

        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfMonitorClicked);
        InputManager.Instance.OnEsc.AddListener(Escape);
    }

    private void CheckIfMonitorClicked(Transform objTransform) {
        if (objTransform == null) return;

        Debug.Log(objTransform.name);

        if (objTransform.tag == "Level") {
            int number = int.Parse(objTransform.name[^1].ToString());
            _cameraTransition.SetCam(number);
        }
        else if (objTransform.parent.tag == "Level") {
            int number = int.Parse(objTransform.parent.name[^1].ToString());
            _cameraTransition.SetCam(number);
        }
    }

    private void Escape() {
        _cameraTransition.SetCam(0);
    }
}
