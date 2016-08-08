using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;
using System;

public class UI_Manager_World : MonoBehaviour {

	private RotateGlobe rg;
	public Text townText,deskripsi,money,happiness,education,health,polution,crime,traffic,lastPlayed;
	public GameObject[] preSelectionObjects;
	public GameObject[] townSelectionObjects;

	// Use this for initialization
	void Start () {
		GameManager.state = State.INTRO;
		rg = FindObjectOfType<RotateGlobe> ();
		townText.text = GameManager.activeTown.name;
		deskripsi.text = GameManager.activeTown.description;
		if (GameManager.activeTown.hasPlayed) {
			health.text = GameManager.activeTown.health.ToString ("F2");
			happiness.text = GameManager.activeTown.happiness.ToString ("F2");
			education.text = GameManager.activeTown.education.ToString ("F2");
			traffic.text = GameManager.activeTown.traffic.ToString ("F2");
			crime.text = GameManager.activeTown.crime.ToString ("F2");
			polution.text = GameManager.activeTown.polution.ToString ("F2");
			if (PlayerPrefs.HasKey ("time")) {
				long temp = Convert.ToInt64 (PlayerPrefs.GetString ("time"));
				DateTime old = DateTime.FromBinary (temp);
				lastPlayed.text = "Last Played : " + old.ToString();
			}
		}

	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.state == State.INTRO && Input.GetMouseButtonDown (0)) {
			selectTownActive ();
		}

	}

	public void nextTown(){
		
		rg.currentIndex = rg.currentIndex+1 > rg.towns.Length-1 ? 0 : rg.currentIndex+1;
		GameManager.activeTown = GameManager.Towns.Find (x => x.id == rg.currentIndex);
		townText.text = GameManager.activeTown.name;
		deskripsi.text = GameManager.activeTown.description;
		money.text = GameManager.activeTown.money.ToString("F2");
		if (GameManager.activeTown.hasPlayed) {
			health.text = GameManager.activeTown.health.ToString ("F2");
			happiness.text = GameManager.activeTown.happiness.ToString ("F2");
			education.text = GameManager.activeTown.education.ToString ("F2");
			traffic.text = GameManager.activeTown.traffic.ToString ("F2");
			crime.text = GameManager.activeTown.crime.ToString ("F2");
			polution.text = GameManager.activeTown.polution.ToString ("F2");
		}
	}

	public void prevTown(){
		rg.currentIndex = rg.currentIndex-1 < 0 ? rg.towns.Length-1 : rg.currentIndex-1;
		GameManager.activeTown = GameManager.Towns.Find (x => x.id == rg.currentIndex);
		townText.text = GameManager.activeTown.name;
		deskripsi.text = GameManager.activeTown.description;
		if (GameManager.activeTown.hasPlayed) {
			money.text = GameManager.activeTown.money.ToString ("F2");
			health.text = GameManager.activeTown.health.ToString ("F2");
			happiness.text = GameManager.activeTown.happiness.ToString ("F2");
			education.text = GameManager.activeTown.education.ToString ("F2");
			traffic.text = GameManager.activeTown.traffic.ToString ("F2");
			crime.text = GameManager.activeTown.crime.ToString ("F2");
			polution.text = GameManager.activeTown.polution.ToString ("F2");
		}
	}

	public void selectTownActive(){
		foreach (GameObject g in preSelectionObjects) {
			g.SetActive(false);
		}
		foreach (GameObject g in townSelectionObjects) {
			g.SetActive(true);
		}

		GameManager.state = State.SELECT_TOWN;
	}

	public void loadRegionScene(){
//		GameManager.activeTown = GameManager.userdata.towns [rg.currentIndex];
		SceneManager.LoadSceneAsync ("Loading Screen Town");
//		StartCoroutine (loadRegion ());
	}


}
