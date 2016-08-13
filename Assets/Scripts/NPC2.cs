using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC2 : MonoBehaviour{

	Vector2 finalDestination,tempDestination;
	PolygonCollider2D finalPoly;
	List<Vector2> possibleDestination;
	List<Vector2> altRoute;

	public string[] allDestinationFacilityId;
	public float speed;

	int startIdx,finishIdx;

	Animator anim;

	LineRenderer l;

	void Start(){
		
		anim = GetComponent<Animator> ();

		possibleDestination = new List<Vector2> ();
		altRoute = new List<Vector2> ();

		l = GetComponent<LineRenderer> ();

		StartCoroutine(NPCActivity());


	}

	void Update(){
		l.SetPosition (0, new Vector3(transform.position.x,transform.position.y,-5f));
		l.SetPosition (1, new Vector3(tempDestination.x,tempDestination.y,-5f));
	}

	void switchAnimationFacing(Vector2 target){
		if (anim) {
			if(transform.position.x < target.x && transform.position.y <= target.y )anim.SetInteger("facing",0);
			if(transform.position.x < target.x && transform.position.y >= target.y )anim.SetInteger("facing",1);
			if(transform.position.x >= target.x && transform.position.y > target.y )anim.SetInteger("facing",2);
			if(transform.position.x >= target.x && transform.position.y < target.y )anim.SetInteger("facing",3);
		}
	}

	void updateDestination (){
		
		possibleDestination.Clear ();
		
		foreach (Facility f in FindObjectsOfType<Facility>()) {
			foreach (string s in allDestinationFacilityId) {
				if (f.data.facilityId.Equals (s)) {
					possibleDestination.Add (new Vector2 (f.data.posX, f.data.posY));
					finalPoly = f.GetComponentInChildren<Plot>().GetComponent<PolygonCollider2D>();
					break;
				}
			}
		}

		if (possibleDestination.Count > 0) {
			finalDestination = possibleDestination [Random.Range (0, possibleDestination.Count)];
		} else
			Destroy (gameObject);

	}

	IEnumerator NPCActivity(){



		while (true) {
			updateDestination ();
			yield return new WaitForSeconds(Random.Range(0.1f,3f));
			ObservingDestination();
			yield return StartCoroutine(Moving());

		}

	}

	void ObservingDestination(){


		tempDestination = finalDestination;
		altRoute.Clear ();



		foreach(RaycastHit2D hit in Physics2D.RaycastAll(transform.position, finalDestination, Vector2.Distance(transform.position, finalDestination) )){

			if(hit.collider.name.Equals("River")){

				print("River ahead");

				tempDestination = hit.collider.GetComponentInChildren<EdgeCollider2D>().transform.position;


				break;
			}

		}
		
		foreach(RaycastHit2D hit in Physics2D.RaycastAll(transform.position, tempDestination)){
			print (hit.collider.name);
			if( hit.distance >0.1f){
				if(hit.collider.tag.Equals("Plot")){ // if facing plot and not this plot

					print("seeing plot");

					PolygonCollider2D p = (PolygonCollider2D)hit.collider;

					foreach(Vector2 v in p.points){
						altRoute.Add(v+p.offset+(Vector2)p.transform.position);
					}

					break;

				}
				else if(hit.collider.name.Equals("Bridge")){
					print ("seeing bridge");

					EdgeCollider2D e = (EdgeCollider2D)hit.collider;
					foreach(Vector2 v in e.points){
						altRoute.Add(v+ e.offset + (Vector2)e.transform.position);
					}

					break;
				}

			}
			
		}
	}

	void findNearestPoint(){
		float minToSelf = 99999,minToDest = 9999999;
		startIdx = finishIdx = 0;
		
		
		
		for (int i = 0; i<altRoute.Count; i++) {

			
			print (i+" to self : " +Vector2.Distance(transform.position,altRoute[i]));
			print (i+" to dest : " +Vector2.Distance(tempDestination,altRoute[i]));
			
			if(Vector2.Distance(transform.position,altRoute[i]) < minToSelf){
				minToSelf =  Vector2.Distance(transform.position,altRoute[i]);
				startIdx = i;
				
			}
			
			if(Vector2.Distance(tempDestination,altRoute[i]) < minToDest){ 
				minToDest =  Vector2.Distance(tempDestination,altRoute[i]);
				finishIdx = i;
				
			}
		}

		
	}

	IEnumerator Moving(){

		findNearestPoint ();

		int icr = 0;

		if (startIdx > finishIdx) {
			
			
			
			if (startIdx - finishIdx < altRoute.Count / 2) {
				icr = -1;
			} else
				icr = 1;
			
		} else if (startIdx < finishIdx) {
			
			
			
			if (finishIdx - startIdx < altRoute.Count / 2) {
				icr = 1;
			} else
				icr = -1;
		}
		
		
		for (int i = startIdx; i!= finishIdx;) {

			while (Vector2.Distance(transform.position,altRoute[i]  )>0.1f) {
				transform.position = Vector2.MoveTowards (transform.position, altRoute[i], Time.deltaTime * speed);
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
				yield return null;
			}

			i += icr;
			if (i >= altRoute.Count)
				i = 0;
			if (i < 0)
				i = altRoute.Count - 1;

		}
		if (altRoute.Count > 0) {
			while (Vector2.Distance(transform.position,altRoute[finishIdx]  )>0.1f) {
				transform.position = Vector2.MoveTowards (transform.position, altRoute [finishIdx], Time.deltaTime * speed);
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
				yield return null;
			}
		}


	}






}