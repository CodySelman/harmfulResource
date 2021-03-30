using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerCardManager : MonoBehaviour
{
    // singleton instance
    public static PlayerCardManager instance = null;

    [SerializeField]
    private GameObject playerCard;
    public TMP_Text deckNumCards;
    public TMP_Text discardNumCards;

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();

    private int startingHandSize = 5;
    [HideInInspector]
    public CardManager cardManager;

    [SerializeField]
    private GameObject handCardParent;
    [SerializeField]
    private float handYPos = -3;
    [SerializeField]
    private int handMinXPos = -5;
    [SerializeField]
    private int handMaxXPos = 5;
    [SerializeField]
    private int cardWidth = 2;
    [SerializeField]
    private float handCardMargin = 0.25f;

    public GameObject cardBackObj;
    public GameObject deckObj;
    public GameObject discardObj;

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
        // GameController handles initialization
    }

    public void Initialize() {
        deck = CardUtility.Shuffle(GameController.instance.testDeck);
        discardPile = new List<Card>();
        hand = new List<Card>();
        cardManager = new CardManager(deck, discardPile, hand, startingHandSize);
        UpdateAllDisplay();
    }

    void UpdateDeckNumCardsText() {
        deckNumCards.text = deck.Count.ToString();
    }

    void UpdateDiscardNumCardsText() {
        discardNumCards.text = discardPile.Count.ToString();
    }

    void UpdateDeckAndDiscardNumCardsText() {
        deckNumCards.text = deck.Count.ToString();
        discardNumCards.text = discardPile.Count.ToString();
    }

    // TODO consolidate with SupplyCardManager method?
    void UpdateHandDisplay() {
        // remove old hand render
        foreach (Transform handCard in handCardParent.transform) {
            GameObject.Destroy(handCard.gameObject);
        }

        // render cards in hand
        for (int i = 0; i < hand.Count; i++) {
            // instantiate card with correct card object
            GameObject handCard = Object.Instantiate(playerCard);
            handCard.GetComponent<CardBase>().card = hand[i];
            handCard.transform.parent = handCardParent.transform;

            if (hand.Count <= 5) {
                // set card positions
                float xOffset = 0;
                float xCardAndMarginWidth = cardWidth + handCardMargin;
                // even number of cards in hand
                if (hand.Count % 2 == 0) {
                    xOffset = (hand.Count - 1) * xCardAndMarginWidth / 2;
                } else { // odd number of cards in hand
                    xOffset = Mathf.FloorToInt(hand.Count / 2) * xCardAndMarginWidth;
                }
                float xPos = (xCardAndMarginWidth * i) - xOffset;
                handCard.transform.position = new Vector3(xPos, handYPos, -i);
            } else {
                // first card at minX, last at maxX, others distributed evenly between
                float xOffset = (float)(handMaxXPos - handMinXPos) / (hand.Count - 1);
                float xPos = handMinXPos + (i * xOffset);
                handCard.transform.position = new Vector3(xPos, handYPos, -i);
            }
            
        }
    }

    public void UpdateAllDisplay() {
        UpdateDeckNumCardsText();
        UpdateDiscardNumCardsText();
        UpdateHandDisplay();
    }

    public void PlayCard(Card card) {
        AudioManager.instance.OnPlayCard(card.isGoodCard);
        IEnumerator coroutine = ResolveCardEffects(card);
        Debug.Log("coroutine started");
        StartCoroutine(coroutine);
        Debug.Log("past coroutined");
        // UpdateAllDisplay();
    }

    IEnumerator ResolveCardEffects(Card card) {
        foreach (CardEffect cardEffect in card.effects) {
            switch (cardEffect.effect) {
                case CardEffects.DrawCard:
                    cardManager.DrawCards(cardEffect.amount);
                    yield return StartCoroutine("DrawCardsAnim");
                    UpdateAllDisplay();
                    break;
                case CardEffects.DiscardCard:
                    if (hand.Count > 1) {
                        AudioManager.instance.OnDiscard();
                        cardManager.DiscardCards(cardEffect.amount);
                        yield return StartCoroutine("DiscardCardsAnim");
                        UpdateAllDisplay();
                    }
                    break;
                case CardEffects.GainMoney:
                    GameController.instance.Money += cardEffect.amount;
                    yield return null;
                    break;
                case CardEffects.LoseMoney:
                    GameController.instance.Money -= cardEffect.amount;
                    yield return null;
                    break;
                case CardEffects.GainHealth:
                    GameController.instance.MentalHealth += cardEffect.amount;
                    yield return null;
                    break;
                case CardEffects.LoseHealth:
                    GameController.instance.MentalHealth -= cardEffect.amount;
                    yield return null;
                    break;
                case CardEffects.GainMoneyByWageHours:
                    GameController.instance.Money += cardEffect.amount * GameController.instance.HourlyWage;
                    yield return null;
                    break;
                case CardEffects.LoseMoneyByWageHours:
                    GameController.instance.Money -= cardEffect.amount * GameController.instance.HourlyWage;
                    yield return null;
                    break;
                case CardEffects.GainWage:
                    GameController.instance.HourlyWage += cardEffect.amount;
                    yield return null;
                    break;
                case CardEffects.LoseWage:
                    GameController.instance.HourlyWage -= cardEffect.amount;
                    yield return null;
                    break;
                default:
                    yield return null;
                    break;
            }
        }
        cardManager.DiscardCard(card);
        UpdateAllDisplay();
        Debug.Log("end of resolve");
        yield return null;
    }

    IEnumerator DrawCardsAnim() {
        GameObject card = GameObject.Instantiate(cardBackObj, deckObj.transform);
        bool arrived = false;
        Vector3 targetPos = new Vector3(-4, card.transform.position.y, card.transform.position.z);
        while (!arrived) {
            card.transform.position = Vector3.MoveTowards(card.transform.position, targetPos, 7 * Time.deltaTime);
            if (Vector3.Distance(card.transform.position, targetPos) <= 0.1f) {
                arrived = true;
                GameObject.Destroy(card);
            }
            yield return null;
        }
    }

    IEnumerator DiscardCardsAnim() {
        discardObj.transform.localScale = new Vector3(1.5f, 1.5f, 1);
        bool arrived = false;
        while (!arrived) {
            discardObj.transform.localScale = Vector3.MoveTowards(discardObj.transform.localScale, Vector3.one, 1.5f * Time.deltaTime);
            if (Vector3.Distance(discardObj.transform.localScale, Vector3.one) <= 0.1f) {
                arrived = true;
                discardObj.transform.localScale = Vector3.one;
            }
            yield return null;
        }
    }

    // private void ResolveCardEffects(Card card) {
    //     foreach (CardEffect cardEffect in card.effects) {
    //         switch (cardEffect.effect) {
    //             case CardEffects.DrawCard:
    //                 cardManager.DrawCards(cardEffect.amount);
    //                 break;
    //             case CardEffects.DiscardCard:
    //                 cardManager.DiscardCards(cardEffect.amount);
    //                 AudioManager.instance.OnDiscard();
    //                 break;
    //             case CardEffects.GainMoney:
    //                 GameController.instance.Money += cardEffect.amount;
    //                 break;
    //             case CardEffects.LoseMoney:
    //                 GameController.instance.Money -= cardEffect.amount;
    //                 break;
    //             case CardEffects.GainHealth:
    //                 GameController.instance.MentalHealth += cardEffect.amount;
    //                 break;
    //             case CardEffects.LoseHealth:
    //                 GameController.instance.MentalHealth -= cardEffect.amount;
    //                 break;
    //             case CardEffects.GainMoneyByWageHours:
    //                 GameController.instance.Money += cardEffect.amount * GameController.instance.HourlyWage;
    //                 break;
    //             default:
    //                 break;
    //         }
    //     }
    // }

    public void DiscardHand() {
        cardManager.DiscardHand();
        UpdateAllDisplay();
    }

    public void DrawHand() {
        cardManager.DiscardHand();
        cardManager.DrawHand();
        UpdateAllDisplay();
    }
}
