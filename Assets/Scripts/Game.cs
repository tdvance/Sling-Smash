using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Time.timeScale = 0.5f;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetGame() {
        SceneManager.LoadScene("Game");
    }
}
