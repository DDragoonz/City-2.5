using UnityEngine;
using System.Collections;

public class BuildPanel : MonoBehaviour {

	public GameObject facilityButton;
	//public Facility[] facilityEconomic,facilityPolution,facilityCrime, facilityHealth,;
	public GameObject contentPanel;
	public DataCategory activeCategory;
	RectTransform rect;
	Facility[] facilities;

	Town town;
	// Use this for initialization
	void Start () {

		rect = contentPanel.GetComponent<RectTransform> ();

		activeCategory = FindObjectOfType<UI_Manager_Region> ().getLastCategory ("build");
		town = FindObjectOfType<Town> ();


//		facilityEconomic = town.getAllFacility (DataCategory.ECONOMIC);
//		facilityPolution = town.getAllFacility (DataCategory.POLUTION);
//		facilityCrime = town.getAllFacility (DataCategory.CRIME);

		populateButton ();
	}

	void OnDestroy(){
		FindObjectOfType<UI_Manager_Region> ().switchLastCategory ("build", activeCategory);
	}

	public void showEconomicFacilities ()
	{
		changeCategory (DataCategory.ECONOMIC);
	}

	public void showCrimeFacilities ()
	{
		changeCategory (DataCategory.CRIME);
	}

	public void showPolutionFacilities ()
	{
		changeCategory (DataCategory.POLUTION);
	}

	public void showTrafficFacilities ()
	{
		changeCategory (DataCategory.TRAFFIC);
	}

	public void showHealthFacilities ()
	{
		changeCategory (DataCategory.HEALTH);
	}

	public void showHappinessFacilities ()
	{
		changeCategory (DataCategory.HAPPINESS);
	}

	public void showEducationFacilities(){
		changeCategory (DataCategory.EDUCATION);
	}


	public void changeCategory(DataCategory category){
		if (activeCategory != category) {
			activeCategory = category;

			foreach (FacilityBuildButton f in GameObject.FindObjectsOfType<FacilityBuildButton>()) {
			
				Destroy (f.gameObject);
			}

			populateButton ();
		}

	}

	void populateButton(){

		GameObject g;

		facilities = town.getAllFacility(activeCategory);
		foreach (Facility f in facilities) {
			g = Instantiate (facilityButton) as GameObject;
			g.transform.SetParent (contentPanel.transform, false);
			g.GetComponentInChildren<FacilityBuildButton> ().assignValue (f);
		}

		rect.sizeDelta = new Vector2(0,   facilityButton.GetComponent<RectTransform> ().rect.height * facilities.Length);

//		switch (activeCategory) {
//		case DataCategory.ECONOMIC:
//			
//			foreach (Facility f in town.getAllFacility (DataCategory.ECONOMIC)) {
//				g = Instantiate (facilityButton) as GameObject;
//				g.transform.SetParent (contentPanel.transform, false);
//				g.GetComponentInChildren<FacilityBuildButton> ().assignValue (f);
//			}
//
//			rect.sizeDelta = new Vector2(0,   facilityButton.GetComponent<RectTransform> ().rect.height * facilityEconomic.Length);
//
//			break;
//		case 2:
//
//			foreach (Facility f in town.getAllFacility (DataCategory.POLUTION)) {
//				g = Instantiate (facilityButton) as GameObject;
//				g.transform.SetParent (contentPanel.transform, false);
//				g.GetComponentInChildren<FacilityBuildButton> ().assignValue (f);
//			}
//			rect.sizeDelta = new Vector2(0,   facilityButton.GetComponent<RectTransform> ().rect.height * facilityPolution.Length);
//
//			break;
//		case 3:
//
//			foreach (Facility f in town.getAllFacility (DataCategory.CRIME)) {
//				g = Instantiate (facilityButton) as GameObject;
//				g.transform.SetParent (contentPanel.transform, false);
//				g.GetComponentInChildren<FacilityBuildButton> ().assignValue (f);
//			}
//
//			rect.sizeDelta = new Vector2(0,   facilityButton.GetComponent<RectTransform> ().rect.height * facilityCrime.Length);
//			break;
//		}
	}
}
