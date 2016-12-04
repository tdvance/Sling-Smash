using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smashable : MonoBehaviour {

    public float health = 100;
    public float scoreFromDamage = 1f;
    public float scoreFromDamageExponent = 1.5f;
    public int scoreFromDeath = 100;
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
                int s = (int)Mathf.Round(scoreFromDamage * Mathf.Pow(damage, scoreFromDamageExponent));
                scoreDisplay.score += s;
                if (s >= 10) {
                    FindObjectOfType<NumberSprite>().ShowNumber(s, transform.position - Vector3.forward, 0.5f);
                }

                if (health <= 0) {
                    Die();
                }
                Debug.Log(gameObject + "recieves Damage: " + damage + " from " + obj);
            }
        }

    }

    public void Die() {
        scoreDisplay.score += scoreFromDeath;
        FindObjectOfType<NumberSprite>().ShowNumber(scoreFromDeath, transform.position-Vector3.forward, 0.5f);
        Destroy(gameObject);
    }
}
