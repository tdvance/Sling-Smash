using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Attach this script to a sprite having a Collider2D component to make it draggable.
/// </summary>
public class Dragable : MonoBehaviour {

    [Tooltip("Cross Platform Input Manager buttion for dragging the sprite")]
    public string dragButton = "Fire1";

    [Tooltip("Probably should keep this set to true")]
    public bool mouseMustBeOver = true;

    [Tooltip("Object used for callbacks; optional")]
    public MonoBehaviour userDragHandlerObject;
    public string callbackWhenDraggingStarts = "None";
    public string callbackWhenDraggingEnds = "None";

    [Tooltip("A script can set this to false if attempting to drop in an illegal location")]
    public bool allowDrop = true;

    bool isBeingDragged = false;
    bool mouseIsOver = false;
    Vector3 dragStart = Vector3.zero;
    Vector3 originalPosition = Vector3.zero;
    ScreenManager sm;


    // Use this for initialization
    void Start() {
        isBeingDragged = false;
        if (mouseMustBeOver && !GetComponent<Collider>() && !GetComponent<Collider2D>()) {
            Debug.LogError("Detection of mouseover requires a Collider or Collider2D component");
        }
        sm = FindObjectOfType<ScreenManager>();
    }

    // Update is called once per frame
    void Update() {
        if (isBeingDragged && !CrossPlatformInputManager.GetButton(dragButton)) {
            StopDragging();
        }
        if (!isBeingDragged && MouseIsOver() && CrossPlatformInputManager.GetButton(dragButton)) {
            StartDragging();
        }
        if (isBeingDragged) {
            Drag();
        }
    }

    void StopDragging() {
        isBeingDragged = false;
        if (userDragHandlerObject) {
            userDragHandlerObject.Invoke(callbackWhenDraggingEnds, 0f);
        }
        if (!allowDrop) {
            RevertPosition();
        }
    }

    void RevertPosition() {
        //TODO animate this
        transform.position = originalPosition;
    }

    void StartDragging() {
        dragStart = Camera.main.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition) - transform.position;
        originalPosition = transform.position;
        isBeingDragged = true;
        if (userDragHandlerObject) {
            userDragHandlerObject.Invoke(callbackWhenDraggingStarts, 0f);
        }
    }

    void Drag() {
        Vector3 v = Camera.main.ScreenToWorldPoint(
      sm.ClampToScreen(CrossPlatformInputManager.mousePosition)) - dragStart;
        if (v.x >= -30 && v.x <= -5.25 && v.y >= -6.75 && v.y <= 15) {
            transform.position = v;
        }
    }
    private void OnMouseEnter() {
        mouseIsOver = true;
    }

    private void OnMouseExit() {
        mouseIsOver = false;
    }

    bool MouseIsOver() {
        if (!mouseMustBeOver) {
            return true;
        }

        return mouseIsOver;
    }
}
