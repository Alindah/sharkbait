using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupController : MonoBehaviour
{
    public float pickupDuration = 10.0f;
    private GameController gameController;
    private AudioManager audioManager;
    private const string PU_LIFE = "PickupLife";
    private const string PU_STARFISH = "PickupStarfish";
    private const string PU_WILSON = "Wilson";
    private const string GAME_CONTROLLER_NAME = "GameController";
    private const string AUDIO_MANAGER_NAME = "AudioManager";

    void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();
        audioManager = GameObject.Find(AUDIO_MANAGER_NAME).GetComponent<AudioManager>();

        StartCoroutine("PickupLifespan");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (tag == PU_LIFE)
            {
                audioManager.PlaySoundEffect(audioManager.audioPickupLife);
                TriggerActionLifePickup();
            }
            else if (tag == PU_STARFISH)
            {
                audioManager.PlaySoundEffect(audioManager.audioPickupBorder);
                transform.parent.GetComponent<BorderController>().DisableBorderTemporarily();
            }
            else if (tag == PU_WILSON)
            {
                other.gameObject.transform.GetChild(1).gameObject.SetActive(true);
            }

            Destroy(gameObject);
        }

        if (other.tag == "Enemy" && tag == PU_WILSON)
        {
            audioManager.PlaySoundEffect(audioManager.audioBite);
            gameController.SetWilsonActive(false);
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
