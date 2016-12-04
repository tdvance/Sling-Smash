using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sling : MonoBehaviour {
    public float forceMultiplier = 25;

    HingeJoint2D joint;
    Vector3 lastPos;

    // Use this for initialization
    void Start() {
        joint = GetComponent<HingeJoint2D>();
        lastPos = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (joint.connectedBody) {
            Vector3 delta = transform.position - lastPos;
            float amount = delta.magnitude / .01f;
            if (amount >= 1) {
                joint.connectedBody.AddTorque(-forceMultiplier * amount, ForceMode2D.Impulse);
            }
            lastPos = transform.position;
        }
    }

    public void DragStart() {
        //do nothing
    }

    public void Release() {
        GameObject rock = joint.connectedBody.gameObject;
        joint.connectedBody.gravityScale = 1f;
        joint.connectedBody.drag = 1f;
        joint.connectedBody = null;
        Time.timeScale = 1f;
    }
}
