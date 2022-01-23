using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float pickupDuration = 10.0f;
    private GameController gameController;
    private const string PU_LIFE = "PickupLife";
    private const string PU_STARFISH = "PickupStarfish";
    private const string GAME_CONTROLLER_NAME = "GameController";

    void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();
        StartCoroutine("PickupLifespan");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (tag == PU_LIFE)
                TriggerActionLifePickup();
            else if (tag == PU_STARFISH)
                transform.parent.GetComponent<BorderController>().DisableBorderTemporarily();

            Destroy(gameObject);
        }
    }

    // Destroy the pickup after some amount of time if it has not been picked up
    private IEnumerator PickupLifespan()
    {
        yield return new WaitForSeconds(pickupDuration);
        Destroy(gameObject);
    }

    // Life pickups add an extra life
    private void TriggerActionLifePickup()
    {
        gameController.UpdateHealth(1);
    }
}
