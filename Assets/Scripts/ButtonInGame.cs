using UnityEngine;
using System.Collections;

public class ButtonInGame : MonoBehaviour {

	Facility facility;

	void Start(){
		facility = gameObject.GetComponentInParent<Facility> ();
		StartCoroutine (clickEvent ());
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

	void clicked(){
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

	IEnumerator clickEvent(){

		while (GameManager.state == State.CHOOSE_FACILITY_PLOT) {

#if UNITY_EDITOR

			if(Input.GetMouseButtonDown(0)){
				Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

				foreach(RaycastHit2D hit in Physics2D.RaycastAll(pos, Vector2.zero)){

					if(hit.collider.gameObject.Equals(this.gameObject)){
						clicked();
						print("click");
						break;
					}

				}

			}
#endif

			yield return null;
		}

	}
}
