using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CardUtility
{
    // Fischer-Yates Shuffle Algorithm
    public static List<Card> Shuffle(List<Card> a) {
        List<Card> cardsToShuffle = new List<Card>(a);
		// Loops through array
		for (int i = cardsToShuffle.Count-1; i > 0; i--)
		{
			// Randomize a number between 0 and i (so that the range decreases each time)
			int rnd = Random.Range(0, i);
			
			// Save the value of the current i, otherwise it'll overright when we swap the values
			Card temp = cardsToShuffle[i];
			
			// Swap the new and old values
			cardsToShuffle[i] = cardsToShuffle[rnd];
			cardsToShuffle[rnd] = temp;
		}
        return cardsToShuffle;
	}
}
