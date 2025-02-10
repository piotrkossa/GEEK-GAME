using UnityEngine;
using UnityEngine.UI;
using System.Threading.Tasks;
using System.Collections.Generic;

public class TransitionManager : MonoBehaviour
{
    public static TransitionManager Instance {get; private set;}

    private HashSet<GameObject> transObj = new HashSet<GameObject> {};

    private void Awake() {
        Instance = this;
    }

    public async Task VectorTransition(Transform objTransform, Vector3 originPosition, Vector3 destinyPosition, float time, float bezierY1, float bezierY2) {
        GameObject obj = objTransform.gameObject;

        transObj.Add(obj);

        float t = 0f;

        while (t <= 1f) {
            if (HasTransStopped(obj))
                break;
            t += Time.deltaTime / time;
            objTransform.position = Vector3.Lerp(originPosition, destinyPosition, QuadraticBezierCurve(t, bezierY1, bezierY2));
            await Task.Yield();
        }
    }

    public async Task CameraSizeTransition(Camera objCamera, float originFloat, float destinyFloat, float time, float bezierY1, float bezierY2) {
        GameObject obj = objCamera.gameObject;

        transObj.Add(obj);

        float t = 0f;

        while (t <= 1f) {
            if (HasTransStopped(obj))
                break;
            t += Time.deltaTime / time;
            objCamera.orthographicSize = Mathf.Lerp(originFloat, destinyFloat, QuadraticBezierCurve(t, bezierY1, bezierY2));
            await Task.Yield();
        }
    }

    public async Task CanvasGroupAlphaTransition(CanvasGroup objCanvasGroup, float originFloat, float destinyFloat, float time, float bezierY1, float bezierY2) {
        GameObject obj = objCanvasGroup.gameObject;

        transObj.Add(obj);

        float t = 0f;

        while (t <= 1f) {
            if (HasTransStopped(obj))
                break;
            t += Time.deltaTime / time;
            objCanvasGroup.alpha = Mathf.Lerp(originFloat, destinyFloat, QuadraticBezierCurve(t, bezierY1, bezierY2));
            await Task.Yield();
        }
    }

    public async Task SliderValueTransition(Slider objSlider, float originFloat, float destinyFloat, float time, float bezierY1, float bezierY2) {
        GameObject obj = objSlider.gameObject;

        transObj.Add(obj);

        float t = 0f;

        while (t <= 1f) {
            if (HasTransStopped(obj))
                break;
            t += Time.deltaTime / time;
            objSlider.value = Mathf.Lerp(originFloat, destinyFloat, QuadraticBezierCurve(t, bezierY1, bezierY2));
            await Task.Yield();
        }
    }

    private float QuadraticBezierCurve(float t, float y1, float y2) {
        float y = 3f * Mathf.Pow((1-t), 2) * t * y1 + 3 * (1-t) * Mathf.Pow(t, 2) * y2 + Mathf.Pow(t, 3);

        return y;
    }

    public void StopTransition(GameObject obj) {
        transObj.Remove(obj);
    }

    private bool HasTransStopped(GameObject obj) {
        if (transObj.Contains(obj))
            return false;
        return true;
    }
}
