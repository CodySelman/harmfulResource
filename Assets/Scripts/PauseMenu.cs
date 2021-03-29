using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject pauseMenu;

    void Start() {
        pauseMenu.SetActive(false);
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            ToggleShowPauseMenu();
        }
    }

    public void ToggleShowPauseMenu() {
        ShowPauseMenu(!pauseMenu.activeInHierarchy);
    }

    public void ShowPauseMenu(bool shouldShow) {
        AudioManager.instance.OnPause();
        AudioManager.instance.OnPause(shouldShow);
        pauseMenu.SetActive(shouldShow);
    }
}
