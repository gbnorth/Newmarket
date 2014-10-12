using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CardGame : MonoBehaviour
{



	public CardDeck Deck;
	List<CardDefinition> m_deck = new List<CardDefinition>();
	
	List<Card> m_dealer = new List<Card>();
	List<Card> m_player = new List<Card>();



	//GameObject PlayerWins;
	//GameObject DealerWins;
	//GameObject NobodyWins;

	public Card lowestCard;

	public Card inPlayCard;
	public Card cardInCheck;

	public Transform Player1;

	public Transform playerPrefab;




	
	
	/*enum GameState
	{
		Invalid,
		Started,
		PlayerBusted,
		Resolving,
		DealerWins,
		PlayerWins,
		NobodyWins,
	};*/

	//GameState m_state;
	
	GameObject [] Buttons;
	
	// Use this for initialization
	void Start ()
	{
		//m_state = GameState.Invalid;
		Deck.Initialize();
		Player1 = Instantiate (playerPrefab) as Transform;
		//PlayerWins = this.transform.Find("MessagePlayerWins").gameObject;
		//DealerWins = this.transform.Find("MessageDealerWins").gameObject;
		//NobodyWins = this.transform.Find("MessageTie").gameObject;
		//PlayerWins.SetActive(false);
		//DealerWins.SetActive(false);
		//NobodyWins.SetActive(false);
		//Buttons = new GameObject[3];
		//Buttons[0] = this.transform.Find("Button1").gameObject;
		//Buttons[1] = this.transform.Find("Button2").gameObject;
		//Buttons[2] = this.transform.Find("Button3").gameObject;
		//UpdateButtons();
	}
	
	/*void UpdateButtons()
	{
		Buttons[0].renderer.material.color = Color.blue;
		Buttons[1].renderer.material.color = (m_state == GameState.Started) ? Color.blue : Color.red;
		Buttons[2].renderer.material.color = (m_state == GameState.Started || m_state == GameState.PlayerBusted) ? Color.blue : Color.red;
	}*/
	
	/*void ShowMessage(string msg)
	{
		if (msg == "Dealer")
		{
			PlayerWins.SetActive(false);
			DealerWins.SetActive(true);
			NobodyWins.SetActive(false);
		}
		else if (msg == "Player")
		{
			PlayerWins.SetActive(true);
			DealerWins.SetActive(false);
			NobodyWins.SetActive(false);
		}
		else if (msg == "Nobody")
		{
			PlayerWins.SetActive(false);
			DealerWins.SetActive(false);
			NobodyWins.SetActive(true);
		}
		else
		{
			PlayerWins.SetActive(false);
			DealerWins.SetActive(false);
			NobodyWins.SetActive(false);
		}
	}*/
	
	// Update is called once per frame
	void Update ()
	{
		/*if (Input.GetKeyDown(KeyCode.F1))
		{
			OnReset();
		}
		else if (Input.GetKeyDown(KeyCode.F2))
		{
			OnHitMe();
		}
		else if (Input.GetKeyDown(KeyCode.F3))
		{
			OnStop();
		}
		UpdateButtons();*/
	}
	
	void Shuffle()
	{
		//if (m_state != GameState.Invalid)
		//{
		//}
	}
	
	void Clear()
	{
		foreach (Card c in m_dealer)
		{
			GameObject.DestroyImmediate(c.gameObject);
		}
		foreach (Card c in m_player)
		{
			GameObject.Destroy(c.gameObject);
		}
		m_dealer.Clear();
		m_player.Clear();
		Deck.Reset();
	}
	
	Vector3 GetDeckPosition()
	{
		return new UnityEngine.Vector3(-7,0,0);
	}
	
	const float FlyTime = 0.5f;
	
	void HitDealer()
	{
		CardDef c1 = Deck.Pop();
		if (c1 != null)
		{
			GameObject newObj = new GameObject();
			newObj.name = "Card";
			Card newCard = newObj.AddComponent(typeof(Card)) as Card;

			//add a collider to the newCard
			newCard.gameObject.AddComponent(typeof(BoxCollider));

			//Set the box collider size
			newCard.gameObject.GetComponent<BoxCollider>().size = new Vector3(3f,2.5f,2f);
			//set the boxcollider to be a trigger
			newCard.gameObject.GetComponent<BoxCollider>().isTrigger = true;

			//need to make a listener component here for the trigger
			newCard.gameObject.AddComponent<CardCollisionDetector>();

			newCard.Definition = c1;
			newObj.transform.parent = Deck.transform;
			newCard.TryBuild();
			float x = -3+(m_dealer.Count)*2.0f;
			float z = (m_dealer.Count)*-0.1f;
			Vector3 deckPos = GetDeckPosition();
			newObj.transform.position = deckPos;
			m_dealer.Add(newCard);
			newCard.SetFlyTarget(deckPos,new Vector3(x,2,z),FlyTime);
			inPlayCard = newCard;

		}
	}




	void DealPlayer()
	{
		CardDef c1 = Deck.Pop();
		if (c1 != null)
		{

			GameObject newObj = new GameObject();
			newObj.name = "Card";
			Card newCard = newObj.AddComponent(typeof(Card)) as Card;
			//add a collider to allow card to be moveable
			newCard.gameObject.AddComponent(typeof(BoxCollider));
			//init box collider
			newCard.gameObject.GetComponent<BoxCollider>().size = new Vector3(3f,2.5f,0f);
			//add a rigidbody
			newCard.gameObject.AddComponent(typeof(Rigidbody));
			//init rigidbody
			newCard.gameObject.GetComponent<Rigidbody>().useGravity = false;
			//Add draggable component
			newCard.gameObject.AddComponent(typeof(Draggable));

			newCard.Definition = c1;
			newObj.transform.parent = Deck.transform;
			newCard.TryBuild();
			float x = -3+(m_player.Count)*1.5f;
			float y = -3-m_player.Count*0.15f;
			float z = (m_player.Count)*-0.1f;
			//newObj.transform.position = new Vector3(x,-3,z);
			m_player.Add(newCard);
			Vector3 deckPos = GetDeckPosition();
			newCard.transform.position = deckPos;
			newCard.SetFlyTarget(deckPos,new Vector3(x,-2,z),FlyTime);
			newCard.transform.parent = Player1.transform;
			getPlayerLowCard();
			//setTargetCardValues();

		}
	}
	
	static int Value(Card c)
	{

		if (c != null)
		{
			switch (c.Definition.Text)
			{
			
			case "2":
				return 2;
			case "3":
				return 3;
			case "4":
				return 4;
			case "5":
				return 5;
			case "6":
				return 6;
			case "7":
				return 7;
			case "8":
				return 8;
			case "9":
				return 9;
			case "10":
				return 10;
			case "J":
				return 11;
			case "Q":
				return 12;
			case "K":
				return 13;
			case "A":
				return 14;

			}
			//return c.Definition.Text;
			return 0;
		}
		return 0;
	}
	
	static int GetScore(List<Card> cards)
	{
		int score = 0;
		bool ace = false;
		foreach (Card c in cards)
		{
			int s = Value(c);
			if ((score + s) > 21)
			{
				if (s == 11)
				{
					s = 1;
				}
				else if (ace)
				{
					score -= 10;
					ace = false;
				}
			}
			score += s;
			ace |= (s == 11);
		}
		return score;
	}
	
	int GetDealerScore()
	{
		return GetScore(m_dealer);
	}
	
	int GetPlayerScore()
	{
		return GetScore(m_player);
	}
	
	const float DealTime = 0.35f;
	
	IEnumerator OnDeal()
	{
		//if (m_state != GameState.Resolving)
		{
			//m_state = GameState.Resolving;
			//ShowMessage("");
			Clear();
			Deck.Shuffle();
			HitDealer();
			yield return new WaitForSeconds(DealTime);
			for (int i = 0; i < 7; i++) {
				DealPlayer();
			}

			//m_state = GameState.Started;
		}
	}


	void MoveCardToDeck(){
		Component.Destroy(cardInCheck.GetComponent<Rigidbody> ());
		Component.Destroy (cardInCheck.GetComponent<BoxCollider> ());
		Component.Destroy (cardInCheck.GetComponent<Draggable> ());

		cardInCheck.SetFlyTarget(cardInCheck.transform.position,inPlayCard.transform.position,FlyTime);
		//move the card in check gameobject into the deck gameobject
		cardInCheck.transform.parent = Deck.transform;

		//push the cards z pos
		var cicX = cardInCheck.transform.position.x;
		/*var cicY = cardInCheck.transform.position.y;
		var cicZ = cardInCheck.transform.position.z;
		var cicTP = new Vector3 (cicX, cicY, cicZ + 0.01f);
		cardInCheck.transform.position = cicTP;*/

		//push all the child objects z positions back -0.1 of deck

		var deckObj = GameObject.Find("Deck").transform;//Get the deck object
		//iterate over all child objs
		foreach (Transform child in deckObj) {
			var cX = child.position.x; //get the childs X postion
			var cY = child.position.y; //get the Y
			var cZ = child.position.z; // get the Z
			var cT = new Vector3(cX,cY,cZ + 0.01f);//set the new target position i.e. z = z -0.1f (move it back in z space)
			child.position = cT;//set the new positon for each child obj
				}


		inPlayCard = cardInCheck;
		}


	void SetPickedUpCard(Card c){
		cardInCheck = c;


		}

	void OnCheckCards()
	{
		//get the inPlayCard

		var c1 = Value (inPlayCard);


		//foreach (Transform child in Player1) { //Need to change this to the card thats been selected

						//cardInCheck = child.GetComponent<Card> ();
						//cardInCheck = child;
						if(cardInCheck != null){
						var c2 = Value (cardInCheck);
						var c3 = c1 + 1;
						if (c2 == c3) {
				if (inPlayCard.GetComponent<Card> ().Definition.Symbol ==  cardInCheck.GetComponent<Card> ().Definition.Symbol){
										Debug.Log ("Same"); //child.GetComponent<Card> ().Definition.Symbol)

										//move the card
										MoveCardToDeck ();
										//cardInCheck.SetFlyTarget(cardInCheck.transform.position,inPlayCard.transform.position,FlyTime);
										//cardInCheck.transform.parent = Deck.transform;

										//set the card in play to the cardincheck
										//inPlayCard = cardInCheck;
								}

						} else {
								Debug.Log ("no");
							//send it back to it's original pos

								//Debug.Log (c1);Debug.Log (c2);Debug.Log (c3);
						}
			}


				//}



				




		/*if (m_state == GameState.Started)
		{
			HitPlayer();
			if (GetPlayerScore() > 21)
			{
				m_state = GameState.PlayerBusted;
			}
		}*/
	}
	/*bool TryFinalize(int playerScore, int dealer)
	{
		if (playerScore > 21)
		{
			// Dealer Wins!
			ShowMessage("Dealer");
			m_state = GameState.DealerWins;
			return true;
		}
		if (dealer > 21)
		{
			ShowMessage("Player");
			m_state = GameState.PlayerWins;
			return true;
		}
		if (dealer > playerScore)
		{
			ShowMessage("Dealer");
			m_state = GameState.DealerWins;
			return true;
		}
		// Natural 21 beats everything else.
		bool pn = (playerScore == 21) && (m_player.Count == 2);
		bool dn = (dealer == 21) && (m_dealer.Count == 2);
		if (pn && !dn)
		{
			ShowMessage("Player");
			m_state = GameState.PlayerWins;
			return true;
		}
		if (dn && !pn)
		{
			ShowMessage("Dealer");
			m_state = GameState.DealerWins;
			return true;
		}
		if (dealer > 17)
		{
			if (playerScore == dealer)
			{
				// Nobody Wins!
				ShowMessage("Nobody");
				m_state = GameState.NobodyWins;
				return true;
			}
			else if (dealer < playerScore)
			{
				// Player Wins!
				ShowMessage("Player");
				m_state = GameState.PlayerWins;
				return true;
			}
			else
			{
				// Dealer Wins!
				ShowMessage("Dealer");
				m_state = GameState.DealerWins;
				return true;
			}
		}
		return false;
	}*/
	/*IEnumerator OnStop()
	{
		if (m_state == GameState.Started || m_state == GameState.PlayerBusted)
		{
			m_state = GameState.Resolving;
			UpdateButtons();
			int playerScore = GetPlayerScore();
			while (true)
			{
				int d = GetDealerScore();
				Debug.Log(string.Format("Player={0}  Dealer={1}",playerScore,d));
				if (TryFinalize(playerScore,d))
				{
					break;
				}
				else
				{
					Debug.Log("HitDealer");
					HitDealer();
					yield return new WaitForSeconds(DealTime*1.5f);
				}
			}
		}
	}*/
	
	public void OnButton(string msg)
	{
		Debug.Log("OnButton = "+msg);
		switch (msg)
		{
		case "Deal":
			StartCoroutine(OnDeal());
			break;
		case "checkCards":
			OnCheckCards();
			break;
		case "putLowCard":
			StartCoroutine(PutLowCard());
			break;
		}
	}


	IEnumerator PutLowCard(){
		Debug.Log ("Put low card down");
		//get the low card
		//move it
		if (lowestCard == null) {
			//create a collection of players cards
			List<Card> cardArray = new List<Card>();
			foreach(Transform child in Player1){
				cardArray.Add (child.GetComponent<Card>());
				lowestCard = cardArray[0];
				foreach(Card c in cardArray){
					if(Value (c) < Value (lowestCard)){
						lowestCard = c;
					}
				}
				Debug.Log (lowestCard);
			}

				}
		var zPosTarget = inPlayCard.transform.position.z - 0.1f;
		var xPosTarget = inPlayCard.transform.position.x + 0.1f;
		var targetPos = new Vector3 (xPosTarget, inPlayCard.transform.position.y, zPosTarget);
		lowestCard.SetFlyTarget(lowestCard.transform.position,targetPos,FlyTime);
		lowestCard.transform.parent = Deck.transform;
		// remove components
		Component.Destroy(lowestCard.GetComponent<Rigidbody> ());
		//Component.Destroy (lowestCard.GetComponent<BoxCollider> ());
		Component.Destroy (lowestCard.GetComponent<Draggable> ());

		inPlayCard = lowestCard;
		lowestCard = null;
		yield break;
		}



	public int getPlayerLowCard(){

		var lowCard = m_player [0];
		for (int i = 0; i < m_player.Count; i++) {
			if(Value(m_player[i]) < Value (lowCard)){
				lowCard = m_player[i];
				}
			}
		return this.Value(lowCard);
	}


	public void isSameCard(){
				
		}



		
}
