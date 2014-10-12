using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Hand : MonoBehaviour {

	//mainstains a collection of cards


	List<Card> hand = new List<Card>();

	//add a card
	void addCardToHand(Card c){
		hand.Add (c);
		}

	public Card getCardFromHand(Card c){
				if (hand.Contains (c)) {
						return c;
				} else {
						print ("no card found that matches");
						return null;
				}
		}
}
