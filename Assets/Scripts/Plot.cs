using UnityEngine;
using System.Collections;

public class Plot : MonoBehaviour {

	Facility facility;
	SpriteRenderer parentRenderer;
	//	GameObject parent;
	public bool validPlace;

	Renderer r;

	// Use this for initialization
	void Awake () {
		r = GetComponent<Renderer> ();
		facility = GetComponentInParent<Facility> ();
		//		parent = facility.gameObject;
//		if (!facility.data.isNew ()) {
//			gameObject.layer = 0;
//		}
		parentRenderer = GetComponentInParent <SpriteRenderer> ();
		validPlace = true;
		generateMesh ();
	}


	// Update is called once per frame


	void OnTriggerEnter2D(Collider2D collision){
//		Debug.Log (collision.name);
		if ( facility.data.isNew() && (collision.tag.Equals("Plot")
		    || collision.tag.Equals("Plain") || collision.tag.Equals("Path Blocker"))){



			parentRenderer.color = Color.red;	
			r.material.color = Color.red;
			validPlace = false;
		}
	}

	void OnTriggerStay2D(Collider2D collision){

		if (facility.data.isNew() && (collision.tag.Equals("Plot")
          || collision.tag.Equals("Plain") || collision.tag.Equals("Path Blocker"))){

		

			parentRenderer.color = Color.red;	
			r.material.color = Color.red;
			validPlace = false;
		}
	}

	void OnTriggerExit2D(Collider2D collision){
		if ( facility.data.isNew() && (collision.tag.Equals("Plot")
           || collision.tag.Equals("Plain") || collision.tag.Equals("Path Blocker"))){
			parentRenderer.color = Color.white;
			r.material.color = Color.white;
			validPlace = true;
		}

	}

	public void switchPlot(bool toogleOn){
		r.sortingLayerName = toogleOn ?
			"Object" : "Default";
	}

	void generateMesh(){
		PolygonCollider2D poly;
		Vector2[] points;
		poly = GetComponent<PolygonCollider2D> ();
		points = poly.points;

		for (int i = 0; i < points.Length; i++) {
			points [i] += poly.offset;
		}

		Triangulator tr = new Triangulator (points);
		int[] indices = tr.Triangulate ();

		Vector3[] vertices = new Vector3[points.Length];
		for (int i = 0; i < points.Length; i++) {
			vertices [i] = new Vector3 (points [i].x, points [i].y, 0);
		}

		Mesh mesh = new Mesh ();
		mesh.vertices = vertices;
		mesh.triangles = indices;
//		for (int i = 0; i < points.Length; i++) {
//			points [i].x /= textureSize;
//			points [i].y /= textureSize;
//		}

		mesh.uv = points;
		mesh.RecalculateNormals();
		mesh.RecalculateBounds();

		GetComponent<MeshFilter> ().mesh = mesh;
	}



}
