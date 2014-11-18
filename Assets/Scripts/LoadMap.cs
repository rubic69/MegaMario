using UnityEngine;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;

public class LoadMap : MonoBehaviour {

	int X, Y;
	public int mapHeight;
	public int mapWidth;
	
	GameObject mapTile;
	
	GameObject [,] mapArray;
	public GameObject [] Tiles = new GameObject[8];

	public Transform player;
	public Transform floor_valid;
	
	public const char sfloor_valid = '0';

	// Use this for initialization
	void Start () {
		char[][] map = readFile(Application.dataPath + "/Maps/level1.txt");
		loadMap (map);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	char[][] readFile(string file){
		string text = System.IO.File.ReadAllText(file);
		string[] lines = Regex.Split(text, "\r\n");
		int rows = lines.Length;
		
		char[][] levelBase = new char[rows][];
		for (int i = 0; i < lines.Length; i++)  {
			char[] chars = lines[i].ToCharArray();
			levelBase[i] = chars;
		}
		return levelBase;
	}

	void loadMap(char[][] map) {
		for (int i = 0; i < map.Length; i++) {
			for (int j = 0; j < map[i].Length; j++) {
				switch (map[i][j]){	
					case sfloor_valid:
						Instantiate(floor_valid, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						break;
				}
			}
		} 
	}
}
