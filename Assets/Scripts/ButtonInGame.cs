using UnityEngine;
using System.Collections;

public class ButtonInGame : MonoBehaviour {

	Facility facility;

	void Start(){
		facility = gameObject.GetComponentInParent<Facility> ();
	}

//	void OnMouseOver(){
//		if (Input.GetMouseButtonDown (0)) {
//			switch(this.name){
//			case "Build ok": 
//				Destroy (GameObject.FindGameObjectWithTag ("Build Menu"));
//				foreach (GameObject g in GameObject.FindGameObjectsWithTag("Objects")) {
//					g.GetComponent<SpriteRenderer> ().color = Color.white;
//					g.GetComponent<PolygonCollider2D> ().enabled = true;
//				}
//				GameManager.state = State.REGION_GAMEPLAY;
//				break;
//			case "Build no":
//				Plot plot = GetComponentInParent<Plot> ();
//				int category = plot.category;
//				plot.data.empty = true;
//				Destroy (plot.gameObject.GetComponentInChildren<Facility> ().gameObject);
//				foreach (GameObject g in GameObject.FindGameObjectsWithTag("Plot")) {
//
//					if (g.GetComponent<Plot> ().category == category) {
//						g.GetComponent<Plot> ().showPlot ();
//
//					}
//				}
//				Destroy (GameObject.FindGameObjectWithTag("Build Menu"));
//				GameManager.state = State.CHOOSE_FACILITY_PLOT;
//				break;
//			}	
//		}



//	}

	void OnMouseDown(){
			switch(this.name){
			case "Build ok": 
				if (facility.finishPlacing ()) {
					
					Destroy (GameObject.FindGameObjectWithTag ("Menu"));

					GameManager.activeTown.money -= facility.data.price;

					FindObjectOfType<UI_Manager_Region> ().updateValues ();

					GameManager.activeTown.facilities.Add (facility.data);

					GameManager.state = State.REGION_GAMEPLAY;
				} 
				break;
			case "Build no":
				Destroy (facility.gameObject);
				Destroy (GameObject.FindGameObjectWithTag("Menu"));
				GameManager.state = State.REGION_GAMEPLAY;
				break;
			}	

	}
}
