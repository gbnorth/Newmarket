using UnityEngine;
using System.Collections;

public class Dealer : MonoBehaviour {


	public enum DealerState
	{
		DEAL,
		WAIT
	}

	DealerState mDealerState = DealerState.WAIT;



	public void UpdateState(){

		switch (mDealerState) {

		case DealerState.WAIT:
			mDealerState = DealerState.WAIT;
			Debug.Log("Dealer is waiting...");

			break;

		case DealerState.DEAL:
			mDealerState = DealerState.DEAL;
			Debug.Log ("Dealing...");

			break;

		
				}
		UpdateState ();

		}



	void OnGUI(){
				if (GUI.Button (new Rect (10, 10, 50, 50), "Deal")) {
			mDealerState = DealerState.DEAL;
			UpdateState();
		}else if (GUI.Button (new Rect (10, 75, 50, 50), "Wait")) {
			mDealerState = DealerState.WAIT;
			UpdateState();
		}else if(GUI.Button (new Rect (10, 200, 50, 50), "State Incr")) {

			UpdateState();
		}
	}
}
