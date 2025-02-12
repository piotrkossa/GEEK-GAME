using UnityEngine;

public class CamFocus : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private float camSize;
    [SerializeField] private int camNumber;
    [SerializeField] private float transTime;

    private Vector3 thisPosition;

    private CameraTransition _camera;


    private void Start() {
        _camera = Camera.main.GetComponent<CameraTransition>();
        thisPosition = this.transform.position;
        InputManager.Instance.OnGameObjectClicked.AddListener(CheckIfClicked);
    }

    private void CheckIfClicked(Transform objTransform) {
        if (objTransform == this.transform || objTransform.IsChildOf(this.transform)) {
            _camera.SetCam(camNumber, thisPosition, camSize, transTime);
        }
    }
}
