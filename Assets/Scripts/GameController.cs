using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // singleton instance
    public static GameController instance = null;

    private PlayerCardManager playerCardManager;

    public List<Card> testDeck;

    void Awake() {
        // singleton setup
        if (instance == null) {
            Object.DontDestroyOnLoad(this.gameObject);
            instance = this;
        } else if (instance != this) {
            Destroy(gameObject);
        }
    }

    void Start() {
        playerCardManager = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER_CARD_MANAGER).GetComponent<PlayerCardManager>();
        playerCardManager.deck = CardUtility.Shuffle(testDeck);
    }
}
