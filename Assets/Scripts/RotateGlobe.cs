using UnityEngine;
using System.Collections;

public class RotateGlobe : MonoBehaviour {

	public Transform[] towns;
	public int currentIndex = 0;


	
	// Update is called once per frame
	void Update () {

		if (GameManager.state == State.SELECT_TOWN) {

			Transform temp = towns [currentIndex];
			temp.LookAt (Vector3.zero);

			transform.rotation = Quaternion.Slerp (transform.rotation, temp.rotation, Time.deltaTime);
		} else {
			transform.Rotate (new Vector3 (0, .2f, 0));
		}


	}
}
