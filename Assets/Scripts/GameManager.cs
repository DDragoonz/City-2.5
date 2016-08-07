using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class GameManager {
	

	public static State state;
	public static List<TownData> Towns;
	public static TownData activeTown;

	public static List<SosMed> allSosmed;

//	public static RandomEvent[] polution1Event;
//	public static RandomEvent[] polution2Event;

	public static void save(){
		if (GameManager.activeTown!=null) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Create (Application.persistentDataPath + "/SaveGame.sav");
			bf.Serialize (file, Towns);
			file.Close ();
			PlayerPrefs.SetString ("time", System.DateTime.Now.ToBinary ().ToString ());
		}
	}

	public static bool load(List<TownData> defaultTowns){
		if (File.Exists (Application.persistentDataPath + "/SaveGame.sav")) {
			BinaryFormatter bf = new BinaryFormatter ();
			FileStream file = File.Open (Application.persistentDataPath + "/SaveGame.sav", FileMode.Open);

			Towns = (List<TownData>)bf.Deserialize (file);
			
			file.Close ();
			activeTown = Towns.Find (x => x.id == 0);
			foreach (TownData t in Towns) {
				if (t.hasPlayed) {
					t.initUpdate ();
				}
			}
			Debug.Log ("load success");
			return true;
		} else{
			Debug.Log ("load fail");
			Towns = defaultTowns;

			int idx = 0;
			foreach (TownData t in Towns) {
				t.id = idx++;

			}

			activeTown = Towns.Find (x => x.id == 0);
			return false;
		}
	}



	public static double timeDifferenceInMinutes(){
		if (PlayerPrefs.HasKey ("time")) {
			long temp = Convert.ToInt64 (PlayerPrefs.GetString ("time"));
			DateTime old = DateTime.FromBinary (temp);

			TimeSpan timeDifference;
			timeDifference = System.DateTime.Now.Subtract (old);

			return timeDifference.TotalMinutes;
		} else
			return 0;
	}

	public static double timeDifferenceInSeconds(){
		if (PlayerPrefs.HasKey ("time")) {
			long temp = Convert.ToInt64 (PlayerPrefs.GetString ("time"));
			DateTime old = DateTime.FromBinary (temp);

			TimeSpan timeDifference;
			timeDifference = System.DateTime.Now.Subtract (old);

			return timeDifference.TotalSeconds;
		}else
			return 0;
	}

}

public enum State{

	INTRO, // intro, showing the whole world (globe)
	SELECT_TOWN, // after intro, selecting town

	REGION_GAMEPLAY, // normal region gameplay
	BUILD_FACILITY_MENU, // show facility build menu
	CHOOSE_FACILITY_PLOT, // choosing plot for facility
//	CONFIRM_FACILITY_PLOT, // wait to confirm facility built

	BIG_DATA_MENU,
	EVENT_MENU,
	CHOOSE_SOSIALIZE_PLACE
}

