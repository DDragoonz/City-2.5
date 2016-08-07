using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class ButtonEvent : MonoBehaviour
{

	public string id;
	public RandomEvent rEvent;
	public Text timeCounter;


	void Start(){
		rEvent = GameManager.activeTown.activeEvent.Find (x => x.id.Equals (id));
	}

	void Update(){
		if (timeCounter) {
			TimeSpan time = TimeSpan.FromSeconds (rEvent.timeCounter);
			timeCounter.text = string.Format ("{0:D2}:{1:D2}:{2:D2}", time.Hours,time.Minutes,time.Seconds);

		}
		if (EventHandler.resolve(rEvent)) {
			Destroy (this.gameObject);
		}
	}

	//functions bellow are all event resolving method. apply to button
}


[System.Serializable]

public class RandomEvent
{

	public string id;
	public DataCategory category;
	public bool isActive;
	public float GHappiness, GCrime, GPolution, GEducation, GTraffic, GEconomic, GHealth;
	public float LHappiness, LCrime, LPolution, LEducation, LTraffic, LEconomic, LHealth;
	public float efficiencyModifier;
	public List<string> facilityId;
	public float duration, timeCounter;

	public RandomEvent ()
	{
		isActive = false;
		GHappiness = GCrime = GPolution = GEducation = GTraffic = GEconomic = GHealth=
		LHappiness = LCrime = LPolution = LEducation = LTraffic = LEconomic = LHealth=
		efficiencyModifier = duration = timeCounter = 0;
		facilityId = new List<string> ();
	}

	public RandomEvent clone(){
		return (RandomEvent)this.MemberwiseClone ();
	}


	public void invokeEvent ()
	{
		foreach (FacilityData f in GameManager.activeTown.facilities) {
			foreach (string id in facilityId) {
				if (f.facilityId.Equals (id)) {
					Debug.Log ("update "+f.name);
					f.happiness += LHappiness;
					f.health += LHealth;
					f.income += LEconomic;
					f.education += LEducation;
					f.crime += LCrime;
					f.traffic += LTraffic;
					f.polution += LPolution;
					f.eficiency += efficiencyModifier;
				}
			}
		}

	}

	public void eventFinish(){
		isActive = false;



		List<SosMed> removedSosmed = new List<SosMed> ();

		foreach(SosMed s in GameManager.allSosmed){
			if (s.eventId.Equals (id))
				removedSosmed.Add (s);
		}

		foreach (SosMed s in removedSosmed) {
			GameManager.allSosmed.Remove (s);
		}


	}
}