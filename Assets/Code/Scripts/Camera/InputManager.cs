using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance {get; private set;}

    private PlayerInput _input;

    public bool isInputBlocked {get; private set;}

    public UnityEvent OnLeftMouse;
    public UnityEvent OnRightMouse;
    public UnityEvent OnEsc;

    private void Awake() {
        isInputBlocked = false;
        
        Instance = this;

        _input = this.GetComponent<PlayerInput>();
    }

    private void OnLeftClick() {
        if (isInputBlocked) return;
        OnLeftMouse.Invoke();
    }

    private void OnEscape() {
        if (isInputBlocked) return;
        OnEsc.Invoke();
    }

    private void OnRightClick() {
        if (isInputBlocked) return;
        OnRightMouse.Invoke();
    }



    public void BlockAllInput() {
        isInputBlocked = true;
    }

    public void UnlockAllInput() {
        isInputBlocked = false;
    }

    public Transform GetClickedObject() {
        RaycastHit2D rayHit = Physics2D.GetRayIntersection(Camera.main.ScreenPointToRay(Input.mousePosition));
        if (!rayHit.collider) return null;

        return rayHit.transform;
    }
}
