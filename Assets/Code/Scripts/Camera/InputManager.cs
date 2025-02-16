using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

    public bool isInputBlocked {get; private set;}
    private int blockCount = 0;

    public UnityEvent OnLeftMouse;
    public UnityEvent OnRightMouse;
    public UnityEvent OnEsc;
    public UnityEvent OnEscUnlocked;
    public UnityEvent<Transform> OnGameObjectClicked;

    private void Awake() {
        isInputBlocked = false;
        
        Instance = this;
    }

    private void OnLeftClick() {
        if (isInputBlocked) return;
        OnLeftMouse.Invoke();

        Transform clickedObj = GetClickedObject();
        if (clickedObj != null)
            OnGameObjectClicked.Invoke(clickedObj);
    }

    private void OnEscape() {
        if (!isInputBlocked)
            OnEsc.Invoke();
            
        OnEscUnlocked.Invoke();
    }

    private void OnRightClick() {
        if (isInputBlocked) return;
        OnRightMouse.Invoke();
    }


    public void BlockAllInput() {
        blockCount += 1;
        isInputBlocked = true;
    }

    public void UnlockAllInput() {
        if (blockCount > 0)
            blockCount -= 1;

        if (blockCount == 0)
            isInputBlocked = false;
    }

    public Transform GetClickedObject() {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (!rayHit.collider) return null;

        return rayHit.transform;
    }
}
