using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSpawner : MonoBehaviour
{
    public GameController gameController;
    public GameObject[] pickups;
    public float xSpawnBoundary = 5.5f;
    public float ySpawnPosition = 0.0f;
    public float zSpawnBoundary = 3.7f;
    public float delayTime = 2.0f;
    public float intervalTime = 20.0f;

    private const string SPAWN_FUNC = "SpawnPickup";

    // Start is called before the first frame update
    void Start()
    {
        // Spawn the first pickup after some predetermined amount of time
        InvokeRepeating(SPAWN_FUNC, delayTime, intervalTime);
    }

    private void SpawnPickup()
    {
        // Pickup at index 0 MUST be assigned as the life pickup
        // If the player's health is maxed, do not allow a life pickup to spawn
        int startRange = gameController.lives < gameController.GetMaxHealth() ? 0 : 1;
        int pickupIndex = Random.Range(startRange, pickups.Length);

        Vector3 pickupPosition = new Vector3(Random.Range(-xSpawnBoundary, xSpawnBoundary), ySpawnPosition, Random.Range(-zSpawnBoundary, zSpawnBoundary));
        Instantiate(pickups[pickupIndex], pickupPosition, pickups[pickupIndex].transform.rotation, transform);
    }
}
