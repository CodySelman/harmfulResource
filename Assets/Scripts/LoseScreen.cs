using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseScreen : MonoBehaviour
{
    public GameObject loseScreen;

    void Start() {
        loseScreen.SetActive(false);
    }

    public void ShowLoseScreen(bool shouldShow) {
        if (shouldShow) {
            loseScreen.SetActive(true);
        } else {
            loseScreen.SetActive(false);
        }
    }
}
