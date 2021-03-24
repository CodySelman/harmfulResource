using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    // singleton instance
    public static GameController instance = null;

    private PlayerCardManager playerCM;
    private EventCardManager eventCM;
    private SupplyCardManager supplyCM;

    // player stats
    public int money = 0;
    public int mentalHealth = 100;
    public float hourlyWage = 7.5f;

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
        playerCM = GameObject.FindGameObjectWithTag(Constants.TAG_PLAYER_CARD_MANAGER).GetComponent<PlayerCardManager>();
        // TODO initialize with real deck
        playerCM.deck = CardUtility.Shuffle(testDeck);

        eventCM = GameObject.FindGameObjectWithTag(Constants.TAG_EVENT_CARD_MANAGER).GetComponent<EventCardManager>();
        // TODO initialize with real deck
        eventCM.deck = CardUtility.Shuffle(testDeck);

        supplyCM = GameObject.FindGameObjectWithTag(Constants.TAG_SUPPLY_CARD_MANAGER).GetComponent<SupplyCardManager>();
        // TODO initialize with real deck
        supplyCM.deck = CardUtility.Shuffle(testDeck);
    }

    void ExitGame() {
        Application.Quit();
    }
}
