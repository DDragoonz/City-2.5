using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EventPanel : MonoBehaviour
{


	//public Facility[] facilityEconomic,facilityPolution,facilityCrime, facilityHealth,;
	public GameObject contentPanel;
	public DataCategory activeCategory;
	RectTransform rect;
	List<GameObject> events;

	Town town;
	// Use this for initialization
	void Start ()
	{

		events = new List<GameObject> ();
		rect = contentPanel.GetComponent<RectTransform> ();

		activeCategory = FindObjectOfType<UI_Manager_Region> ().getLastCategory ("event");
		town = FindObjectOfType<Town> ();


		//		facilityEconomic = town.getAllFacility (DataCategory.ECONOMIC);
		//		facilityPolution = town.getAllFacility (DataCategory.POLUTION);
		//		facilityCrime = town.getAllFacility (DataCategory.CRIME);

		populateButton ();
	}

	void OnDestroy(){
		FindObjectOfType<UI_Manager_Region> ().switchLastCategory ("event", activeCategory);
	}

	public void showEconomicEvent ()
	{
		changeCategory (DataCategory.ECONOMIC);
	}

	public void showCrimeEvent ()
	{
		changeCategory (DataCategory.CRIME);
	}

	public void showPolutionEvent ()
	{
		changeCategory (DataCategory.POLUTION);
	}

	public void showTrafficEvent ()
	{
		changeCategory (DataCategory.TRAFFIC);
	}

	public void showHealthEvent ()
	{
		changeCategory (DataCategory.HEALTH);
	}

	public void showHappinessEvent ()
	{
		changeCategory (DataCategory.HAPPINESS);
	}

	public void showEducationEvent(){
		changeCategory (DataCategory.EDUCATION);
	}


	public void changeCategory (DataCategory category)
	{
		if (activeCategory != category) {
			activeCategory = category;

			foreach (ButtonEvent f in GameObject.FindObjectsOfType<ButtonEvent>()) {

				Destroy (f.gameObject);
			}

			populateButton ();
		}

	}

	void populateButton ()
	{

		GameObject g;
		float height = 0;

		events = town.getAllEvent (activeCategory);
		foreach (RandomEvent e in GameManager.activeTown.activeEvent) {

			if (e.category == activeCategory) {
				print ("found event");
				g = Instantiate (events.Find (x => x.GetComponent<ButtonEvent> ().id.Equals(e.id))) as GameObject;
				g.transform.SetParent (contentPanel.transform, false);
				height += g.GetComponent<RectTransform> ().rect.height;
			}

		}

		rect.sizeDelta = new Vector2 (0, height * events.Count);
	}
}
