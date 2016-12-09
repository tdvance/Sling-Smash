using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sling : MonoBehaviour {
    public float forceMultiplier = 25;
    public Button endLevelButton;
    public GameObject[] rocks;
    public Vector3 relativeRockPosition = new Vector3(0, -4, 0);

    HingeJoint2D joint;
    Vector3 lastPos;
    int whichRock;
    GameObject currentRock;
    Game game;


    // Use this for initialization
    void Start() {
        game = FindObjectOfType<Game>();
        joint = GetComponent<HingeJoint2D>();
        lastPos = transform.position;
        endLevelButton.interactable = false;
        whichRock = 0;
        GetARock();
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
        if (game.currentEnemyCount == 0 && currentRock) {
            endLevelButton.interactable = true;
        }
    }

    public void DragStart() {
        Time.timeScale = 0.25f;
        if (currentRock) {
            Destroy(currentRock);
        }
        if (joint.connectedBody) {
            currentRock = joint.connectedBody.gameObject;
        }
    }

    public void Release() {
        GameObject rock = joint.connectedBody.gameObject;
        joint.connectedBody.gravityScale = 1f;
        joint.connectedBody.drag = 1f;
        joint.connectedBody = null;
        Time.timeScale = 1f;
        if (whichRock < rocks.Length) {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = 0;
            Invoke("GetARock", 1);
        } else {
            whichRock++;
            endLevelButton.interactable = true;
        }
    }

    public void GetARock() {
        GameObject rock = Instantiate(rocks[whichRock], transform.position + relativeRockPosition, Quaternion.identity);
        Rigidbody2D rb = rock.GetComponent<Rigidbody2D>();
        rb.gravityScale = 0f;
        joint.connectedBody = rb;
        whichRock++;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = 0;       
    }

    public int WhichRock() {
        return whichRock;
    }
}
