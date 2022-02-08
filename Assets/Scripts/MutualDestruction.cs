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
    private const string SEAGULL_NAME = "Seagull";
    private const string SWALLOW_NAME = "Swallow";
    private const string WEAPON_TAG_NAME = "Weapon";
    private const string ENEMY_TAG_NAME = "Enemy";
    private const string COCONUT_TAG_NAME = "Coconut";
    private GameStats gameStats;

    private void Start()
    {
        gameController = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>();
        audioManager = GameObject.Find(AUDIO_MANAGER_NAME).GetComponent<AudioManager>();
        gameStats = gameController.gameObject.GetComponent<GameStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Increase count for bird killed
        if (tag == WEAPON_TAG_NAME && other.gameObject.name.Contains(SEAGULL_NAME))
            gameStats.seagullsKilled++;
        else if (tag == WEAPON_TAG_NAME && other.gameObject.name.Contains(SWALLOW_NAME))
            gameStats.swallowsKilled++;

        // Increase count for sharks killed
        if (tag == ENEMY_TAG_NAME && other.tag == COCONUT_TAG_NAME)
            gameStats.sharksKilled++;

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
