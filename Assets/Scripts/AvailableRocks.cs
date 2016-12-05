using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AvailableRocks : MonoBehaviour {
    int whichRock;
    Sling sling;
    // Use this for initialization
    void Start() {
        sling = FindObjectOfType<Sling>();
        whichRock = sling.WhichRock();
        ShowRocks();
    }

    // Update is called once per frame
    void Update() {
        if (sling.WhichRock() != whichRock) {
            whichRock = sling.WhichRock();
            ShowRocks();
        }
    }

    void ShowRocks() {
        foreach (Transform oldRock in transform) {
            Destroy(oldRock.gameObject);
        }
        float offset = 0;
        for (int i = whichRock; i < sling.rocks.Length; i++) {
            offset += sling.rocks[i].GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
        }
        for (int i = whichRock; i < sling.rocks.Length; i++) {
            GameObject rock = Instantiate(sling.rocks[i], transform) as GameObject;
            rock.transform.position = transform.position + offset * Vector3.left;
            offset -= sling.rocks[i].GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2;
            rock.GetComponent<Rigidbody2D>().simulated = false;
            rock.GetComponent<Collider2D>().enabled = false;
        }
    }
}