using UnityEngine;
using System.Collections.Generic;

public class Map : MonoBehaviour {

	public int numberOfItems;
	public GameObject player;
	public Sprite city;
	public Sprite groundBlock;
	public Sprite Gate;
	public Sprite WaterBlock;

	public Sprite[] largeSolidElements;
	public Sprite[] softElements;

	public float LevelLength = 100;
	public float cityDistance = 0.8f;

	private GameObject farCity;
	private GameObject floor;
	private GameObject largeSolidElementsPlaced;
	private GameObject water;
	private GameObject items;
	private Vector3 prevCameraPos;
	

	// Use this for initialization
	void Start () {
		int numberOfBlocks = (int)(LevelLength/groundBlock.bounds.size.x);
		floor = new GameObject("floor");
		water = new GameObject("water");
		items = new GameObject("items");

		GameObject bottom = new GameObject("floorBottom");
		bottom.transform.parent = floor.transform;
		bottom.AddComponent<BoxCollider2D> ();
		bottom.AddComponent<Item> ();
		bottom.GetComponent<Item> ().Player = player;
		bottom.GetComponent<Item> ().hpModifier = -1;
		bottom.transform.position = new Vector3(0, -10, 0);
		bottom.transform.localScale = new Vector2(groundBlock.bounds.size.x * numberOfBlocks, 2);

		List<Vector2> goodPointsGround = new List<Vector2> ();
		List<Vector2> goodPointsBuildings = new List<Vector2> ();
		for (int i =0; i<numberOfBlocks; i++){
			if (i < 10 || i>numberOfBlocks-10 || Random.value > 0.05f) {
				GameObject g = new GameObject("block"+i);
				g.AddComponent<SpriteRenderer>();
				g.GetComponent<SpriteRenderer>().sprite = groundBlock;
				g.AddComponent<BoxCollider2D>();
				g.GetComponent<SpriteRenderer>().sortingOrder = 0;
				g.transform.position = new Vector3(i*groundBlock.bounds.size.x-10,-3,0);
				g.transform.parent = floor.transform;
				if ((i * groundBlock.bounds.size.x-10) > 5) {
					goodPointsGround.Add (new Vector2(i * groundBlock.bounds.size.x-10,0));
				}
			} else {
				GameObject g = new GameObject("waterBlock"+i);
				g.AddComponent<SpriteRenderer>();
				g.GetComponent<SpriteRenderer>().sprite = WaterBlock;
				g.GetComponent<SpriteRenderer>().sortingOrder = 0;
				g.transform.position = new Vector3(i*groundBlock.bounds.size.x-10,-3,0);
				g.transform.parent = water.transform;
			}
		}

		int numberOfCieties = Mathf.CeilToInt(LevelLength/city.bounds.size.x);
		farCity = new GameObject("farCity");
		for (int i =0; i<numberOfCieties; i++){
			GameObject g = new GameObject("city"+i);
			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = city;
			g.GetComponent<SpriteRenderer>().sortingOrder = -1;
			g.transform.position = new Vector3(i*city.bounds.size.x-5,-1f,0);
			g.transform.parent = farCity.transform;
		}

		int lastElementPos = 10;
		largeSolidElementsPlaced = new GameObject("solidElements");
		for (int i=0;i<LevelLength/10;i++){

			Sprite s = largeSolidElements[Mathf.FloorToInt(Random.Range(0,largeSolidElements.Length))];

			GameObject g = new GameObject("solid"+i);

			int offset = 4 + Mathf.FloorToInt(Random.Range(0,10));

			g.transform.position = new Vector3(floor.transform.GetChild(lastElementPos+offset).position.x,-2.6f,0);


			g.AddComponent<SpriteRenderer>();
			g.GetComponent<SpriteRenderer>().sprite = s;
			g.AddComponent<BoxCollider2D>();
			g.GetComponent<SpriteRenderer>().sortingOrder = 0;


			if (s.name == "floatingPlatform")
				g.transform.Translate(0,1+Random.value,0);


			lastElementPos += offset;
			g.transform.parent = largeSolidElementsPlaced.transform;
			if ((i * groundBlock.bounds.size.x-10) > 5) {
				goodPointsBuildings.Add (g.transform.position);
			}

		}

		GameObject gate = new GameObject("gate");
		gate.AddComponent<SpriteRenderer>();
		gate.GetComponent<SpriteRenderer>().sprite = Gate;
		gate.AddComponent<BoxCollider2D>();
		gate.GetComponent<BoxCollider2D>().isTrigger = true;
		gate.GetComponent<BoxCollider2D>().size = new Vector2(0.2f,10);
		gate.GetComponent<SpriteRenderer>().sortingOrder = 0;
		gate.transform.localScale = new Vector3(1.5f,2,1);
		gate.transform.position = new Vector3(LevelLength-15,-2.6f,0);
		gate.transform.parent = largeSolidElementsPlaced.transform;


		gate = new GameObject("startGate");
		gate.AddComponent<SpriteRenderer>();
		gate.GetComponent<SpriteRenderer>().sprite = Gate;
		gate.GetComponent<SpriteRenderer>().sortingOrder = 0;
		gate.transform.localScale = new Vector3(1.5f,2,1);
		gate.transform.position = new Vector3(-5,-2.6f,0);
		gate.transform.parent = largeSolidElementsPlaced.transform;

		
		prevCameraPos = Camera.main.transform.position;

		if (numberOfItems > 0) {
			makeItems(goodPointsGround.ToArray(), goodPointsBuildings.ToArray());
		}
	}

