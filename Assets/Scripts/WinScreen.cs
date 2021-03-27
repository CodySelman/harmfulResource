using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinScreen : MonoBehaviour
{
    public GameObject winScreen;

    void Start() {
        winScreen.SetActive(false);
    }

    public void ShowWinScreen(bool shouldShow) {
        if (shouldShow) {
            winScreen.SetActive(true);
        } else {
            winScreen.SetActive(false);
        }
    }
}
