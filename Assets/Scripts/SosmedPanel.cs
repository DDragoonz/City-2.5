using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SosmedPanel : MonoBehaviour {

	public Image economicBar, healthBar, crimeBar, polutionBar, educationBar, trafficBar, happinessBar;
	public Text economicText, healthText, crimeText, polutionText, educationText, trafficText, happinessText;

	BigData bigdata;

	// Use this for initialization
	void Start () {
		bigdata = GameManager.activeTown.bigData;
	}
	
	// Update is called once per frame
	void Update () {

		for (int i = 0; i < bigdata.level.Length; i++) {
			if (bigdata.economic > bigdata.level [i]) {
				
				if ((i + 1) < bigdata.level.Length) {
					economicBar.fillAmount = bigdata.economic / bigdata.level [i + 1];
					economicText.text = "Keuangan Lv " + (i + 1) + " (" + bigdata.economic + "/" + bigdata.level [i + 1] + ")";
				} else {
					economicBar.fillAmount = 1;
					economicText.text = "Keuangan Lv max";
				}
			}

			if (bigdata.health > bigdata.level [i]) {
				
				if ((i + 1) < bigdata.level.Length) {
					healthBar.fillAmount = bigdata.health / bigdata.level [i + 1];
					healthText.text = "Kesehatan Lv " + (i + 1) + " (" + bigdata.health + "/" + bigdata.level [i + 1] + ")";
				} else {
					healthBar.fillAmount =1;
					healthText.text = "Kesehatan Lv max";
				}
			}

			if (bigdata.happiness > bigdata.level [i]) {
				
				if ((i + 1) < bigdata.level.Length) {
					happinessBar.fillAmount = bigdata.happiness / bigdata.level [i + 1];
					happinessText.text = "Kesenanagan Lv " + (i + 1) + " (" + bigdata.happiness + "/" + bigdata.level [i + 1] + ")";
				} else {
					happinessBar.fillAmount = 1;	
					happinessText.text = "Kesenanagan Lv Max";
				}
			}

			if (bigdata.crime > bigdata.level [i]) {
				

				if ((i + 1) < bigdata.level.Length) {
					crimeBar.fillAmount = bigdata.crime / bigdata.level [i + 1];
					crimeText.text = "Keamanan Lv " + (i + 1) + " (" + bigdata.crime + "/" + bigdata.level [i + 1] + ")";
				} else {
					crimeBar.fillAmount = 1;
					crimeText.text = "Keamanan Lv max";
				}
			}

			if (bigdata.education > bigdata.level [i]) {
				
				if ((i + 1) < bigdata.level.Length) {
					educationBar.fillAmount = bigdata.education / bigdata.level [i + 1];
					educationText.text = "Pendidikan Lv " + (i + 1) + " (" + bigdata.education + "/" + bigdata.level [i + 1] + ")";
				} else {
					educationBar.fillAmount = 1;
					educationText.text = "Pendidikan Lv max";
				}
			}

			if (bigdata.traffic > bigdata.level [i]) {
				
				if ((i + 1) < bigdata.level.Length) {
					trafficBar.fillAmount = bigdata.traffic / bigdata.level [i + 1];
					trafficText.text = "Kemacetan Lv " + (i + 1) + " (" + bigdata.traffic + "/" + bigdata.level [i + 1] + ")";
				} else {
					trafficBar.fillAmount = 1;
					trafficText.text = "Kemacetan Lv max";
				}
			}

			if (bigdata.polution > bigdata.level [i]) {
				
				if ((i + 1) < bigdata.level.Length) {
					polutionBar.fillAmount = bigdata.polution / bigdata.level [i + 1];
					polutionText.text = "Kebershian Lv " + (i + 1) + " (" + bigdata.polution + "/" + bigdata.level [i + 1] + ")";
				} else {
					polutionBar.fillAmount = 1;
					polutionText.text = "Kebershian Lv max";
				}
			}
		}

//		if (bigdata.economic < bigdata.lv2) {
//			economicBar.fillAmount = bigdata.economic / bigdata.lv2;
//			economicText.text = "Keuangan Lv 1 (" + bigdata.economic + "/" + bigdata.lv2 + ")";
//		} else if (bigdata.economic > bigdata.lv2 && bigdata.economic < bigdata.lv3) {
//			economicBar.fillAmount = bigdata.economic / bigdata.lv3 ;
//			economicText.text = "Keuangan Lv 2 (" + bigdata.economic + "/" + bigdata.lv3 + ")";
//		} else {
//			economicBar.fillAmount = 1;
//			economicText.text = "Keuangan Lv 3 (MAX)";
//		}
//
//		if (bigdata.health < bigdata.lv2) {
//			healthBar.fillAmount = bigdata.health / bigdata.lv2 ;
//			healthText.text = "Kesehatan Lv 1 (" + bigdata.health + "/" + bigdata.lv2 + ")";
//		} else if (bigdata.health > bigdata.lv2 && bigdata.health < bigdata.lv3) {
//			healthBar.fillAmount = bigdata.health / bigdata.lv3 ;
//			healthText.text = "Kesehatan Lv 2 (" + bigdata.health + "/" + bigdata.lv3 + ")";
//		} else {
//			healthBar.fillAmount = 1;
//			healthText.text = "Kesehatan Lv 3 (MAX)";
//		}
//
//		if (bigdata.crime < bigdata.lv2) {
//			crimeBar.fillAmount = bigdata.crime / bigdata.lv2 ;
//			crimeText.text = "Kejahatan Lv 1 (" + bigdata.crime + "/" + bigdata.lv2 + ")";
//		} else if (bigdata.crime > bigdata.lv2 && bigdata.crime < bigdata.lv3) {
//			crimeBar.fillAmount = bigdata.crime / bigdata.lv3;
//			crimeText.text = "Kejahatan Lv 2 (" + bigdata.crime + "/" + bigdata.lv3 + ")";
//		} else {
//			crimeBar.fillAmount = 1;
//			crimeText.text = "Kejahatan Lv 3 (MAX)";
//		}
//
//		if (bigdata.polution < bigdata.lv2) {
//			polutionBar.fillAmount = bigdata.polution / bigdata.lv2 ;
//			polutionText.text = "Polusi Lv 1 (" + bigdata.polution + "/" + bigdata.lv2 + ")";
//		} else if (bigdata.polution > bigdata.lv2 && bigdata.polution < bigdata.lv3) {
//			polutionBar.fillAmount = bigdata.polution / bigdata.lv3 ;
//			polutionText.text = "Polusi Lv 2 (" + bigdata.polution + "/" + bigdata.lv3 + ")";
//		} else {
//			polutionBar.fillAmount = 1;
//			polutionText.text = "Polusi Lv 3 (MAX)";
//		}
//
//		if (bigdata.education < bigdata.lv2) {
//			educationBar.fillAmount = bigdata.education / bigdata.lv2;
//			educationText.text = "Pendidikan Lv 1 (" + bigdata.education + "/" + bigdata.lv2 + ")";
//		} else if (bigdata.education > bigdata.lv2 && bigdata.education < bigdata.lv3) {
//			educationBar.fillAmount = bigdata.education / bigdata.lv3 ;
//			educationText.text = "Pendidikan Lv 2 (" + bigdata.education + "/" + bigdata.lv3 + ")";
//		} else {
//			educationBar.fillAmount = 1;
//			educationText.text = "Pendidikan Lv 3 (MAX)";
//		}
//
//		if (bigdata.traffic < bigdata.lv2) {
//			trafficBar.fillAmount = bigdata.traffic / bigdata.lv2 ;
//			trafficText.text = "Kemacetan Lv 1 (" + bigdata.traffic + "/" + bigdata.lv2 + ")";
//		} else if (bigdata.traffic > bigdata.lv2 && bigdata.traffic < bigdata.lv3) {
//			trafficBar.fillAmount = bigdata.traffic / bigdata.lv3 ;
//			trafficText.text = "Kemacetan Lv 2 (" + bigdata.traffic + "/" + bigdata.lv3 + ")";
//		} else {
//			trafficBar.fillAmount = 1;
//			trafficText.text = "Kemacetan Lv 3 (MAX)";
//		}
//
//		if (bigdata.happiness < bigdata.lv2) {
//			happinessBar.fillAmount = bigdata.happiness / bigdata.lv2 ;
//			happinessText.text = "Hiburan Lv 1 (" + bigdata.happiness + "/" + bigdata.lv2 + ")";
//		} else if (bigdata.happiness > bigdata.lv2 && bigdata.happiness < bigdata.lv3) {
//			happinessBar.fillAmount = bigdata.happiness / bigdata.lv3 ;
//			happinessText.text = "Hiburan Lv 2 (" + bigdata.happiness + "/" + bigdata.lv3 + ")";
//		} else {
//			happinessBar.fillAmount = 1;
//			happinessText.text = "Hiburan Lv 3 (MAX)";
//		}
	}


}
