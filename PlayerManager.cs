using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerManager : MonoBehaviour {

	public GameObject playerPrefab;




	private string newPlayerName;



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
				var newPlayerGO = Instantiate(playerPrefab);


			

	
			
			newPlayerName = "";
			}
		}
		}
}
