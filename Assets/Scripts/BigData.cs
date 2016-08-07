using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class BigData {

	public float 
		economic,
		health,
		polution,
		crime,
		traffic,
		education,
		happiness;

	public float lv2, lv3;
	public float[] level;

	public List<FacilityData> unlockedFacilities;

	public void updateData(DataCategory category){
		int 
		economicModifier	=0,
		healthModifier		=0,
		polutionModifier	=0,
		crimeModifier		=0,
		trafficModifier		=0,
		educationModifier	=0,
		happinessModifier	=0;

		//change modifier here later when sosialize and event is completed
		switch (category) {
		case DataCategory.CRIME:
			crimeModifier += 1;
			break;
		case DataCategory.ECONOMIC:
			economicModifier += 1;
			break;
		case DataCategory.EDUCATION:
			educationModifier += 1;
			break;
		case DataCategory.HEALTH:
			healthModifier += 1;
			break;
		case DataCategory.HAPPINESS:
			happinessModifier += 1;
			break;
		case DataCategory.POLUTION:
			polutionModifier += 1;
			break;
		case DataCategory.TRAFFIC:
			trafficModifier += 1;
			break;

		}



		economic += economicModifier;
		health += healthModifier;
		polution += polutionModifier;
		crime += crimeModifier;
		traffic += trafficModifier;
		education += educationModifier;
		happiness += happinessModifier;



	}

	public void progress (DataCategory category, int value){
		switch (category) {
		case DataCategory.CRIME:
			crime += value;
			break;
		case DataCategory.ECONOMIC:
			economic += value;
			break;
		case DataCategory.EDUCATION:
			education += value;
			break;
		case DataCategory.HEALTH:
			health += value;
			break;
		case DataCategory.HAPPINESS:
			happiness += value;
			break;
		case DataCategory.POLUTION:
			polution += value;
			break;
		case DataCategory.TRAFFIC:
			traffic += value;
			break;

		}
	}

}
