using System.Linq;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    // Start is called before the first frame update

    public Transform[] spawnPoints;

    public GameObject blockPrefab;
    
    public float timeBetweenWaves = 1f;

    public int maxAmountToSpawn = 1;

    public float timeToSpawn = 2f;

    private void Awake()
    {
        InvokeRepeating("IncreaseDifficulty", 30f, 30f);
    }

    void Update(){
        if(Time.time >= timeToSpawn){
            SpawnBlocks();
            timeToSpawn = Time.time + timeBetweenWaves;
        }
    }
        
    void IncreaseDifficulty()
    {
        if (maxAmountToSpawn < spawnPoints.Length - 2) {
            maxAmountToSpawn += 1;
        }
        timeBetweenWaves /= 1.5f;
    }

    void SpawnBlocks (){
        var freeSpawns = spawnPoints.ToList();
        for(int i = 0; i < maxAmountToSpawn; i++)
        {
            var spawn = freeSpawns[Random.Range(0, freeSpawns.Count)];
            freeSpawns.Remove(spawn);
            Instantiate(blockPrefab, spawn.position, Quaternion.identity);
        }       
    }
}
