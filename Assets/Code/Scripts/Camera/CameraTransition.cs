using UnityEngine;
using System.Threading.Tasks;


public class CameraTransition : MonoBehaviour
{
    [SerializeField] private TransitionManager _transitionManager;
    [SerializeField] private GlobalVariables _globalVariables;

    private Camera thisCam;

    private float mainCamSize;
    private float monitorCamSize = 5.554f;

    [SerializeField] private Transform[] _levels = new Transform [4];
    private Vector3[] monitorLocations = new Vector3[4];

    private bool isTransitioning = false;


    private void Awake() {
        thisCam = this.GetComponent<Camera>();

        mainCamSize = thisCam.orthographicSize;

        for (int i = 0; i < 4; i++) {
            monitorLocations[i] = _levels[i].position;
            monitorLocations[i].z = -1;
        }
    }


    private async void ChangeCam(int cam) {
        isTransitioning = true;
        InputManager.Instance.BlockAllInput();
        
        float size;
        if (cam != 0) {
            size = monitorCamSize;
        }
        else {
            size = mainCamSize;
        }
        Task[] tasks = new Task [2];
        tasks[0] = _transitionManager.CameraSizeTransition(thisCam, thisCam.orthographicSize, size, 1f, 0.55f, 1f);
        tasks[1] = _transitionManager.VectorTransition(this.transform, this.transform.position, monitorLocations[cam], 1f, 0.55f, 1f);
        await Task.WhenAll(tasks);

        InputManager.Instance.UnlockAllInput();
        isTransitioning = false;
    }

    public void SetCam(int cam) {
        if (isTransitioning)
            return;

        if (_globalVariables.CurrentLevelView != cam) {
            ChangeCam(cam);
            _globalVariables.UpdateCurrentLevelView(cam);
        }
    }
}
