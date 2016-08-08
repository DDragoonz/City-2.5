using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NPC : MonoBehaviour {


	public Vector2 destination;
	public Vector2 tempDestination;

	public string[] allFacilityID;
	
	List<Vector2> allDestination;

	Animator anim;


	RaycastHit2D[] hits;
	RaycastHit2D curHit;

	NPCState state;

	PolygonCollider2D poly;
	int startIdx,finishIdx;
	float lastX,lastY;
	float speed;
	
	// Use this for initialization
	void Start () {

		speed = Random.Range (0.3f, 0.7f);

		anim = GetComponent<Animator> ();
		state = NPCState.IDLE;
		allDestination = new List<Vector2> ();



		StartCoroutine (FSMController ());

	}

	void switchAnimationFacing(Vector2 target){
		if (anim) {
			if(transform.position.x < target.x && transform.position.y <= target.y )anim.SetInteger("facing",0);
			if(transform.position.x < target.x && transform.position.y >= target.y )anim.SetInteger("facing",1);
			if(transform.position.x >= target.x && transform.position.y > target.y )anim.SetInteger("facing",2);
			if(transform.position.x >= target.x && transform.position.y < target.y )anim.SetInteger("facing",3);
		}
	}

	
	// Update is called once per frame
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





	void findNearestPoint(){
		float minToSelf = 99999,minToDest = 9999999;
		startIdx = finishIdx = 0;



		for (int i = 0; i<poly.points.Length; i++) {
			Vector2 newPos = new Vector2(poly.transform.position.x,poly.transform.position.y);
			newPos+=poly.points[i]+poly.offset;

//			print (i+" : " +Vector2.Distance(transform.position,newPos));

			if(Vector2.Distance(transform.position,newPos) < minToSelf){
				minToSelf =  Vector2.Distance(transform.position,newPos);
				startIdx = i;

			}

			if(Vector2.Distance(destination,newPos) < minToDest){ 
				minToDest =  Vector2.Distance(destination,newPos);
				finishIdx = i;

			}
		}
		print ("i will start on "+startIdx+" point");
		print ("and finish on "+finishIdx+" point");

	}



	
	IEnumerator FSMController(){
		while (true) {

			switch(state){

			case NPCState.IDLE:
				if(anim)anim.SetBool("moving",false);
				yield return StartCoroutine(idle());
				break;

			case NPCState.FINDPATH:
				yield return StartCoroutine(findPath());
				break;

			case NPCState.MOVING:
				if(anim)anim.SetBool("moving",true);
				yield return StartCoroutine(moving());
				break;

			case NPCState.CHECK_DESTINATION:
				yield return StartCoroutine(checkDestination());
				break;

			case NPCState.ENCIRCLE:
				if(anim)anim.SetBool("moving",true);
				yield return StartCoroutine(encircle());
				break;
			}


		}
	}

	IEnumerator idle(){
		print ("relax for a while...");
		yield return new WaitForSeconds (Random.Range(0,3));
		print("where i shall go today?");
		updateDestination ();
		state = NPCState.FINDPATH;

	}

	IEnumerator moving(){

		print ("time to move!");

		switchAnimationFacing (tempDestination);

		while (Vector2.Distance(transform.position,tempDestination)>0.1f) {

			transform.position = Vector2.MoveTowards(transform.position , tempDestination, Time.deltaTime*speed);
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
			yield return null;
		}
		print ("am i arrive?");
		state = NPCState.CHECK_DESTINATION;
//		state=NPCState.IDLE;
	}
	IEnumerator findPath(){

		print ("let see where is my destination");
		tempDestination = destination;

		hits = Physics2D.LinecastAll (transform.position, destination);
		
		foreach (RaycastHit2D hit in hits) {
			if((hit.collider.name.Equals("plot")|| hit.collider.name.Equals("Not Passable"))&& Vector2.Distance(hit.point,transform.position)>0.1f){
//				print ("my path blocked by " + hit.collider.GetComponentInParent<Facility>().name);
				curHit = hit;
				poly = (PolygonCollider2D)hit.collider;

				tempDestination = hit.point;
				break;
			}
		}

		state = NPCState.MOVING;
		yield return null;
//		destination = tempDestination;
		
	}
	IEnumerator checkDestination(){
		if (Vector2.Distance (curHit.collider.bounds.center, destination) < 0.05f) {
			print ("arrive in final destination! i want to take a break");
			state = NPCState.IDLE;
		} else {
			print ("should go around");
			findNearestPoint();
			state = NPCState.ENCIRCLE;
		}

		yield return null;
	}

	IEnumerator encircle(){

		int icr = 1;



		if (startIdx > finishIdx) {



			if (startIdx - finishIdx < poly.points.Length/2) {
				icr = -1;
			} else
				icr = 1;

		} else if ( startIdx < finishIdx){



			if( finishIdx - startIdx < poly.points.Length/2 ){
				icr = 1;
			}
			else icr = -1;
		}


		for (int i = startIdx; i!= finishIdx; ) {




//			print ("encircling... "+i );

			Vector2 newPos = new Vector2(poly.transform.position.x,poly.transform.position.y);
			newPos+=poly.points[i]+poly.offset;

			switchAnimationFacing(newPos);

			while(Vector2.Distance(transform.position,newPos )>0.05f){

//				print (poly.transform.position);

				transform.position = Vector2.MoveTowards(transform.position , newPos, Time.deltaTime*speed);
				transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
				yield return null;

			}

			i+=icr;
			if(i >= poly.points.Length) i = 0;
			if(i < 0)i = poly.points.Length-1;

		}


		switchAnimationFacing(new Vector2(poly.transform.position.x,poly.transform.position.y) + poly.points[finishIdx]+poly.offset);
		
		while(Vector2.Distance(transform.position,
		                       new Vector2(poly.transform.position.x,poly.transform.position.y) + poly.points[finishIdx]+poly.offset )>0.05f){
			
			//				print (poly.transform.position);
			
			transform.position = Vector2.MoveTowards(transform.position , new Vector2(poly.transform.position.x,poly.transform.position.y)
			                                         + poly.points[finishIdx]+poly.offset, Time.deltaTime*speed);
			transform.position = new Vector3 (transform.position.x, transform.position.y, transform.position.y);
			yield return null;
			
		}

		print("ok, let see where am i");
		state = NPCState.FINDPATH;

	}


	private enum NPCState{
		IDLE,
		FINDPATH,
		MOVING,
		CHECK_DESTINATION,
		ENCIRCLE

	}
}
