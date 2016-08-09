using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class LoadingScreen : MonoBehaviour {

	public float minimumLoadingTime = 0;

	// Use this for initialization
	void Start () {
		StartCoroutine (loadRegion ());
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	IEnumerator loadRegion(){
		yield return new WaitForSeconds (minimumLoadingTime);
		
		AsyncOperation async = SceneManager.LoadSceneAsync ("Town "+GameManager.activeTown.id);
		
		while (!async.isDone) {
			yield return null;
		}
	}
}
