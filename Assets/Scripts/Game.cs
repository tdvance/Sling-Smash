using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour {
    public GameObject enemiesParent;
    public float initialScorePerRemainingRock = 5000;
    public float multiplierPerRemainingRock = 2;
    public int currentEnemyCount = 0;

    int whichRock;
    float scorePerRock;
    NumberSprite numberSprite;
    Sling sling;
    ScoreDisplay scoreDisplay;
    AvailableRocks availableRocks;

    // Use this for initialization
    void Start() {
        Time.timeScale = 0.25f;
        InvokeRepeating("CountEnemies", 0.01f, 1.13f);
    }

    // Update is called once per frame
    void Update() {

    }

    public void ResetGame() {
        float time = 0f;
        CountEnemies();
        sling = FindObjectOfType<Sling>();
        availableRocks = FindObjectOfType<AvailableRocks>();
        numberSprite = FindObjectOfType<NumberSprite>();
        scorePerRock = initialScorePerRemainingRock;
        whichRock = sling.WhichRock();
        scoreDisplay = FindObjectOfType<ScoreDisplay>();
        if (currentEnemyCount == 0 && whichRock < sling.rocks.Length) {
            sling.endLevelButton.interactable = false;
            for (int i = sling.WhichRock(); i < sling.rocks.Length; i++) {
                Invoke("ShowBonus", time);
                time += 1.5f;
                scorePerRock *= multiplierPerRemainingRock;
            }
        }
        Invoke("NextLevel", time);
    }

    void NextLevel() {
        SceneManager.LoadScene("Game");
    }
    void ShowBonus() {
        scoreDisplay.score += (int)scorePerRock;
        numberSprite.ShowNumber((int)scorePerRock, availableRocks.transform.position + Vector3.up*whichRock, Color.white);
        whichRock++;
    }

    public void CountEnemies() {
        int count = 0;
        foreach (Transform enemy in enemiesParent.transform) {
            count++;
        }
        currentEnemyCount = count;
    }
}
