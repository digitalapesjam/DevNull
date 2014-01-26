using UnityEngine;
using System.Collections;

public class Map : MonoBehaviour {

	public Sprite city;
	public Sprite groundBlock;

	public Sprite[] largeSolidElements;
	public Sprite[] softElements;

	public float LevelLength = 100;
	public float cityDistance = 0.8f;

	private GameObject farCity;
	private GameObject floor;
	private GameObject largeSolidElementsPlaced;
	private Vector3 prevCameraPos;
	

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
			g.GetComponent<SpriteRenderer>().sortingOrder = -1;
			g.transform.position = new Vector3(i*city.bounds.size.x-5,-0.5f,0);
			g.transform.parent = farCity.transform;
		}

		float lastElementPos = 0;
		largeSolidElementsPlaced = new GameObject("solidElements");
		for (int i=0;i<LevelLength/10;i++){

			Sprite s = largeSolidElements[Mathf.FloorToInt(Random.Range(0,largeSolidElements.Length))];

			GameObject g = new GameObject("solid"+i);
			g.transform.position = new Vector3(lastElementPos+3+Random.Range(0,10),-2.6f,0);
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = s;
			g.AddComponent<BoxCollider2D>();
			g.GetComponent<SpriteRenderer>().sortingOrder = 0;


			if (s.name == "floatingPlatform")
				g.transform.Translate(0,1+Random.value,0);

			lastElementPos = g.transform.position.x;
			g.transform.parent = largeSolidElementsPlaced.transform;

		}

		prevCameraPos = Camera.main.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 cameraOffset = Camera.main.transform.position-prevCameraPos;
		prevCameraPos = Camera.main.transform.position;

		farCity.transform.position = Vector3.Lerp(farCity.transform.position,farCity.transform.position + cameraOffset*0.9f,Time.deltaTime*20);
	}
}
