using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Facility : MonoBehaviour
{

	public bool isSelected;
	public Sprite selected, normal, building, destroying;

	public float price;
	public float happiness;
	public float education;
	public float income;
	public float crime;
	public float health;
	public float polution;
	public float traffic;
	public float  eficiency;

	public int buildTime, destroyTime, repairtime;

	public int 
	economicPointToUnlock,
	happinessPointToUnlock,
	polutionPointToUnlock,
	crimePointToUnlock,
	educationPointToUnlock,
	healthPointToUnlock,
	trafficPointToUnlock;

	public string[] facilityIdToUnlock;

	public string keterangan;

	public FacilityData data;

	GameObject detailPanel;

	Vector2[] defaultPoints,plotPoints;

	public void init (FacilityData f)
	{
		data = f;
		gameObject.transform.position = new Vector3 (f.posX, f.posY, f.posY);
		switch (data.state) {
		case facilityState.NORMAL:
			GetComponent<SpriteRenderer> ().sprite = normal;
			break;
		case facilityState.DESTROYING:
			GetComponent<SpriteRenderer> ().sprite = destroying;
			break;
		case facilityState.BUILDING:
			GetComponent<SpriteRenderer> ().sprite = building;
			break;
		}
		isSelected = false;
		if (!data.isNew ())
			Destroy (GetComponent<Rigidbody2D> ());
		
//		data.timeCounter -= (float)GameManager.timeDifferenceInSeconds ();
	}



	// Use this for initialization
	void Start ()
	{
//		isSelected = false;
//		if (!data.isNew ())
//			Destroy (GetComponent<Rigidbody2D> ());
		transform.position =  new Vector3(transform.position.x, transform.position.y, transform.position.y);
		data.posX = transform.position.x;
		data.posY = transform.position.y;

		detailPanel = FindObjectOfType<UI_Manager_Region> ().detailPanel;

		defaultPoints = GetComponent<PolygonCollider2D> ().points;
		PolygonCollider2D plot = GetComponentInChildren<Plot> ().gameObject.GetComponent<PolygonCollider2D>();
		plotPoints = plot.points;
		for (int i = 0; i < plot.points.Length; i++) {
			plotPoints [i] += plot.offset;
		}

		StartCoroutine (newBuild ());
	}

	void Update ()
	{
		switch (data.state) {
		case facilityState.BUILDING:
			data.timeCounter -= Time.deltaTime;

			if (data.timeCounter <= 0) {
				finishBuilding ();
			}
			break;
		case facilityState.DESTROYING:
			data.timeCounter -= Time.deltaTime;

			if (data.timeCounter <= 0) {
				finishDestroy ();
			}
			break;
		

		case facilityState.REPAIRING:
			data.timeCounter -= Time.deltaTime;

			if (data.timeCounter <= 0) {
//				finishRepair();
			}
			break;
		}
	}




//	void OnMouseOver ()
//	{
//		if (Input.GetMouseButtonDown (0) && GameManager.state == State.REGION_GAMEPLAY) {
//			unselectAll ();
//			if (!isSelected) {
//
//				detailPanel.GetComponent<DetailPanel> ().changeFacility (this);
//				isSelected = true;
////				GetComponent<SpriteRenderer> ().sprite = selected;
//				GetComponent<SpriteRenderer> ().sortingLayerName = "Selected";
//				foreach (Image i in detailPanel.GetComponentsInChildren<Image>()) {
//					if (i.name.Equals ("Icon")) {
//						i.sprite = normal;
//						break;
//					}
//				}
//				foreach (Text i in detailPanel.GetComponentsInChildren<Text>()) {
//					if (i.name.Equals ("Nama Objek")) {
//						i.text = name;
//						break;
//					}
//				}
//			}
//		}
//
//	}


//	void OnMouseDrag ()
//	{
//		if (GameManager.state == State.CHOOSE_FACILITY_PLOT) {
//			Vector3 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			pos.z = pos.y;
//			if (data.isNew ()) {
//				transform.position = pos;
//
//			}
//		}
//	}

	public void switchToPlot(bool toogleOn){
		if (!toogleOn)GetComponent<PolygonCollider2D> ().SetPath (0, defaultPoints);
		else GetComponent<PolygonCollider2D> ().SetPath (0, plotPoints);
	}

	public static void unselectAll ()
	{
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Objects")) {
			g.GetComponent<Facility> ().unselect ();
		}
	}

	public void unselect ()
	{
		
		isSelected = false;
		switch (data.state) {
		case facilityState.NORMAL:
			GetComponent<SpriteRenderer> ().sprite = normal;
			break;
		case facilityState.DESTROYING:
			GetComponent<SpriteRenderer> ().sprite = destroying;
			break;
		case facilityState.BUILDING:
			GetComponent<SpriteRenderer> ().sprite = building;
			break;
		}

		GetComponent<SpriteRenderer> ().sortingLayerName = "Object";

	}

	public bool finishPlacing ()
	{
		if (GetComponentInChildren<Plot> ().validPlace) {
			data.state = facilityState.BUILDING;
			data.timeCounter = data.buildTime;
			GetComponent<SpriteRenderer> ().sprite = building;

			GetComponentInChildren<Plot>().gameObject.layer = 0;
			data.posX = transform.position.x;
			data.posY = transform.position.y;
			transform.position = new Vector3 (data.posX, data.posY, data.posY);
			Destroy (GetComponentInChildren<Rigidbody2D> ());
			return true;
		} else {
			return false;

		}
	}

	public void finishBuilding ()
	{
		data.state = facilityState.NORMAL;
		GetComponent<SpriteRenderer> ().sprite = normal;
		StartCoroutine (afterBuild ());
	}

	public void destroy ()
	{
		if (data.state == facilityState.BUILDING) {
			data.timeCounter = 0;
		} else if (data.state != facilityState.DESTROYING) {
			data.timeCounter = data.destroyTime;
		}


		GetComponent<SpriteRenderer> ().sprite = destroying;
		
		data.state = facilityState.DESTROYING;
		FindObjectOfType<UI_Manager_Region> ().updateValues ();
	}

	public void finishDestroy ()
	{
		
		Destroy (gameObject);

		GameManager.activeTown.facilities.Remove (data);


//		StartCoroutine (afterBuild ());


	}

	public bool unlock (BigData d)
	{

		bool haveFacilities = true;

		if (facilityIdToUnlock != null) {
			if(facilityIdToUnlock.Length > 0){

			haveFacilities = false;
			int counter = 0;

			foreach (string id in facilityIdToUnlock) {
				foreach (FacilityData f in GameManager.activeTown.facilities) {

					if (f.facilityId.Equals (id)) {
						counter++;
						break;

					}
				}
				}
			

			haveFacilities = counter >=facilityIdToUnlock.Length;
			}
		}

		data.unlocked = (
			d.economic >= economicPointToUnlock
			&& d.education >= educationPointToUnlock
			&& d.crime >= crimePointToUnlock
			&& d.happiness >= happinessPointToUnlock
			&& d.health >= healthPointToUnlock
			&& d.traffic >= trafficPointToUnlock
			&& d.polution >= polutionPointToUnlock
			&& haveFacilities
		);

		return data.unlocked;
	}



	IEnumerator afterBuild ()
	{
		yield return new WaitForEndOfFrame ();
		FindObjectOfType<UI_Manager_Region> ().updateValues ();
//		gameObject.SetActive (false);
//		Destroy (facility);

	}

	IEnumerator newBuild(){

		print ("start new build");			
		Vector2 pos = Vector2.zero;
		Vector3 offset = Vector3.zero;
		bool isTouching = false;

		while(GameManager.state == State.CHOOSE_FACILITY_PLOT && data.isNew()){


#if UNITY_EDITOR


			if(Input.GetMouseButtonDown(0)){

				Vector2 touchPos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				isTouching = false;
				offset = gameObject.transform.position - new Vector3(touchPos.x,touchPos.y);

				print ("checking position");
				foreach(RaycastHit2D hit in Physics2D.RaycastAll (touchPos, Vector2.zero)){


					if(hit.collider.tag.Equals("Objects")){
						if(hit.collider.GetComponentInParent<Facility>().Equals(this)){
							print ("enable drag");
							isTouching = true;

							break;
						}
					}
				}


			}
			if(isTouching){
				pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);

					
				transform.position = new Vector3(pos.x,pos.y,pos.y)+offset;

			}
			if(Input.GetMouseButtonUp(0)){
				print("not dragging");
				isTouching = false;
			}

#elif UNITY_ANDROID
			if (Input.touchCount == 1) {



//				print ("touchcount 1");

				if (Input.GetTouch (0).phase == TouchPhase.Began) {

					Vector2 touchpos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
					RaycastHit2D[] hits = Physics2D.RaycastAll (touchpos, Vector2.zero);

					print ("checking position");
					foreach(RaycastHit2D hit in hits){
						if(hit.collider.tag.Equals("Objects")){
							if(hit.collider.GetComponentInParent<Facility>().Equals(this)){
								print ("enable drag");
								isTouching = true;
								break;
							}
						}
					}

//					isTouching = false;
				

				}



				if(Input.GetTouch(0).phase == TouchPhase.Moved && isTouching){
					pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//					print("dragging");

					transform.position = new Vector3(pos.x,pos.y,pos.y);
				}

				if(Input.GetTouch(0).phase == TouchPhase.Canceled || Input.GetTouch(0).phase == TouchPhase.Ended){
					isTouching = false;
				}



			}

#endif
			yield return null;
		}
		print ("Finish build");
	}


}


