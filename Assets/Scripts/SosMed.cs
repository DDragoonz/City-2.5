using UnityEngine;
using System.Collections;
[System.Serializable]
public struct SosMed  {

	public string from,message;
	public DataCategory category;
	public string eventId;

	public SosMed(string user, string message){
		this.from = user;
		this.message = message;
		this.category = DataCategory.NONE;
		this.eventId = "common";
	}
	public SosMed(string user, string message,string eventId){
		this.from = user;
		this.message = message;
		this.category = DataCategory.NONE;
		this.eventId = eventId;
	}

	public SosMed(string user, string message, DataCategory category){
		this.from = user;
		this.message = message;
		this.category = category;
		this.eventId = "common";
	}
	public SosMed(string user, string message, DataCategory category, string eventId){
		this.from = user;
		this.message = message;
		this.category = category;
		this.eventId = eventId;
	}
}
[System.Serializable]
public enum DataCategory{
	NONE,
	ECONOMIC,
	HEALTH,
	POLUTION,
	TRAFFIC,
	EDUCATION,
	CRIME,
	HAPPINESS
}
