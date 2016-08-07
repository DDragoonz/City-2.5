using UnityEngine;
using System.Collections;

public class EventHandler {

	public static bool eventValid(RandomEvent e){

		Debug.Log ("will checking " + e.id);

		switch (e.id) {
		case  "sampah1":

			return checkFacilitiy (e);

		case "tokobuah1":
			
			return checkFacilitiy (e);

		case "penghijauan1":

			return !facilityGroupExist (facilityGroup.POHON, 5);

		default : 
			Debug.Log ("event invalid");
			return false;
		}

	}

	public static bool resolve(RandomEvent e){
		switch (e.id) {
		case  "sampah1":

			return durationEnd (e);

		case "tokobuah1":

			return durationEnd (e);

		case "penghijauan1":

			return facilityGroupExist (facilityGroup.POHON, 5);


		default : 
			Debug.Log ("no such event");
			return false;
		}
	}

	private static bool durationEnd(RandomEvent e){
		if (e.timeCounter > 0) {
			e.timeCounter -= Time.deltaTime;

		}
		return e.timeCounter <= 0;
	}

	private static bool checkFacilitiy(RandomEvent e){



		foreach (string id in e.facilityId) {
			FacilityData fd;

			fd = GameManager.activeTown.facilities.Find (x => x.facilityId.Equals (id));
			if (fd!=null) {
				Debug.Log (id+" found! event will activated");
				return true;
			}
		}
		Debug.Log(e.id+" no facility found! event will not activated");
		return false;
	}
	private static bool facilityExist(string facilityId, int n){
		int count = 0;
		foreach(FacilityData f in GameManager.activeTown.facilities){
			if (f.facilityId.Equals (facilityId))
				count++;
		}
		return count >= n;
	}

	private static bool facilityGroupExist(facilityGroup group, int n){
		int count = 0;
		foreach(FacilityData f in GameManager.activeTown.facilities){
			if (f.group == group)
				count++;
		}
		return count >= n;
	}



}
