using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    private int count = 0;

    public Text scoreText;
    public GameObject explosion;

    void Start () {
        scoreText.enabled = false;
    }

    void Update ()
    {
        switch (Boule.step) {
            case Boule.GameStep.Done:
                scoreText.enabled = true;
                if (count == 0) {
                    scoreText.text = "STRIKE !!";
                } else {
                    scoreText.text = string.Format("Score : {0}/10", 10 - count);
                }
                break;
        }
    }
    void OnTriggerEnter () {
        count++;
    }

    void OnTriggerExit () {
        count--;
    }
}
