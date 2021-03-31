using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameController : MonoBehaviour
{
    // singleton instance
    public static GameController instance = null;

    public bool isPaused = false;
    public bool isPlayingCard = false;

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
    private float startingWage = 5f;
    [SerializeField]
    private float winMoney = 1000;

    public List<Card> testDeck;
    public List<Card> testSupply;
    public List<Card> testEvents;

    public TMP_Text moneyText;
    public TMP_Text healthText;
    public TMP_Text wageText;
    public TMP_Text turnText;

    // player stats
    private float money = 0;
    public float Money {
        get {
            return this.money;
        }
        set {
            this.money = value;
            moneyText.text = this.money.ToString();
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
            healthText.text = this.mentalHealth.ToString() + "%";
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
            wageText.text = this.hourlyWage.ToString();
        }
    }
    private int turnCount = 1;
    public int TurnCount {
        get {
            return this.turnCount;
        }
        set {
            this.turnCount = value;
            turnText.text = "Turn: " + this.turnCount;
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
        // eventCM = GameObject.FindGameObjectWithTag(Constants.TAG_EVENT_CARD_MANAGER).GetComponent<EventCardManager>();
        supplyCM = GameObject.FindGameObjectWithTag(Constants.TAG_SUPPLY_CARD_MANAGER).GetComponent<SupplyCardManager>();
        
        winScreen = GetComponent<WinScreen>();
        loseScreen = GetComponent<LoseScreen>();

        InitializeGame();
    }

    public bool CheckForBusy() {
        return isPaused || isPlayingCard;
    }

    void InitializeGame() {
        turnCount = 0;

        playerCM.Initialize();
        // // TODO initialize method for event deck
        // eventCM.deck = CardUtility.Shuffle(testEvents);
        supplyCM.Initialize();

        Money = startingMoney;
        MentalHealth = startingHealth;
        HourlyWage = startingWage;

        winScreen.ShowWinScreen(false);
        loseScreen.ShowLoseScreen(false);

        StartTurn();
    }

    public void RestartGame() {
        InitializeGame();
    }

    public void ExitGame() {
        Application.Quit();
    }

    void CheckForWin() {
        if (Money >= winMoney) {
            AudioManager.instance.OnWinGame();
            winScreen.ShowWinScreen(true);
        }
    }

    void CheckForLoss() {
        if (MentalHealth <= 0 || Money < 0) {
            AudioManager.instance.OnLoseGame();
            loseScreen.ShowLoseScreen(true);
        }
        
    }

    public void StartTurn() {
        TurnCount += 1;
        MentalHealth -= (TurnCount * 3);
        HourlyWage += 1;
        playerCM.DrawHand();
        supplyCM.DrawSupply();
        // new event
    }

    public void EndTurn() {
        AudioManager.instance.OnClickButton();
        playerCM.DiscardHand();
        supplyCM.DiscardHand();
        StartTurn();
    }
}
