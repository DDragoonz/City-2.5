using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class UI_Manager_World : MonoBehaviour {

	private RotateGlobe rg;
	public Text townText,deskripsi;
	public GameObject panel1, panel2,introText;

	// Use this for initialization
	void Start () {
		GameManager.state = State.INTRO;
		rg = FindObjectOfType<RotateGlobe> ();
		townText.text = GameManager.activeTown.name;
		deskripsi.text = GameManager.activeTown.description;

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
	}

	public void prevTown(){
		rg.currentIndex = rg.currentIndex-1 < 0 ? rg.towns.Length-1 : rg.currentIndex-1;
		GameManager.activeTown = GameManager.Towns.Find (x => x.id == rg.currentIndex);
		townText.text = GameManager.activeTown.name;
		deskripsi.text = GameManager.activeTown.description;
	}

	public void selectTownActive(){
		panel1.SetActive (true);
		panel2.SetActive (true);
		introText.SetActive (false);
		GameManager.state = State.SELECT_TOWN;
	}

	public void loadRegionScene(){
//		GameManager.activeTown = GameManager.userdata.towns [rg.currentIndex];

		StartCoroutine (loadRegion ());
	}

	IEnumerator loadRegion(){
		yield return new WaitForSeconds (.1f);

		AsyncOperation async = SceneManager.LoadSceneAsync ("Town "+rg.currentIndex);

		while (!async.isDone) {
			yield return null;
		}
	}
}
