using UnityEngine;
using System.Collections;

public class Bet : MonoBehaviour {

	public Transform coin;
	public Transform coinPrefab;

	// Use this for initialization
	void Start () {
		coin = null;
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	void initBet(){
		coin = Instantiate (coinPrefab) as Transform;
		}
}
