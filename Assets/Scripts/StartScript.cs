using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;

public class StartScript : MonoBehaviour {

	public List<TownData> towns;
//	public RandomEvent[] commonEvent;


	// Use this for initialization
	void Start () {




//		GameManager.commonEvent = commonEvent;

		GameManager.load (towns);

		SosmedGenerator.generateCommonSosmed ();

			
		StartCoroutine (loadLevelSelect ());
	}
	


	IEnumerator loadLevelSelect(){
		//yield return new WaitForSeconds (.5f);

		AsyncOperation async = SceneManager.LoadSceneAsync ("Town Select");

		while (!async.isDone) {
			yield return null;
		}
	}






//	RandomEvent[] commonEvent(){
//
//		List<Event> events = new List<Event> ();
//		Event e;
//
//		e = new Event ();
//		e.category = DataCategory.POLUTION;
//		e.efficiencyModifier = 50;
//		e.facilityId.Add ("sampah1");
//
//
//
//
//
//		return events.ToArray ();
//
//
//	}
}
