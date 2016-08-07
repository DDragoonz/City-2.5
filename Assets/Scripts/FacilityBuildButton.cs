using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FacilityBuildButton : MonoBehaviour {

	public GameObject confirm;
	public Image icon;
	public Text facilityname,happyText,CrimeText,EduText,trafficText,PoluteText,healthText,priceText,incomeText;
	string facilityId;
//	int category;

	Town region;

	// Use this for initialization
	void Start () {
		region = FindObjectOfType<Town> ();
	}


	public void buildFacility(){

//		region.currentFacilityID = facilityId;

		//		foreach (GameObject g in GameObject.FindGameObjectsWithTag("Plot")) {
		//
		//			if (g.GetComponent<Plot> ().category == category) {
		//				g.GetComponent<Plot> ().showPlot ();
		//
		//			}
		//		}
		GameObject newFacilities = region.findFacilities(facilityId);
		newFacilities.GetComponent<Facility> ().data.state = facilityState.NEW;
		GameObject a = Instantiate (newFacilities) as GameObject;
		Vector3 newpos = Camera.main.ScreenToWorldPoint (new Vector3(Screen.width/2,Screen.height/2,Camera.main.nearClipPlane));
		newpos.z = newpos.y;
		a.transform.position = newpos;
		if (FindObjectOfType<UI_Manager_Region> ().togle.isOn) {
			a.GetComponent<SpriteRenderer> ().enabled = false;
			a.GetComponentInChildren<Plot> ().switchPlot (true);
		}
		Instantiate (confirm).transform.SetParent (a.transform,false);


		Destroy (GameObject.FindGameObjectWithTag("Menu"));

		GameManager.state = State.CHOOSE_FACILITY_PLOT;
	}

	public void assignValue(Facility f){

//		category = f.data.category;
		facilityname.text = f.data.name;
		icon.sprite = f.normal;
		facilityId = f.data.facilityId;
		happyText.text = ""+f.data.happiness;
		CrimeText.text = ""+f.data.crime;
		EduText.text = ""+f.data.education;
		trafficText.text = ""+f.data.traffic;
		PoluteText.text = ""+f.data.polution;
		priceText.text = ""+f.data.price;
		incomeText.text = ""+ f.data.income;
		healthText.text = "" + f.data.health;
	}
}