	Vector2 findPosition(Vector2[] pts, int idx, int total) {
		return pts[(int)idx * pts.Length / total];
		//return pts[idx % pts.Length];
	}

	void makeItems(Vector2[] groundPoints, Vector2[] buildingPoints) {
		Texture2D[] textures = GameObject.FindObjectOfType<FbPicturesHolder> ().FbTextures;
		int lastUsedId = 0;
		int itemsCreated = 0;

		int onGround = (int)(numberOfItems / 3) * 2;
		int onBuildings = numberOfItems - onGround;

		for (int i = 0; i < onGround; i ++) {
			Vector2 pt = findPosition(groundPoints, i, onGround); //groundPoints[i % groundPoints.Length];
			Texture2D texture = textures[lastUsedId % textures.Length];
			makeItem (itemsCreated, pt, texture, 0, 1);
			lastUsedId += 1;
			itemsCreated += 1;
		}
		for (int i = 0; i < onBuildings; i ++) {
			Vector2 pt = findPosition(buildingPoints, i, onBuildings); //buildingPoints[i % buildingPoints.Length];
			Texture2D texture = textures[lastUsedId % textures.Length];
			makeItem (itemsCreated, pt, texture, 0, 2);
			lastUsedId += 1;
			itemsCreated += 1;
		}

	}

	void makeItem(int index, Vector2 pt, Texture2D texture, int hpMod, int ptMod) {
		Sprite s = Sprite.Create(texture,Rect.MinMaxRect(0,0,texture.width,texture.height),Vector2.one/2,texture.width);
		GameObject g = new GameObject("profile_" + index);
		g.transform.parent = items.transform;
		g.AddComponent<SpriteRenderer>();
		g.GetComponent<SpriteRenderer>().sprite = s;
		g.AddComponent<Rigidbody2D>();
		g.AddComponent<PolygonCollider2D>();
		g.GetComponent<Rigidbody2D>().fixedAngle = true;
		g.GetComponent<SpriteRenderer>().sortingOrder = 1;
		g.transform.position = new Vector2(pt.x, pt.y + 5);
		g.transform.localScale = new Vector2(0.6f, 0.6f);
		g.AddComponent<Item> ();
		Item item = g.GetComponent<Item> ();
		item.Player = player;
		item.hpModifier = hpMod;
		item.ptModifier = ptMod;
	}

	// Update is called once per frame
	void Update () {
		Vector3 cameraOffset = Camera.main.transform.position-prevCameraPos;
		prevCameraPos = Camera.main.transform.position;

		farCity.transform.position = Vector3.Lerp(farCity.transform.position,farCity.transform.position + cameraOffset*0.9f,Time.deltaTime*20);
	}
}
