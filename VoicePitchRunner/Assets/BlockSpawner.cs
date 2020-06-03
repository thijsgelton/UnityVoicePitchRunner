using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] spawnPoints;

    public GameObject blockPrefab;
    
    public float timeBetweenWaves = 1f;

    public int maxAmountToSpawn = 2;

    public float timeToSpawn = 2f;

    void Update(){
        if(Time.time >= timeToSpawn){
            SpawnBlocks();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
    }
        

    void SpawnBlocks (){
        var freeSpawns = spawnPoints.ToList();
        for(int i = 0; i < maxAmountToSpawn; i++)
        {
            var spawn = freeSpawns[Random.Range(0, spawnPoints.Length)];
            freeSpawns.Remove(spawn);
            Instantiate(blockPrefab, spawn.position, Quaternion.identity);
        }       
    }
}
