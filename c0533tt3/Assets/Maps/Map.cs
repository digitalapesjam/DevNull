using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public Sprite city;
	public Sprite groundBlock;
	public float LevelLength = 100;
	public float cityDistance = 0.8f;

	private GameObject farCity;
	private GameObject floor;
	private float prevCameraX = 0;

	// Use this for initialization
	void Start () {
		int numberOfBlocks = (int)(LevelLength/groundBlock.bounds.size.x);
		floor = new GameObject("floor");
		for (int i =0; i<numberOfBlocks; i++){
			GameObject g = new GameObject("block"+i);
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = groundBlock;
			g.AddComponent<PolygonCollider2D>();
			g.GetComponent<SpriteRenderer>().sortingOrder = 0;
			g.transform.position = new Vector3(i*groundBlock.bounds.size.x-10,-3,0);
			g.transform.parent = floor.transform;
		}

		int numberOfCieties = Mathf.CeilToInt(LevelLength/city.bounds.size.x);
		farCity = new GameObject("farCity");
		for (int i =0; i<numberOfCieties; i++){
			GameObject g = new GameObject("city"+i);
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = city;
			g.AddComponent<PolygonCollider2D>();
			g.GetComponent<SpriteRenderer>().sortingOrder = -1;
			g.transform.position = new Vector3(i*city.bounds.size.x-10,-0.5f,0);
			g.transform.parent = farCity.transform;
		}

		prevCameraX = Camera.main.transform.position.x;
	}
	
	// Update is called once per frame
	void Update () {
		float cameraOffset = Camera.main.transform.position.x-prevCameraX;
		prevCameraX = Camera.main.transform.position.x;

		farCity.transform.Translate(cameraOffset*cityDistance,0,0);
	}
}
