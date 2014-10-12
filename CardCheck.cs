using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardCheck : MonoBehaviour {
	public Card targetCard;
	public Card playerCard;
	
	public bool compareCards;
	// Use this for initialization
	void Start () {
		 
	}
	
	// Update is called once per frame
	void Update () {

		CompareCards ( targetCard, playerCard );


	}


	public bool CompareCards(Card a, Card b){

		if (a.GetComponent<Card> ().Definition.Symbol == b.GetComponent<Card> ().Definition.Symbol) {
			if (a.GetComponent<Card> ().Definition.Pattern + 1  == b.GetComponent<Card> ().Definition.Pattern ) {

				return compareCards = true;
						} else {

				return compareCards = false;
						}

			return compareCards = false;
				}

		return compareCards = false;

	}



	static int Value(Card c)
			{
				if (c != null)
				{
					switch (c.Definition.Pattern)
					{
					case 0:
						return 10;
					case 1:
						return 11;
					}
					return c.Definition.Pattern;
				}
				return 0;
			}


	public void updateTargetCard(Card playerCard){
		targetCard = playerCard;



		}
		
}
