using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkSpawner : MonoBehaviour
{
    public GameObject sharkPrefab;
    public float xBoundary;
    public float zBoundary;
    public float ySpawnPosition;
    public float delayTime;
    public float spawnIntervalMin;
    public float spawnIntervalMax;
    public int spawnLimit;

    private const string SPAWN_FUNC = "SpawnShark";
    private float spawnInterval;

    void Start()
    {
        // Spawn the first shark after some predetermined amount of time
        Invoke(SPAWN_FUNC, delayTime);
    }

    void Update()
    {
        // Do not spawn sharks beyond the spawn limit
        if (transform.childCount >= spawnLimit)
        {
            CancelInvoke(SPAWN_FUNC);
        }
        else if (!IsInvoking(SPAWN_FUNC))
        {
            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            Invoke(SPAWN_FUNC, spawnInterval);
        }
    }

    // Spawn a shark
    private void SpawnShark()
    {
        Vector3 sharkPosition = new Vector3(Random.Range(-xBoundary, xBoundary), ySpawnPosition, Random.Range(-zBoundary, zBoundary));
        Instantiate(sharkPrefab, sharkPosition, sharkPrefab.transform.rotation, transform);
    }
}
