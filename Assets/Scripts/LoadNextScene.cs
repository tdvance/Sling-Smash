using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadNextScene : MonoBehaviour {

    public string sceneToLoad = "Game";
    public float delay = 2f;

	// Use this for initialization
	void Start () {
        Invoke("LoadScene", delay);
	}

    void LoadScene() {
        SceneManager.LoadScene(sceneToLoad);
    }
	
}
