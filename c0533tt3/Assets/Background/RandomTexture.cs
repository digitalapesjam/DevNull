using UnityEngine;
using System.Collections;

public class RandomTexture : MonoBehaviour {
	
	public Color RainColor = new Color(0.5f,0.5f,1.0f);

	// Use this for initialization
	void Start () {
		int height = Screen.height;
		int width = Screen.width;

		Color transparent = new Color(0,0,0,0);
		Texture2D map = new Texture2D(width/2,height/2);
		for (int i=0;i<width;i++)
			for (int j=0;j<height;j++)
				map.SetPixel(i,j,transparent);

		for (int i=0;i<width/4;i++)
			map.SetPixel((int)(Random.value*width),(int)(Random.value*height),RainColor);

		map.Apply();

		renderer.material.SetTexture("_Parallax0",map);

		map = new Texture2D(width/2,height/2);
		for (int i=0;i<width;i++)
			for (int j=0;j<height;j++)
				map.SetPixel(i,j,transparent);
		
		for (int i=0;i<width/6;i++)
			map.SetPixel((int)(Random.value*width),(int)(Random.value*height),RainColor*0.8f);
		
		map.Apply();

		renderer.material.SetTexture("_Parallax1",map);

		map = new Texture2D(width,height);
		for (int i=0;i<width;i++)
			for (int j=0;j<height;j++)
				map.SetPixel(i,j,transparent);
		
		for (int i=0;i<width/2;i++)
			map.SetPixel((int)(Random.value*width),(int)(Random.value*height),RainColor*0.8f);
		
		map.Apply();
		
		renderer.material.SetTexture("_Parallax2",map);

		transform.localScale = new Vector3(((float)width)/height,1,1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = Camera.main.transform.position + Vector3.forward*8;
	}
}
