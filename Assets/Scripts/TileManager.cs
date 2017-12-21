using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TileManager : MonoBehaviour {


    public GameObject[] TilePrefabs;

    public GameObject currentTile;

    private static TileManager instance;

    private Stack<GameObject> leftTiles = new Stack<GameObject>();

    private Stack<GameObject> topTiles = new Stack<GameObject>();

    public static TileManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<TileManager>();
            }
            return instance;
        }

      
    }

    public Stack<GameObject> LeftTiles
    {
        get
        {
            return leftTiles;
        }

        set
        {
            leftTiles = value;
        }
    }

    public Stack<GameObject> TopTiles
    {
        get
        {
            return topTiles;
        }

        set
        {
            topTiles = value;
        }
    }


    // Use this for initialization
    void Start () {

        CreateTiles(100);
        for (int i = 0; i < 50; i++)
        {
            SpawnTile();
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    public void CreateTiles (int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            LeftTiles.Push(Instantiate(TilePrefabs[0]));
            TopTiles.Push(Instantiate(TilePrefabs[1]));
            TopTiles.Peek().name = "TopTile";
            TopTiles.Peek().SetActive(false);
            LeftTiles.Peek().name = "LeftTile";
            LeftTiles.Peek().SetActive(false);
           
        }
    }
    public void SpawnTile()
    {

        if(TopTiles.Count == 0 || LeftTiles.Count == 0)
        {
            CreateTiles(10);
        }


        int RandomIndex = Random.Range(0, 2);

        if (RandomIndex == 0)
        {
            GameObject tmp = LeftTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(RandomIndex).position;
            currentTile = tmp;
        }
        else if(RandomIndex==1)
        {

            GameObject tmp = TopTiles.Pop();
            tmp.SetActive(true);
            tmp.transform.position = currentTile.transform.GetChild(0).transform.GetChild(RandomIndex).position;
            currentTile = tmp;

        }

        int spawnPickup = Random.Range(0, 10);

        if (spawnPickup == 0)
        {
            currentTile.transform.GetChild(1).gameObject.SetActive(true);
        }

        currentTile =(GameObject) Instantiate(TilePrefabs[RandomIndex], currentTile.transform.GetChild(0).transform.GetChild(RandomIndex).position, Quaternion.identity);
    }

    public void ResetGame()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
