using UnityEngine;
using System.Collections;

public class BetPlacedEventListener : MonoBehaviour {

	private bool isPlaced = false;

	void OnEnable(){
		EventManager.onBetPlaced += onBetPlaced;
		}

	void OnDisable(){
		EventManager.onBetPlaced -= onBetPlaced;
	}


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		}

	void onBetPlaced(GameObject card){
		if(card.CompareTag("betcard")){//check the object clicked is a bet card
			//transfer coin to card
			GameObject currentPlayer = GameObject.Find("Player") as GameObject;//get the player
			if(currentPlayer.transform.childCount > 0){//check it has coin child objects
				if(isPlaced == false){//check we can bet
			var coinTobet = currentPlayer.GetComponentInChildren<Coin>().gameObject.transform;//get a coin
			
		
			//set the bet
			///////////
			 coinTobet.parent = GameObject.Find(card.name).transform;//move to card
				//change coin position on screen
				var targetPosition = new Vector3(card.transform.position.x,card.transform.position.y, -1);
				//var startPos = transform.position;
				coinTobet.transform.position = targetPosition;


			var coinToPool = currentPlayer.GetComponentInChildren<Coin>().gameObject.transform;//get a coin for the pool
				//set the pool
					var poolTarget = GameObject.FindGameObjectWithTag("bettingpool").transform;
					coinToPool.parent = poolTarget;
					coinToPool.position = poolTarget.position;

				}
				isPlaced = true;
		}else{
			print ("out of coins");
		}

		}
	}
}




