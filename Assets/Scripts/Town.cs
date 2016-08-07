using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Town:MonoBehaviour
{

	public GameObject[] AllFacilities;
	public GameObject[] AllEvents;

	public int currentFacilityID;

	void Awake ()
	{



		if (!GameManager.activeTown.hasPlayed) {
			// kalau pertama kali main town ini (belum di otak atik)
			// simpan data town sementara ke game manager
			GameManager.activeTown.facilities.Clear ();
			foreach (Facility f in GameObject.FindObjectsOfType<Facility>()) {
				f.data.resetValue (f);
				GameManager.activeTown.facilities.Add (f.data);
			}

			GameManager.activeTown.hasPlayed = true;
		} else {

			GameObject newFacility;

			foreach (Facility f in GameObject.FindObjectsOfType<Facility>()) {
				Destroy (f.gameObject);
			}

			foreach (FacilityData f in GameManager.activeTown.facilities) {
				

				newFacility = Instantiate (findFacilities (f.facilityId)) as GameObject;
				f.resetValue (newFacility.GetComponent<Facility> ());
//				newFacility.GetComponent<Facility> ().data = f;
				newFacility.GetComponent<Facility> ().init (f);

//				newFacility.transform.position = new Vector3(f.posX,f.posY,f.posY);
			}

			foreach (RandomEvent e in GameManager.activeTown.activeEvent) {
				e.invokeEvent ();
			}


		}


	}
	//
	void OnApplicationQuit ()
	{
		GameManager.save ();


	}

	void OnApplicationPause ()
	{ // for android because it can't call onAppQuit()
		GameManager.save ();


	}



	public GameObject findFacilities (string id)
	{
		foreach (GameObject g in AllFacilities) {
			if (g.GetComponent<Facility> ().data.facilityId.Equals (id))
				return g;
		}
		return null;
	}

	public Facility[] getAllFacility (DataCategory category)
	{
		List<Facility> f = new List<Facility> ();
		foreach (GameObject g in AllFacilities) {
			if (g.GetComponent<Facility> ().data.category == category) {
				if (g.GetComponent<Facility> ().unlock (GameManager.activeTown.bigData))
					f.Add (g.GetComponent<Facility> ());

			}
		}
		return f.ToArray ();
	}

	public void generateRandomEvent ()
	{
		int rand = Random.Range (0, AllEvents.Length *  Mathf.Clamp(GameManager.activeTown.activeEvent.Count,1,10)); // semakin banyak event aktif, semakin kecil chance generate event

		if (rand < AllEvents.Length) {
			RandomEvent newEvent = AllEvents [rand].GetComponent<ButtonEvent> ().rEvent.clone();

			bool canAdd = EventHandler.eventValid (newEvent); //check prerequires first
			if (canAdd) {
				foreach (RandomEvent e in GameManager.activeTown.activeEvent) {

					if (e.id.Equals (newEvent.id))
						canAdd = false; 

				}
			}
			if (canAdd) { // check if the event not active
				print (newEvent.id + " active !");
				newEvent.isActive = true;
				newEvent.timeCounter = newEvent.duration;
				GameManager.activeTown.activeEvent.Add (newEvent);
				SosmedGenerator.generateSpecialSosmed (newEvent.id);
				FindObjectOfType<UI_Manager_Region> ().updateValues ();
			}
		}
	}

	public List<GameObject> getAllEvent (DataCategory category)
	{
		List<GameObject> f = new List<GameObject> ();
		foreach (RandomEvent e in GameManager.activeTown.activeEvent) {
			foreach (GameObject g in AllEvents) {
				if (g.GetComponent<ButtonEvent> ().rEvent.category == category) {
					if (g.GetComponent<ButtonEvent> ().id.Equals (e.id)) {
						f.Add (g);
						break;
					}
				}
			}
		}
		return f;
	}
	public GameObject findEvent (string id)
	{
		foreach (GameObject g in AllEvents) {
			if (g.GetComponent<ButtonEvent> ().id.Equals (id))
				return g;
		}
		return null;
	}


}


[System.Serializable]
public class TownData
{

	public string name;
	public string description;
	public int id;
	public bool unlocked;
	public bool hasPlayed;
	public BigData bigData;

	public float money, income, crime, health, education, polution, traffic, happiness;


	public List<FacilityData> facilities;
	public List<RandomEvent> activeEvent;



	public void eventCountDown(){

		List<RandomEvent> re = new List<RandomEvent> ();

		foreach (RandomEvent e in activeEvent) {
			if (EventHandler.resolve(e)) {
				e.eventFinish();
				re.Add (e);

			} 
		}
		foreach (RandomEvent e in re) {
			activeEvent.Remove (e);
		}
	}

	public void updateData ()
	{

		happiness = income = crime = health = education = polution = traffic = 0;

		Facility f;
		Town t = GameObject.FindObjectOfType<Town> ();

		foreach(FacilityData fd in GameManager.activeTown.facilities){
			fd.resetValue( t.findFacilities (fd.facilityId).GetComponent<Facility>());
		}

		foreach (RandomEvent e in activeEvent) {
			income += e.GEconomic;
			crime += e.GCrime;
			health += e.GHealth;
			polution += e.GPolution;
			education += e.GEducation;
			traffic += e.GTraffic;
			happiness += e.GHappiness;

			e.invokeEvent ();
		}


		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Objects")) {
			

			f = g.GetComponent<Facility> ();

			if (f.data.state == facilityState.NORMAL) {
				income += f.data.category == DataCategory.ECONOMIC ? f.data.income * f.data.eficiency / 100 : f.data.income;
				crime += f.data.category == DataCategory.CRIME ? f.data.crime * f.data.eficiency / 100 : f.data.crime;
				health += f.data.category == DataCategory.HEALTH ? f.data.health * f.data.eficiency / 100 : f.data.health;
				polution += f.data.category == DataCategory.POLUTION ? f.data.polution * f.data.eficiency / 100 : f.data.polution;
				traffic += f.data.category == DataCategory.TRAFFIC ? f.data.traffic * f.data.eficiency / 100 : f.data.traffic;
				education += f.data.category == DataCategory.EDUCATION ? f.data.education * f.data.eficiency / 100 : f.data.education;
				happiness += f.data.category == DataCategory.HAPPINESS ? f.data.happiness * f.data.eficiency / 100 : f.data.happiness;
			}
		}


	}

	public void initUpdate ()
	{
		money += income * (int)GameManager.timeDifferenceInMinutes ();
		foreach (FacilityData f in facilities) {
			if (f.timeCounter > 0) {
				f.timeCounter -= (float)GameManager.timeDifferenceInSeconds ();
			}
		}
		foreach (RandomEvent e in activeEvent) {
			if (e.timeCounter > 0) {
				e.timeCounter -= (float)GameManager.timeDifferenceInSeconds ();

			}
			EventHandler.resolve (e);
		}


		Debug.Log ("income" + income + "*" + GameManager.timeDifferenceInMinutes ());
		Debug.Log (name + " " + money);
	}

}