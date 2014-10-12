using UnityEngine;
using System.Collections;

public class EventManager : MonoBehaviour {

	public delegate void TouchEventHandler( GameObject card );
	public static event TouchEventHandler onBetPlaced;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast (ray, out hit, 100f)) {
				if (hit.collider.gameObject && hit.collider != null) {
					onBetPlaced(hit.collider.gameObject);
				}
				
			}
		}
		
	}


}
