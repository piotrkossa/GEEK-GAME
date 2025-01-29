using System;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.U2D;

public class CameraTransition : MonoBehaviour
{
    private Camera thisCam;

    private float mainCamSize;
    private float monitorCamSize = 5.554f;

    [SerializeField] private Transform[] _levels = new Transform [4];
    private Vector3[] monitorLocations = new Vector3[4];

    private struct TransitionManager {
        public bool IsTransitioning;

        public Vector3 StartingLocation;
        public float StartingSize;

        public Vector3 DestinyLocation;
        public float DestinySize;

        public float Step;
        public float CompletedSteps;

        public TransitionManager(bool isTransitioning, Vector3 startingLocation, float startingSize, Vector3 destinyLocation, float destinySize, float step) {
            IsTransitioning = isTransitioning;
            StartingLocation = startingLocation;
            StartingSize = startingSize;
            DestinyLocation = destinyLocation;
            DestinySize = destinySize;
            Step = step;
            CompletedSteps = 0f;
        }
    }

    TransitionManager transitionManager = new TransitionManager(false, new Vector3(), 0f, new Vector3(), 0f, 0f);


    private void Awake() {
        thisCam = this.GetComponent<Camera>();

        mainCamSize = thisCam.orthographicSize;

        for (int i = 0; i < 4; i++) {
            monitorLocations[i] = _levels[i].position;
            monitorLocations[i].z = -1;
        }
    }

    private void LateUpdate() {
        if (transitionManager.IsTransitioning) {
            SmoothLerp();
        }
    }

    private void SmoothLerp() {
        float currentStep = transitionManager.Step * Time.deltaTime;
        transitionManager.CompletedSteps += currentStep;

        float bezierPoint = QuadraticBezierCurve(transitionManager.CompletedSteps);
        this.transform.position = Vector3.Lerp(transitionManager.StartingLocation, transitionManager.DestinyLocation, bezierPoint);
        thisCam.orthographicSize = Mathf.Lerp(transitionManager.StartingSize, transitionManager.DestinySize, bezierPoint);

        if (transitionManager.CompletedSteps >= 1f) {
            transitionManager.IsTransitioning = false;
        }
    }

    public void ChangeCam(int cam) {
        float size;
        if (cam != 0) {
            size = monitorCamSize;
        }
        else {
            size = mainCamSize;
        }

        transitionManager = new TransitionManager(true, this.transform.position, thisCam.orthographicSize, monitorLocations[cam], size, 1f);
    }

    private float QuadraticBezierCurve(float t) {
        float y1 = 0.55f;
        float y2 = 1f;

        float y = 3f * Mathf.Pow((1-t), 2) * t * y1 + 3 * (1-t) * Mathf.Pow(t, 2) * y2 + Mathf.Pow(t, 3);

        return y;
    }
}
