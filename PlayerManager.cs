using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;




	private string newPlayerName;
	List<GameObject> mPlayers = new List<GameObject>();

	public float GUIxpos = 0.0f;


	// Use this for initialization
	void Start () {
		newPlayerName = "";
	
	}


	
	// Update is called once per frame
	void Update () {
	
	}

	void OnGUI(){




		// add a textfield for the players name

		 newPlayerName = GUI.TextArea (new Rect (50, 75, 150, 20), newPlayerName, 20);

		if(GUI.Button(new Rect(50f,40,150f,20f),"Create new player")){
			if(newPlayerName != ""){
			//create new game object from prefab
				var newPlayerGO = Instantiate(playerPrefab) as GameObject;
				var newPlayerComponent = newPlayerGO.transform.GetComponent<PlayerWithState>();
				//change its name
				newPlayerComponent.mPlayerName = newPlayerName;
				//set new players x pos
				newPlayerComponent.myXpos += 60.0f * mPlayers.Count;
				Debug.Log (mPlayers.Count);
				//add new GO to list of players
				mPlayers.Add(newPlayerGO as GameObject);

			
			

	
			
			newPlayerName = "";
			}
		}
		}
}
