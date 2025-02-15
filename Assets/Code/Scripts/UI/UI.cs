using UnityEngine;
using UnityEngine.EventSystems;

public class UI : MonoBehaviour
{
    [SerializeField] GlobalVariables _globalVariables;
    [SerializeField] private TransitionManager _transitionManager;

    private CanvasGroup _thisCanvasGroup;


    private void Start() {
        _thisCanvasGroup = this.GetComponent<CanvasGroup>();
    }

    public GlobalVariables GetGlobalVariables() {
        return _globalVariables;
    }
}
