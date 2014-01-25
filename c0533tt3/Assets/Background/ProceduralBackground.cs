using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class ProceduralBackground : MonoBehaviour {
	
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

		for (int i=0;i<width/8;i++)
			map.SetPixel((int)(Random.value*width),(int)(Random.value*height),RainColor);

		map.Apply();

		renderer.sharedMaterial.SetTexture("_Parallax0",map);

		map = new Texture2D(width/2,height/2);
		for (int i=0;i<width;i++)
			for (int j=0;j<height;j++)
				map.SetPixel(i,j,transparent);
		
		for (int i=0;i<width/10;i++)
			map.SetPixel((int)(Random.value*width),(int)(Random.value*height),RainColor*0.8f);
		
		map.Apply();

		renderer.sharedMaterial.SetTexture("_Parallax1",map);

		map = new Texture2D(width,height);
		for (int i=0;i<width;i++)
			for (int j=0;j<height;j++)
				map.SetPixel(i,j,transparent);
		
		for (int i=0;i<width/4;i++)
			map.SetPixel((int)(Random.value*width),(int)(Random.value*height),RainColor*0.8f);
		
		map.Apply();
		
		renderer.sharedMaterial.SetTexture("_Parallax2",map);
	}
	
	// Update is called once per frame
	void Update () {
		transform.position = new Vector3(Camera.main.transform.position.x,Camera.main.transform.position.y,0.5f);

		int height = Screen.height;
		int width = Screen.width;
		float scaleZ = Camera.main.transform.position.z/-7.5f;
		float scaleX = scaleZ * 1.01f * ((float)width)/height;
		transform.localScale = new Vector3(scaleX,1,scaleZ);
	}
}
