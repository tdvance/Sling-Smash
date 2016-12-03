using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;


public class DragableCallbackTest : MonoBehaviour {

    void StartDrag() {
        Debug.Log("Start Drag: " +
            FindObjectOfType<ScreenManager>().WorldToSprite(new ScreenManager.Position(
                Camera.main.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition)),
                transform.parent.gameObject));
    }

    void EndDrag() {
        Debug.Log("End Drag: " + transform.parent.gameObject
            + ", landed at " + transform.parent.position);
    }
}
