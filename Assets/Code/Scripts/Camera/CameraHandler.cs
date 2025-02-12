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
    [SerializeField] float escTimeTrans;

    private float thisSize;
    private Vector3 thisPosition;

    private void Start()
    {
        _cameraTransition = this.gameObject.GetComponent<CameraTransition>();

        InputManager.Instance.OnEsc.AddListener(Escape);

        thisPosition = this.transform.position;
        thisSize = Camera.main.orthographicSize;
    }

    private void Escape() {
        _cameraTransition.SetCam(0, thisPosition, thisSize, escTimeTrans);
    }
}
