using UnityEngine;
using System.Threading.Tasks;


public class CameraTransition : MonoBehaviour
{
    [SerializeField] private TransitionManager _transitionManager;
    [SerializeField] private GlobalVariables _globalVariables;

    private Camera thisCam;

    private bool isTransitioning = false;


    private void Awake() {
        thisCam = this.GetComponent<Camera>();
    }


    private async Task ChangeCam(Vector3 position, float size, float time) {  
        Task[] tasks = new Task [2];
        tasks[0] = _transitionManager.CameraSizeTransition(thisCam, thisCam.orthographicSize, size, time, 0.4f, 1f);
        tasks[1] = _transitionManager.VectorTransition(this.transform, this.transform.position, position, time, 0.4f, 1f);
        await Task.WhenAll(tasks);
    }

    public async void SetCam(int cam, Vector3 position, float size, float time) {
        if (isTransitioning)
            return;

        if (_globalVariables.CurrentLevelView != cam) {
            isTransitioning = true;
            InputManager.Instance.BlockAllInput();

            position.z = -1;
            await ChangeCam(position, size, time);
            _globalVariables.UpdateCurrentLevelView(cam);

            InputManager.Instance.UnlockAllInput();
            isTransitioning = false;
        }
    }
}
