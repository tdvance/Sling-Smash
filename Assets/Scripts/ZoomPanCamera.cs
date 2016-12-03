using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

/// <summary>
/// Attach this script to an orthographic camera to make it zoomable and panable
/// </summary>
public class ZoomPanCamera : MonoBehaviour {
    [Tooltip("Leave blank to select camera this is attached to")]
    public Camera cam;

    [Tooltip("Zoom in this range relative to camera's initial orthographic size")]
    public float maxZoom = 10;

    [Tooltip("Zoom in this range relative to camera's initial orthographic size")]
    public float minZoom = .1f;

    [Tooltip("Cross Platform Input Manager axis for zooming")]
    public string zoomAxis = "Mouse ScrollWheel";

    [Tooltip("Cross Platform Input Manager button for dragging")]
    public string dragButton = "Fire3";

    float currentZoom = 1f;
    bool isBeingDragged = false;
    Vector3 dragStart = Vector3.zero;

    // Use this for initialization
    void Start() {
        isBeingDragged = false;

        if (!cam) {//try attached camera
            cam = GetComponent<Camera>();
        }

        if (!cam) {//try searching for a camera, prefering active
            ScreenManager sm = FindObjectOfType<ScreenManager>();
            if (sm) {
                cam = sm.activeCamera;
            }
            if (!cam) {
                sm.EnsureActiveCamera();
                cam = sm.activeCamera;
            }
        }
        if (!cam) {
            Debug.LogWarning("Camera not found!");
        }
    }

    // Update is called once per frame
    void Update() {

        //zoom
        if (CrossPlatformInputManager.GetAxis(zoomAxis) < 0 && currentZoom * 2 <= maxZoom) {
            currentZoom *= 2;
            cam.orthographicSize *= 2;
            Debug.Log("Zoom=" + currentZoom);
        } else if (CrossPlatformInputManager.GetAxis(zoomAxis) > 0 && currentZoom / 2 >= minZoom) {
            currentZoom /= 2;
            cam.orthographicSize /= 2;
            Debug.Log("Zoom=" + currentZoom);

        }

        //pan
        if (isBeingDragged && !CrossPlatformInputManager.GetButton(dragButton)) {
            StopDragging();
        }
        if (!isBeingDragged && CrossPlatformInputManager.GetButton(dragButton)) {
            StartDragging();
        }
        if (isBeingDragged) {
            Drag();
        }
    }

    void StopDragging() {
        isBeingDragged = false;
    }

    void StartDragging() {
        dragStart = cam.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition);
        isBeingDragged = true;
    }

    void Drag() {
        Vector3 delta = cam.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition) - dragStart;
        cam.transform.position -= delta;

        //recompute from new camera position
        dragStart = cam.ScreenToWorldPoint(CrossPlatformInputManager.mousePosition);
    }


}

