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

    private WinScreen winScreen;
    private LoseScreen loseScreen;

    [SerializeField]
    private int startingMoney = 100;
    [SerializeField]
    private int startingHealth = 100;
    [SerializeField]
    private float startingWage = 7.5f;
    [SerializeField]
    private float winMoney = 200;

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
            CheckForWin();
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
            CheckForLoss();
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
        eventCM = GameObject.FindGameObjectWithTag(Constants.TAG_EVENT_CARD_MANAGER).GetComponent<EventCardManager>();
        supplyCM = GameObject.FindGameObjectWithTag(Constants.TAG_SUPPLY_CARD_MANAGER).GetComponent<SupplyCardManager>();
        
        winScreen = GetComponent<WinScreen>();
        loseScreen = GetComponent<LoseScreen>();

        InitializeGame();
    }

    void InitializeGame() {
        playerCM.Initialize();
        // // TODO initialize method for event deck
        eventCM.deck = CardUtility.Shuffle(testEvents);
        supplyCM.Initialize();

        Money = startingMoney;
        MentalHealth = startingHealth;
        HourlyWage = startingWage;

        winScreen.ShowWinScreen(false);
        loseScreen.ShowLoseScreen(false);
    }

    public void RestartGame() {
        InitializeGame();
    }

    public void ExitGame() {
        Application.Quit();
    }

    void CheckForWin() {
        if (Money >= winMoney) {
            Debug.Log("Game Over: You Win!");
            winScreen.ShowWinScreen(true);
        }
    }

    void CheckForLoss() {
        if (MentalHealth <= 0) {
            Debug.Log("Game Over: You Lose");
            loseScreen.ShowLoseScreen(true);
        }
    }
}
