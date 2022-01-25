using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoconutController : MonoBehaviour
{
    [Header("Wilson")]
    public GameObject wilsonPrefab;
    public Material wilsonMaterial;
    public Vector3 wilsonRotation = new Vector3(64.136f, 90.0f, 90.0f);
    private GameController gameController;
    private GameObject wilson = null;
    private const string GAME_CONTROLLER_NAME = "GameController";
    private const string AUDIO_MANAGER_NAME = "AudioManager";
    private const string WILSON_TAG = "Wilson";

    void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();

        float randomNum = Random.Range(1, 101);

        // Only spawn a Wilson if there is not another one in the scene
        if (!gameController.WilsonIsActive() && randomNum <= gameController.wilsonSpawnChance)
        {
            GetComponent<Renderer>().material = wilsonMaterial;
            transform.rotation = Quaternion.Euler(wilsonRotation);
            tag = WILSON_TAG;
            gameController.SetWilsonActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        AudioManager audioManager = GameObject.Find(AUDIO_MANAGER_NAME).GetComponent<AudioManager>();

        if (other.name == "Ocean")
        {
            audioManager.PlaySoundEffect(audioManager.audioSplash);
            
            // If this is Wilson, spawn a copy of him for the player to pick up
            if (tag == WILSON_TAG)
            {
                gameController.SetWilsonActive(true);
                wilson = Instantiate(wilsonPrefab, transform.position, Quaternion.Euler(wilsonRotation));
                Destroy(gameObject);
            }
        }
    }

    private void OnDestroy()
    {
        // If this is Wilson's tag and a pickup copy of him was never instantiated, he has been destroyed by going out of bounds 
        if (tag == WILSON_TAG && wilson == null)
            gameController.SetWilsonActive(false);
    }
}
