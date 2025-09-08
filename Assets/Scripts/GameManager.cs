using UnityEngine;
using System.Collections.Generic; // List

/// <summary>
/// Manages the main gameplay of the game
/// </summary>

public class GameManager : MonoBehaviour{
    
    [Tooltip("A reference to the tile we want to spawn")]
    public Transform tile;

    [Tooltip("A reference to the obstacle we want to spawn")]
    public Transform obstacle;

    [Tooltip("Where the first tile should be placed at")]
    public Vector3 startPoint = new Vector3(0,0, -5);

    [Tooltip("How many tiles should we create in advance")]
    [Range(1,15)]
    public int initSpawnNum = 10;

    [Tooltip("How many tiles should we create without obstacles")]
    public int initNoObstacles = 10;

    [Tooltip("Where the nex tile should be spawned at")]
    private Vector3 nextTileLocation;

    [Tooltip("How should the next tile be rotated?")]
    private Quaternion nextTileRotation;


    private void Start(){

        nextTileLocation = startPoint;
        nextTileRotation = Quaternion.identity;

        for (int i = 0; i < initSpawnNum; ++i){
            SpawnNextTile(i >= initNoObstacles);
        }

    }

    public void SpawnNextTile(bool spawnObstacles = true){
        
        var newTile = Instantiate(tile, 
        nextTileLocation, nextTileRotation);

        var nextTile = newTile.Find("NextSpawnPoint");
        nextTileLocation = nextTile.position;
        nextTileRotation = nextTile.rotation;

        if(spawnObstacles){
            SpawnObstacle(newTile);
        }

    }


    public void SpawnObstacle(Transform newTile){

        var obsSpawnPoints = new List<GameObject>();

        foreach (Transform child in newTile){

            if (child.CompareTag("ObstacleSpawn")){
                obsSpawnPoints.Add(child.gameObject);
            }

        }

        if(obsSpawnPoints.Count > 0){

            int index = Random.Range(0, obsSpawnPoints.Count);

            var spawnPoint = obsSpawnPoints[index];

            var spawnPos = spawnPoint.transform.position;

            var newObstacle = Instantiate(obstacle, spawnPos, Quaternion.identity);

            newObstacle.SetParent(spawnPoint.transform);

        }

    }

}