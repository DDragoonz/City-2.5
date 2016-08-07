using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Manager_Region : MonoBehaviour
{

	public GameObject buildPanel, detailPanel, dataMinePanel, eventPanel;
	public Toggle togle;


	public Text moneyText, incomeText, happyText, crimeText, 
	healthText, eduText, polutionText, trafficText, sosmedText;


	bool minuteChanging;
	Town town;

	DataCategory lastBuildCategory,lastEventCategory;

	// Use this for initialization
	void Start ()
	{
		town = FindObjectOfType<Town> ();
		GameManager.state = State.REGION_GAMEPLAY;
		StartCoroutine (initUpdate ());
		moneyText.text = GameManager.activeTown.money.ToString("F2");
		sosmedText.text = "";
	}

	void Update ()
	{
		GameManager.activeTown.eventCountDown ();


		if (System.DateTime.Now.Second % 10 == 0) {
			if (!minuteChanging) {
				minuteChanging = true;

				town.generateRandomEvent ();

				if (System.DateTime.Now.Second == 0) {
					GameManager.activeTown.money += GameManager.activeTown.income;
					moneyText.text =  GameManager.activeTown.money.ToString("F2");
				}

				int idx = Random.Range (0, GameManager.allSosmed.Count-1);
				sosmedText.text = GameManager.allSosmed [Random.Range (0, GameManager.allSosmed.Count-1)].from
				+ " : " + GameManager.allSosmed [idx].message;
				GameManager.activeTown.bigData.progress (GameManager.allSosmed [idx].category, 1);

				GameManager.save ();
				print ("update");
				foreach (RandomEvent e in GameManager.activeTown.activeEvent) {
					print (e.id + " " + e.timeCounter);
				}
			} 
		} else
			minuteChanging = false;
	}

	public void updateValues ()
	{
		GameManager.activeTown.updateData ();


		incomeText.text = GameManager.activeTown.income >= 0 ? 
			("+ " + GameManager.activeTown.income.ToString("F2") + " / mnt") :
			(GameManager.activeTown.income.ToString("F2") + " / mnt");


		crimeText.text =  GameManager.activeTown.crime.ToString("F2");
		healthText.text =  GameManager.activeTown.health.ToString("F2");
		happyText.text =  GameManager.activeTown.happiness.ToString("F2");
		eduText.text =  GameManager.activeTown.education.ToString("F2");
		polutionText.text =  GameManager.activeTown.polution.ToString("F2");
		trafficText.text =  GameManager.activeTown.traffic.ToString("F2");
		moneyText.text =  GameManager.activeTown.money.ToString("F2");
	}

	public void switchLastCategory(string menu, DataCategory category){
		switch(menu){
		case "event":
			lastEventCategory = category;
			break;
		case "build":
			lastBuildCategory = category;
			break;
		default:
			break;
		}
	}

	public DataCategory getLastCategory(string menu){
		switch(menu){
		case "event":
			return lastEventCategory;

		case "build":
			return lastBuildCategory;

		default:
			return DataCategory.POLUTION;
		}
	}

	public void showAllFacility ()
	{

		switch (GameManager.state) {
		case State.REGION_GAMEPLAY:
			Instantiate (buildPanel).transform.SetParent (transform, false);
			GameManager.state = State.BUILD_FACILITY_MENU;
			detailPanel.SetActive (false);
			break;
		case State.BUILD_FACILITY_MENU:
			Destroy (GameObject.FindGameObjectWithTag ("Menu"));
			GameManager.state = State.REGION_GAMEPLAY;
			break;
		}
	}

	public void showBigData ()
	{
		switch (GameManager.state) {
		case State.REGION_GAMEPLAY:
			Instantiate (dataMinePanel).transform.SetParent (transform, false);
			GameManager.state = State.BIG_DATA_MENU;
			detailPanel.SetActive (false);
			break;
		case State.BIG_DATA_MENU:
			Destroy (GameObject.FindGameObjectWithTag ("Menu"));
			GameManager.state = State.REGION_GAMEPLAY;
			break;
		}
	}
	public void showEvent(){
		switch (GameManager.state) {
		case State.REGION_GAMEPLAY:
			detailPanel.SetActive (false);
			Instantiate (eventPanel).transform.SetParent (transform, false);
			GameManager.state = State.EVENT_MENU;
			break;
		case State.EVENT_MENU:
			Destroy (GameObject.FindGameObjectWithTag ("Menu"));
			GameManager.state = State.REGION_GAMEPLAY;
			break;
		}
	}

	public void switchToPlot ()
	{
		
		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Objects")) {
			g.GetComponent<Facility> ().switchToPlot (togle.isOn);
			g.GetComponent<SpriteRenderer> ().enabled = !togle.isOn;
			g.GetComponentInChildren<Plot> ().switchPlot (togle.isOn);
		}

	}

	IEnumerator initUpdate ()
	{
		yield return new WaitForEndOfFrame ();
		updateValues ();
	}
}