[System.Serializable]
public class FacilityData  // changeable values 
{

	public float posX, posY;
	public bool unlocked;
	public facilityState state;
	public DataCategory category;
	public facilityGroup group;
	public string name;
	public string facilityId;
	public float price;
	public float happiness;
	public float education;
	public float income;
	public float crime;
	public float health;
	public float polution;
	public float traffic;
	public float  eficiency;
	public int buildTime, destroyTime, repairtime; // in seconds
	public float timeCounter;


	public void resetValue(Facility f){
		buildTime = f.buildTime;
		destroyTime = f.destroyTime;
		repairtime = f.repairtime;
		crime = f.crime;
		education = f.education;
		eficiency = f.eficiency;
		happiness = f.happiness;
		health = f.health;
		income = f.income;
		polution = f.polution;
		price = f.price;
		traffic = f.traffic;


	}


//	public void reasign (FacilityData f)
//	{
//		this.posX = f.posX;
//		this.posY = f.posY;
//		this.state = f.state;
//		this.facilityId = f.facilityId;
//		eficiency = normalEficiency;
//	}



	public bool isNew ()
	{
		return state == facilityState.NEW;
	}
}

public enum facilityState
{
	NEW,
	BUILDING,
	NORMAL,
	BROKEN,
	REPAIRING,
	DESTROYING
}

public enum facilityGroup{
	NONE,
	TEMPATSAMPAH,
	TOKO,
	RUMAH,
	POHON,
	PERARIRAN,
	GUDANG
}