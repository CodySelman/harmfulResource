using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpPanel : MonoBehaviour
{
    [SerializeField]
    private GameObject helpPanel;

    void Start() {
        helpPanel.SetActive(false);
    }

    void OnMouseEnter() {
        helpPanel.SetActive(true);
    }

    void OnMouseExit() {
        helpPanel.SetActive(false);
    }
}
