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
	public Transform enemy;
	public Transform turtleEnemy;
	public Transform groundBox;
	public Transform startPoint;
	public Transform checkPoint;
	public Transform coinBox;
	public int level = 1;
	
	private const char groundBoxValue = '0';
	private const char startPointValue = '1';
	private const char checkPointValue = '2';
	private const char enemyValue = '3';
	private const char coinBoxValue = '4';
	private const char turtleEnemyValue = '5';

	// Use this for initialization
	void Start () {
		char[][] map = readFile(Application.dataPath + "/Maps/level" + level + ".txt");
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
					case groundBoxValue:
						Instantiate(groundBox, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						break;
					case startPointValue:
						Instantiate(startPoint, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						//Instantiate(player, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						break;
					case checkPointValue:
						Instantiate(checkPoint, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						break;
					case enemyValue:
						Instantiate(enemy, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						break;
					case coinBoxValue:
						Instantiate(coinBox, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						break;
					case turtleEnemyValue:
						Instantiate(turtleEnemy, new Vector3(j-0.5f, -i-0.5f, 0), Quaternion.identity);
						break;
				}
			}
		} 
	}
}
