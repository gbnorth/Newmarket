using UnityEngine;
using System.Collections;

public class PlayerWithState : MonoBehaviour 

{


	public string mPlayerName;

	//contructor

	public PlayerWithState(string mName){
		//init new player name
		mPlayerName = mName;
		this.transform.gameObject.name = mPlayerName;

		}




	enum PlayerState
	{
		STARTING,
		BETTING,
		ROUNDENDED,
		PAUSE,
		CHECKCARDS,
		GETTINGLOWCARD
	}

	public string GetPlayerState(){
		return this.mPlayerState.ToString ();
		}


	//Create a new player state
	PlayerState mPlayerState = PlayerState.STARTING;

	public void NextState(string mMessage){

				switch (mPlayerState) {
				//whichever state it is, move it onto the next one
				case PlayerState.STARTING:				
						mPlayerState = PlayerState.BETTING;
						break;
			
				case PlayerState.BETTING:
						mPlayerState = PlayerState.PAUSE;
						break;

				case PlayerState.PAUSE:
						if (mMessage == "ROUNDENDED") {
								mPlayerState = PlayerState.ROUNDENDED;
						} else {
								mPlayerState = PlayerState.CHECKCARDS;
						}
						break;

				case PlayerState.ROUNDENDED:
						mPlayerState = PlayerState.STARTING;
						break;

				case PlayerState.CHECKCARDS:
						if(mMessage == "PAUSE"){
							mPlayerState = PlayerState.PAUSE;
						} else{
							mPlayerState = PlayerState.GETTINGLOWCARD;
						}
						break;

				case PlayerState.GETTINGLOWCARD:
						mPlayerState = PlayerState.PAUSE;
						break;
				}
						
				
		}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
			Debug.Log (mPlayerName + " is " + GetPlayerState ());
	}


	void OnGUI(){
		if(GUI.Button(new Rect(10,10,50,50),"Next")){
			NextState(null);
		}

		GUI.Label (new Rect (10, 75, 150, 50), this.GetPlayerState ());

		}
}
