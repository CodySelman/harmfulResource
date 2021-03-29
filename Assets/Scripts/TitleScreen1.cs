using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleScreen1 : MonoBehaviour
{
    public void StartGame() {
        SceneManager.LoadScene(Constants.SCENE_GAME);
    }

    public void ExitGame() {
        Application.Quit();
    }
}
