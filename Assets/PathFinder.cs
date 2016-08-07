using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PathFinder : MonoBehaviour {



	public Vector2 destination;

	public Vector2 tempDestination;

	public string[] allFacilityID;

	List<Vector2> allDestination;

	RaycastHit2D[] hits;
	RaycastHit2D hit;
	PolygonCollider2D poly;
	int polyIndex;

	NPCState state;


	// Use this for initialization
	void Start () {
		state = NPCState.IDLE;
		tempDestination = destination;

		allDestination = new List<Vector2> ();
		updateDestination ();

		isPathClear ();

		StartCoroutine (StateMachine());
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
	}

	void updateDestination (){

		allDestination.Clear ();

		foreach (Facility f in FindObjectsOfType<Facility>()) {
			foreach(string s in allFacilityID){
				if(f.data.facilityId.Equals(s)){
					allDestination.Add( new Vector2( f.data.posX,f.data.posY));
					break;
				}
			}
		}

		destination = allDestination[Random.Range(0,allDestination.Count)];
	}

	bool isPathClear(){
		hits = Physics2D.LinecastAll (transform.position, destination);
		foreach (RaycastHit2D hit in hits) {


			if(hit.collider.name.Equals("plot")){

				 if(this.hit != hit){

					tempDestination = hit.point;
					this.hit = hit;
					poly = (PolygonCollider2D)hit.collider;
					print ("changing plot");
					return false;

				}

				if(Vector2.Distance(hit.collider.gameObject.transform.position,destination)<0.1f){
					print("destination plot found");
					print(hit.collider.gameObject.transform.position);
					return true;
				}


			}

		}



		print ("line unblocked");
		return true;



	}

	IEnumerator encircle(){
		while (Vector2.Distance (tempDestination, transform.position) >= .1f) {

			yield return null;

		}
		print ("encircling");
		tempDestination = polyIndex+1>=poly.points.Length? (poly.points[0]*1.3f +poly.offset) : (poly.points[polyIndex++]*1.3f+poly.offset);

	}

	void reRoute(){
		print ("rerouting");
		float minVal = 9999;
		for(int i=0; i<poly.points.Length; i++){
			if(Vector2.Distance(transform.position,poly.points[i])<minVal){
				minVal = Vector2.Distance(transform.position,poly.points[i]);
				polyIndex = i;
			}
		}
	}

	IEnumerator StateMachine(){
		while (true) {
			switch(state){
			case NPCState.IDLE:
				yield return StartCoroutine(idle());
				break;
			case NPCState.MOVING:
				yield return StartCoroutine(moving());
				break;
			}
		}
	}

	IEnumerator moving(){



		while (Vector2.Distance(transform.position, destination)>0.1f) {

			transform.position =  Vector2.MoveTowards(transform.position,tempDestination,Time.deltaTime*.5f);
			if(isPathClear()){

				tempDestination = destination;
			}
			else {
				print ("path blocked");
				reRoute();
				StartCoroutine( encircle());
			}
			yield return null;
		}
		state = NPCState.IDLE;
		print ("move finish");
	}

	IEnumerator idle(){

		yield return new WaitForSeconds (Random.Range(1,5));

		updateDestination ();
		state = NPCState.MOVING;

		print ("i'll move");

	}

	private enum NPCState{
		IDLE,
		MOVING,
		INSIDE
	}
}
