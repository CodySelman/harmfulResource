using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCard : CardBase
{
    // Card card on CardBase
    private bool isHover = false;
    private Vector3 origPos;

    [SerializeField]
    private float hoverEnterSpeed = 5.0f;
    [SerializeField]
    private float hoverExitSpeed = 10.0f;
    [SerializeField]
    private float hoverYPos = -3;

    void Start() {
        origPos = transform.position;
    }

    void Update() {
        if (!GameController.instance.isPaused) {
            // animation would be better here but this is fine now
            if (isHover) {
                // lerp towards hover position and scale
                Vector3 newPos = new Vector3(transform.position.x, transform.position.y, -50);
                transform.position = newPos;
                transform.localScale = new Vector3(1.5f, 1.5f, 1);

            } else if (transform.position != origPos) {
                transform.position = origPos;
                transform.localScale = Vector3.one;
            }
        }
    }

    void OnMouseOver() {
        if (!GameController.instance.isPaused) {
            if (Input.GetMouseButtonDown(0)) {
                PlayerCardManager.instance.PlayCard(card);
            }

            // TODO card zoom
            // if (Input.GetMouseButtonDown(1)) {
            //     Debug.Log(nameText + " right clicked");
            // }
        }
    }

    void OnMouseEnter() {
        if (!GameController.instance.isPaused) {
            AudioManager.instance.OnCardHover();
            isHover = true;
        }
    }

    void OnMouseExit() {
        isHover = false;
    }
}
