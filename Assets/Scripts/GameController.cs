using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // singleton instance
    public static GameController instance = null;

    private PlayerCardManager playerCM;
    private EventCardManager eventCM;
    private SupplyCardManager supplyCM;

    [SerializeField]
    private int startingMoney = 100;
    [SerializeField]
    private int startingHealth = 100;
    [SerializeField]
    private float startingWage = 7.5f;

    public List<Card> testDeck;
    public List<Card> testSupply;
    public List<Card> testEvents;

    public TMP_Text moneyText;
    public TMP_Text healthText;
    public TMP_Text wageText;

    // player stats
    private int money = 0;
    public int Money {
        get {
            return this.money;
        }
        set {
            this.money = value;
            moneyText.text = "Money: $" + this.money;
        }
    }
    private int mentalHealth = 0;
    public int MentalHealth {
        get {
            return this.mentalHealth;
        }
        set {
            // if it hits 0, game over
            this.mentalHealth = value;
            healthText.text = "Health: " + this.mentalHealth;
        }
    }
    private float hourlyWage = 7.5f;
    public float HourlyWage {
        get {
            return this.hourlyWage;
        }
        set {
            this.hourlyWage = value;
            wageText.text = "Hourly Wage: $" + this.hourlyWage;
        }
    }

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
        eventCM.deck = CardUtility.Shuffle(testEvents);

        supplyCM = GameObject.FindGameObjectWithTag(Constants.TAG_SUPPLY_CARD_MANAGER).GetComponent<SupplyCardManager>();
        // TODO initialize with real deck
        supplyCM.deck = CardUtility.Shuffle(testSupply);

        Money = startingMoney;
        MentalHealth = startingHealth;
        HourlyWage = startingWage;
    }

    void ExitGame() {
        Application.Quit();
    }
}
