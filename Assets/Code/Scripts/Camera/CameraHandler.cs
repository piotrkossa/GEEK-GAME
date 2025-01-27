using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] GlobalVariables _globalVariables;

    private Vector3 mainCamPos;
    private float mainCamSize;

    [SerializeField] private Transform[] levelPositions = new Transform [3];
    private float monitorCamSize = 5.554f;

    private struct TransitionManager {
        public bool isTransitioning;
        public Vector3 destinyLocation;
        public float destinySize;
        public float speed;

        public TransitionManager(bool iT, Vector3 dL, float dS, float s) {
            isTransitioning = iT;
            destinyLocation = dL;
            destinySize = dS;
            speed = s;
        }
    }

    private TransitionManager transitionManager = new TransitionManager(false, new Vector3(), 0f, 1f);

    private InputAction Select;
    private InputAction Escape;

    private void Start() {
        Select = InputSystem.actions.FindAction("LeftClick");
        Escape = InputSystem.actions.FindAction("Escape");

        mainCamSize = this.GetComponent<Camera>().orthographicSize;
        mainCamPos = this.transform.position;
    }

    private void Update() {
        if (transitionManager.isTransitioning) {
            TransitionHandler();
            return;
        }
        if (EventSystem.current.IsPointerOverGameObject()) return;

        if (Select.IsPressed() && _globalVariables.CurrentLevelView == 0) {
            IsCamClicked();
        }
        else if (Escape.IsPressed() && _globalVariables.CurrentLevelView != 0) {
            _globalVariables.UpdateCurrentLevelView(0);
            transitionManager = new TransitionManager(true, mainCamPos, mainCamSize, 4);
        }
    }

    private void IsCamClicked() {
        RaycastHit2D hit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (!hit.collider || !hit.transform.CompareTag("Level")) return;
        int camNumber = 1;

        _globalVariables.UpdateCurrentLevelView(camNumber);
        ChangeCam(camNumber);
    }

    private void ChangeCam(int cam) {
        Vector3 lvlPos = levelPositions[cam-1].position;
        lvlPos.z = -1;
        transitionManager = new TransitionManager(true, lvlPos, monitorCamSize, 4);
    }

    private void TransitionHandler() {
        if (Mathf.Abs(Camera.main.orthographicSize - transitionManager.destinySize) < 0.01f) {
            transitionManager.isTransitioning = false;
            return;
        }

        Camera.main.transform.position = Vector3.Lerp(Camera.main.transform.position, transitionManager.destinyLocation, Time.deltaTime * transitionManager.speed);
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, transitionManager.destinySize, Time.deltaTime * transitionManager.speed);
    }
}
