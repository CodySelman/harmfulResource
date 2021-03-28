using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupplyCardManager : MonoBehaviour
{
    // singleton instance
    public static SupplyCardManager instance = null;

    [SerializeField]
    private GameObject supplyCard;

    public List<Card> deck = new List<Card>();
    public List<Card> discardPile = new List<Card>();
    public List<Card> hand = new List<Card>();

    private CardManager cardManager;
    private int startingHandSize = 5;

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
    }

    public void DrawSupply() {
        cardManager.DiscardHand();
        cardManager.DrawHand();
        UpdateHandDisplay();
    }

    public void DiscardHand() {
        cardManager.DiscardHand();
        UpdateHandDisplay();
    }

    public void BuyCard(Card card) {
        // check cost
        // if player can afford card
        if (
            card.costMoney > GameController.instance.Money ||
            card.costHealth > GameController.instance.MentalHealth
        ) {
            // TODO ui feedback for can't afford
            Debug.Log("can't afford card");
            Debug.Log("card cost: $" + card.costMoney + " H" + card.costHealth);
            Debug.Log("You have: $" + GameController.instance.Money + " H" + GameController.instance.MentalHealth);
            return;
        }
        GameController.instance.Money = GameController.instance.Money - card.costMoney;
        GameController.instance.MentalHealth = GameController.instance.MentalHealth - card.costHealth;
        // remove card from supply hand
        Card temp = card;
        cardManager.TrashCard(hand, card);
        // add card to player discard
        PlayerCardManager.instance.cardManager.AddCardToDiscard(card);
        // replace card in supply
        cardManager.DrawCard();
        UpdateHandDisplay();
        PlayerCardManager.instance.UpdateAllDisplay();
    }

    // TODO consolidate with PlayerCardManager method 
    public void UpdateHandDisplay() {
        // // remove old hand render
        // foreach (GameObject pos in supplyCardPositions) {
        //     foreach (Transform child in pos.transform) {
        //         GameObject.Destroy(child.gameObject);
        //     }
        // }
        // // render cards in hand
        // for (int i = 0; i < hand.Count; i++) {
        //     Debug.Log("UpdateHandDisplay: " + i);
        //     if (i >= 5) break;
        //     GameObject handCard = Object.Instantiate(supplyCard);
        //     handCard.GetComponent<CardBase>().card = hand[i];
        //     handCard.transform.parent = supplyCardPositions[i].transform;
        //     handCard.transform.localPosition = Vector3.zero;
        // }
        // remove old hand render
        foreach (Transform handCard in handCardParent.transform) {
            GameObject.Destroy(handCard.gameObject);
        }

        // render cards in hand
        for (int i = 0; i < hand.Count; i++) {
            // instantiate card with correct card object
            GameObject handCard = Object.Instantiate(supplyCard);
            handCard.GetComponent<CardBase>().card = hand[i];
            handCard.transform.parent = handCardParent.transform;

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
            Debug.Log("xPos: " + xPos);
            handCard.transform.position = new Vector3(xPos, handYPos, -i);
        }
    }
}
