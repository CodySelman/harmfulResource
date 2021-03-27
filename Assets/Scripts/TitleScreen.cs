using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleScreen : MonoBehaviour
{
    public GameObject titleScreen;

    void Start() {
        titleScreen.SetActive(true);
    }

    public void ShowTitleScreen(bool shouldShow) {
        if (shouldShow) {
            titleScreen.SetActive(true);
        } else {
            titleScreen.SetActive(false);
        }
    }
}
