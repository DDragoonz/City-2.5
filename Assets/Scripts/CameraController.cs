using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using System.Collections.Generic;

public class CameraController : MonoBehaviour
{

	public float mouseSensitivity;
	public float maxScroll, minScroll;
	public float maxX, minX, maxY, minY;
	Vector3 lastpos;
	GameObject detailPanel;
	bool allowDrag;
	float lastDelta;

	// Use this for initialization
	void Start ()
	{
		detailPanel = FindObjectOfType<UI_Manager_Region> ().detailPanel;
		allowDrag = true;
	}
	
	// Update is called once per frame
	void Update ()
	{

		#if UNITY_EDITOR

		float scroll = Input.GetAxis ("Mouse ScrollWheel");
		if (scroll != 0 &&
		    GameManager.state != State.BUILD_FACILITY_MENU && GameManager.state != State.BIG_DATA_MENU && GameManager.state != State.EVENT_MENU) {
			Camera.main.orthographicSize = 
			Mathf.Clamp (
				Camera.main.orthographicSize - scroll, minScroll, maxScroll);
		}

		if (Input.GetMouseButtonDown (0)) {
			lastpos = Input.mousePosition;

			Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//			RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.zero);
//			if (hit.collider == null) {
//
//				if (!EventSystem.current.IsPointerOverGameObject ()) {
//					detailPanel.SetActive (false);
//					Facility.unselectAll ();
//					allowDrag = true;
//				}
//			} else if (hit.collider.tag.Equals ("Objects")) {
//				
//				if (hit.collider.GetComponent<Facility> ().data.isNew ()) {
//					allowDrag = false;
//				}
//			}
//			else {
//				
//				allowDrag = true;
//			}

			allowDrag = true;

			short counter = 0;
			bool selectingObject = false;

			foreach(RaycastHit2D hit in Physics2D.RaycastAll (pos,Vector2.zero)){
				counter++;
				if(hit.collider.tag.Equals("Objects") ){
					Facility f = hit.collider.GetComponent<Facility> ();

					selectingObject = true;

					if (f.data.isNew ()) {

						allowDrag = false;
					}
					else {

						Facility.unselectAll();
						if(!f.isSelected && GameManager.state == State.REGION_GAMEPLAY){
							print ("touching object");
							detailPanel.GetComponent<DetailPanel> ().changeFacility (f);
							f.isSelected = true;
							//				GetComponent<SpriteRenderer> ().sprite = selected;
							f.GetComponent<SpriteRenderer> ().sortingLayerName = "Selected";
							foreach (Image i in detailPanel.GetComponentsInChildren<Image>()) {
								if (i.name.Equals ("Icon")) {
									i.sprite = f.normal;
									break;
								}
							}
							foreach (Text i in detailPanel.GetComponentsInChildren<Text>()) {
								if (i.name.Equals ("Nama Objek")) {
									i.text = name;
									break;
								}
							}
						}
					}

				}
				else {
					if (!EventSystem.current.IsPointerOverGameObject () && !selectingObject) {
						detailPanel.SetActive (false);
						Facility.unselectAll ();
					}
				}
			}

			if(counter == 0 && !EventSystem.current.IsPointerOverGameObject ()){ // not sure if this work in mobile input
				print ("not hit anything");
				detailPanel.SetActive (false);
				Facility.unselectAll ();
			}



		}
//			
//
		if (GameManager.state != State.BUILD_FACILITY_MENU && GameManager.state != State.BIG_DATA_MENU
		    && GameManager.state != State.EVENT_MENU && allowDrag) {
			if (Input.GetMouseButton (0)) {
			

				Vector3 delta = Input.mousePosition - lastpos;
//				Vector2 pos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
//				RaycastHit2D hit = Physics2D.Raycast (pos, Vector2.zero);

				transform.Translate (-delta.x * Camera.main.orthographicSize * mouseSensitivity, -delta.y * Camera.main.orthographicSize * mouseSensitivity, 0);
				lastpos = Input.mousePosition;	

				transform.position = new Vector3 (
					Mathf.Clamp (transform.position.x, minX, maxX),
					Mathf.Clamp (transform.position.y, minY, maxY),
					transform.position.z

				);
			}

		}



		#elif UNITY_ANDROID
	
		if (GameManager.state != State.BUILD_FACILITY_MENU && GameManager.state != State.BIG_DATA_MENU && GameManager.state != State.EVENT_MENU) {
			if (Input.touchCount == 1) {
				if (Input.GetTouch (0).phase == TouchPhase.Began) {
					Vector2 pos = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position);
					allowDrag = true;

					short counter = 0;

					foreach(RaycastHit2D hit in Physics2D.RaycastAll (pos,Vector2.zero)){
						counter++;
						if(hit.collider.tag.Equals("Objects")){
							Facility f = hit.collider.GetComponent<Facility> ();
							if (f.data.isNew ()) {

								allowDrag = false;
							}
							else {
								Facility.unselectAll();
								if(!f.isSelected && GameManager.state == State.REGION_GAMEPLAY){
									f.isSelected = true;
									//				GetComponent<SpriteRenderer> ().sprite = selected;
									GetComponent<SpriteRenderer> ().sortingLayerName = "Selected";
									foreach (Image i in detailPanel.GetComponentsInChildren<Image>()) {
										if (i.name.Equals ("Icon")) {
											i.sprite = f.normal;
											break;
										}
									}
									foreach (Text i in detailPanel.GetComponentsInChildren<Text>()) {
										if (i.name.Equals ("Nama Objek")) {
											i.text = name;
											break;
										}
									}
								}
							}
						}
					}
					if(counter == 0 && !EventSystem.current.IsPointerOverGameObject ()){ // not sure if this work in mobile input
						print ("not hit anything");
						detailPanel.SetActive (false);
						Facility.unselectAll ();
					}
				}

				if (Input.GetTouch (0).phase == TouchPhase.Moved && allowDrag) {
				
					// Get movement of the finger since last frame
					Vector2 touchDeltaPosition = Input.GetTouch (0).deltaPosition;

					// Move object across XY plane
					transform.Translate (-touchDeltaPosition.x * Camera.main.orthographicSize * mouseSensitivity, -touchDeltaPosition.y * Camera.main.orthographicSize * mouseSensitivity, 0);

					transform.position = new Vector3 (
						Mathf.Clamp (transform.position.x, minX, maxX),
						Mathf.Clamp (transform.position.y, minY, maxY),
						transform.position.z
					);


				}


			}
			if (Input.touchCount == 2) {

				Touch t1 = Input.GetTouch (0);
				Touch t2 = Input.GetTouch (1);

				Vector2 t1PrevPos = t1.position - t1.deltaPosition;
				Vector2 t2PrevPos = t2.position - t2.deltaPosition;

				float prevDeltaMag = (t1PrevPos - t2PrevPos).magnitude;
				float newDeltaMag = (t1.position - t2.position).magnitude;

				float magDifference = prevDeltaMag - newDeltaMag;

				Camera.main.orthographicSize += magDifference * mouseSensitivity;

				Camera.main.orthographicSize = 
					Mathf.Clamp (
					Camera.main.orthographicSize, minScroll, maxScroll);




			}
		}

		#endif


	
			
	}

	private bool isPointerOverUI ()
	{
		PointerEventData pointer = new PointerEventData (EventSystem.current);
		pointer.position = Input.GetTouch (0).position;
		List<RaycastResult> result = new List<RaycastResult> ();
		EventSystem.current.RaycastAll (pointer, result);
		return result.Count > 0;
	}
}
	