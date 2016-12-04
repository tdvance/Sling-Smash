using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NumberSprite : MonoBehaviour {
    public GameObject[] digits;

    public void ShowNumber(int number, Vector3 position, Color color, float time = 1f) {

        GameObject[] toSpawn = createDigits(number);
        float maxwidth = 0;
        foreach (GameObject obj in toSpawn) {
            maxwidth = Mathf.Max(obj.GetComponent<SpriteRenderer>().sprite.bounds.extents.x * 2f, maxwidth);
        }
        float offset = maxwidth * toSpawn.Length / 2;
        float scale = 1;
        if (number > 1000) {
            scale = 2;
        }else {
            if(number < 500) {
                scale = .75f;
                if(number < 250) {
                    scale = .5f;
                    if(number < 100) {
                        scale = .25f;
                    }
                }
            }
        }
        foreach (GameObject obj in toSpawn) {
            GameObject d = Instantiate(obj, position, Quaternion.identity);
            d.transform.localScale = new Vector3(scale, scale, 1);
            Color c = d.GetComponent<SpriteRenderer>().color;
            d.GetComponent<SpriteRenderer>().color = new Color(c.r, c.g, c.b, scale);
            d.transform.position = position - d.transform.right * offset;
            offset -= maxwidth;
            Destroy(d, time*2*scale);
        }

    }

    GameObject[] createDigits(int number) {
        if (number == 0) {
            GameObject[] zero = new GameObject[1];
            zero[0] = digits[0];
            return zero;
        }
        if (number < 0) {
            throw new UnityException("Negative numbers not supported");
        }
        int len = (int)Mathf.Log10(number) + 1;
        GameObject[] result = new GameObject[len];
        int n = number;
        for (int i = 0; i < len; i++) {
            result[len - i - 1] = digits[n % 10];
            n /= 10;
        }
        if (n != 0) {
            throw new UnityException("Something didn't work right in this algorithm; not supposed to get here");
        }
        return result;
    }

    // Use this for initialization
    void Start() {
    }

    // Update is called once per frame
    void Update() {

    }
}
