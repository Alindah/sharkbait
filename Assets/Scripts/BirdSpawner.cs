using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject swallowPrefab;
    public GameObject seagullPrefab;
    public float xBoundary;
    public float zBoundary;
    public float ySpawnPosition;
    public float delayTime;
    public float spawnIntervalMin;
    public float spawnIntervalMax;
    public float swallowPercentage = 30.0f;

    private const string SPAWN_FUNC = "SpawnBird";
    private float spawnInterval;

    // Start is called before the first frame update
    void Start()
    {
        // Spawn first bird
        Invoke(SPAWN_FUNC, delayTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (!IsInvoking(SPAWN_FUNC))
        {
            spawnInterval = Random.Range(spawnIntervalMin, spawnIntervalMax);
            Invoke(SPAWN_FUNC, spawnInterval);
        }
    }

    private void SpawnBird()
    {
        Vector3 birdPosition;
        float randomNum = Random.Range(0, 100);         // Used to determine if swallow will spawn or seagull
        int orientation = Random.Range(-1, 3) * 90;     // Used to determine direction bird will face and spawn in
        GameObject birdPrefab = randomNum < swallowPercentage ? swallowPrefab : seagullPrefab;
        float xSpawnPosition = Random.Range(-xBoundary, xBoundary);
        float zSpawnPosition = Random.Range(-zBoundary, zBoundary);

        // Determine where to place bird based on its direction facing
        switch (orientation)
        {
            case 0:
                zSpawnPosition = -zBoundary;
                break;
            case 180:
                zSpawnPosition = zBoundary;
                break;
            case 90:
                xSpawnPosition = -xBoundary;
                break;
            case -90:
                xSpawnPosition = xBoundary;
                break;
            default:
                break;
        }

        // Set bird position and rotation
        birdPosition = new Vector3(xSpawnPosition, ySpawnPosition, zSpawnPosition);
        Quaternion birdOrientation = Quaternion.Euler(birdPrefab.transform.rotation.x, orientation, birdPrefab.transform.rotation.z);

        // Spawn bird
        Instantiate(birdPrefab, birdPosition, birdOrientation, transform);
    }
}
