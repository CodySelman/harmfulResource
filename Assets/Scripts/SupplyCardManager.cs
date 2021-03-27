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

    public List<GameObject> supplyCardPositions;

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
        cardManager = new CardManager(deck, discardPile, hand, startingHandSize);
        cardManager.DrawHand();
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
    void UpdateHandDisplay() {
        // remove old hand render
        foreach (GameObject pos in supplyCardPositions) {
            foreach (Transform child in pos.transform) {
                GameObject.Destroy(child.gameObject);
            }
        }
        // render cards in hand
        for (int i = 0; i < hand.Count; i++) {
            Debug.Log("UpdateHandDisplay: " + i);
            if (i >= 5) break;
            GameObject handCard = Object.Instantiate(supplyCard);
            handCard.GetComponent<CardBase>().card = hand[i];
            handCard.transform.parent = supplyCardPositions[i].transform;
            handCard.transform.localPosition = Vector3.zero;
        }
    }
}
