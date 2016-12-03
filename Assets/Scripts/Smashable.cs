using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour {

    public float health = 100;
    float scoreFromDamage = 1f;
    float scoreFromDamageExponent = 1.5f;
    int scoreFromDeath = 100;
    public ScoreDisplay scoreDisplay;


    // Use this for initialization
    void Start() {
        if (!scoreDisplay) {
            scoreDisplay = FindObjectOfType<ScoreDisplay>();
        }
    }

    // Update is called once per frame
    void Update() {

    }

    private void OnCollisionEnter2D(Collision2D collision) {
        GameObject obj = collision.gameObject;
        DoesDamage dd = obj.GetComponent<DoesDamage>();
        if (dd) {
            float velocity = (obj.GetComponent<Rigidbody2D>().velocity
                - this.GetComponent<Rigidbody2D>().velocity).magnitude;
            float damage = Mathf.Round(dd.damage * Mathf.Pow(velocity, dd.velocityExponent));
            if (damage > 0) {
                if (health <= damage) {
                    damage = health;
                }
                health -= damage;
                scoreDisplay.score += (int)Mathf.Round(scoreFromDamage * Mathf.Pow(damage, scoreFromDamageExponent));

                if (health <= 0) {
                    Die();
                }
                Debug.Log(gameObject + "recieves Damage: " + damage + " from " + obj);
            }
        }

    }

    public void Die() {
        scoreDisplay.score += scoreFromDeath;
        Destroy(gameObject);
    }
}
