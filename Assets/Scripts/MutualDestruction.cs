using System.Linq;
using UnityEngine;

public class MutualDestruction : MonoBehaviour
{
    private AwardPoints awardPoints;
    private AudioManager audioManager;
    private GameController gameController;
    private string[] excludedTags = { "Player", "Wilson", "Untagged" };
    private const string GAME_CONTROLLER_NAME = "GameController";
    private const string AUDIO_MANAGER_NAME = "AudioManager";

    private void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();
        audioManager = GameObject.Find(AUDIO_MANAGER_NAME).GetComponent<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (tag != other.tag && !excludedTags.Contains(other.tag))
        {
            // Check which game object awards points upon triggered, if any
            awardPoints = GetComponent<AwardPoints>() != null ? GetComponent<AwardPoints>() : other.GetComponent<AwardPoints>();

            // Give points based on the game object's assigned points
            if (awardPoints != null)
                gameController.UpdateScore(awardPoints.pointsAwarded);

            // Play sound effects
            if (other.tag == "Coconut")
                audioManager.PlaySoundEffect(audioManager.audioBonk);
            else if (other.tag == "Bird")
                audioManager.PlaySoundEffect(audioManager.audioSquawk);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
