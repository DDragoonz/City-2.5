using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DetailPanel : MonoBehaviour {

	public Facility facility;
	public Image icon;
	public Text facilityname,happyText,CrimeText,EduText,trafficText,PoluteText,healthText,incomeText,efficiencyText,keteranganText;

	public void changeFacility(Facility f){
		facility = f;
		gameObject.SetActive (true);
		facilityname.text = f.data.name;
		icon.sprite = f.normal;
		happyText.text = f.data.happiness.ToString("F1");
		CrimeText.text = f.data.crime.ToString("F1");
		EduText.text = f.data.education.ToString("F1");
		trafficText.text = f.data.traffic.ToString("F1");
		PoluteText.text = f.data.polution.ToString("F1");
		incomeText.text =  f.data.income.ToString("F1");
		healthText.text = f.data.health.ToString("F1");
		efficiencyText.text = "Efficiency : " + f.data.eficiency+" %";
		keteranganText.text = f.keterangan;
	}

	public void destroyFacility(){

		facility.destroy ();



	}



}
