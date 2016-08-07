using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SosmedGenerator {

	public static void generateCommonSosmed(){


		GameManager.allSosmed = new List<SosMed> ();



		GameManager.allSosmed.Add (new SosMed (
			"@budibee",
			"Hello world !"

		));
		GameManager.allSosmed.Add (new SosMed (
			"@jonlihat",
			"Ngapain ya enaknya ?"

		));
		GameManager.allSosmed.Add (new SosMed (
			"@Ilovemymom",
			"hari baru pekerjaan baru!",
			DataCategory.ECONOMIC

		));
		GameManager.allSosmed.Add (new SosMed (
			"@bashee",
			"lari pagi biar sehat !"

		));
		GameManager.allSosmed.Add (new SosMed (
			"@Yayan",
			"Hati hati di daerah pasar suka banyak copet",
			DataCategory.CRIME

		));
		GameManager.allSosmed.Add (new SosMed (
			"@Bebeb",
			"Apa kabar dia ya?"

		));


		GameManager.allSosmed.Add (new SosMed (
			"@Malik",
			"Lihat kota yang bersih sudah menjadi hiburan tersendiri :)",
			DataCategory.HAPPINESS

		));
		GameManager.allSosmed.Add (new SosMed (
			"@mamamia",
			"belanja dulu ah"

		));
		GameManager.allSosmed.Add (new SosMed (
			"@onesama",
			"Jam berapa sekarang?"

		));
		GameManager.allSosmed.Add (new SosMed (
			"@Aminrisman",
			"Terkadang home teaching bisa jadi solusi pendidikan yang baik",
			DataCategory.EDUCATION

		));
		GameManager.allSosmed.Add (new SosMed (
			"@Wawaruru",
			"Masih aja banyak yang buang sampah sembarangan!",
			DataCategory.POLUTION

		));
		GameManager.allSosmed.Add (new SosMed (
			"@flashisslow",
			"percuma punya motor baru tapi jalan macet :(",
			DataCategory.TRAFFIC

		));
		GameManager.allSosmed.Add (new SosMed (
			"@Qiqi",
			"Lihat orang dipalak, gak berani nolong...",
			DataCategory.CRIME

		));
		GameManager.allSosmed.Add (new SosMed (
			"@Rezanonono",
			"butuh kerjaan..",
			DataCategory.ECONOMIC

		));
		GameManager.allSosmed.Add (new SosMed (
			"@tamasha",
			"coba kalau disini ada taman bunga, pasti keren",
			DataCategory.HAPPINESS

		));
		GameManager.allSosmed.Add (new SosMed (
			"@tordok",
			"Musim flu, perbanyak konsumsi buah",
			DataCategory.HEALTH

		));
	}

	public static void generateSpecialSosmed(string eventID){
		switch (eventID) {
		case  "sampah1":
			GameManager.allSosmed.Add (new SosMed (
				"@mirnaplz",
				"Euhh banyak kali ni sampah!",
				eventID));
			GameManager.allSosmed.Add (new SosMed (
				"@JokoGG",
				"Buang sampah pada tempatnya itu penting!",
				DataCategory.POLUTION,
				eventID
			));
			break;

		case "tokobuah1":
			GameManager.allSosmed.Add (new SosMed (
				"@NonaNoni",
				"Pengen buah segar...",
				eventID

			));
			break;


		default : 
			Debug.Log ("Sosmed : no such event id (" + eventID + ")");
			break;
		}
	}
}
