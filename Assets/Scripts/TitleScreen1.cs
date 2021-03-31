using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen1 : MonoBehaviour
{
    private bool hasClicked = false;
    [SerializeField]
    private VideoFix videoFix;

    public void StartGame() {
        if (hasClicked) {
            SceneManager.LoadScene(Constants.SCENE_GAME);
        } else {
            videoFix.PlayVideo();
            hasClicked = true;
        }
    }

    public void ExitGame() {
        Application.Quit();
    }
}
