using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    public GameObject enemiesParent;

    public int currentEnemyCount = 0;


    // Use this for initialization
    void Start() {
        Time.timeScale = 0.5f;
        InvokeRepeating("CountEnemies", 0.01f, 1.13f);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ResetGame() {
        SceneManager.LoadScene("Game");
    }

    public void CountEnemies() {
        int count = 0;
        foreach (Transform enemy in enemiesParent.transform) {
            count++;
        }
        currentEnemyCount = count;
    }
}
