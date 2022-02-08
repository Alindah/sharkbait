using System.Linq;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private const string COCONUT_TAG_NAME = "Coconut";
    private const string PU_STARFISH_NAME = "PickupStarfish";
    private const string PU_LIFE_NAME = "PickupLife";
    private const string WILSON_TAG = "Wilson";
    private const string GAME_CONTROLLER_NAME = "GameController";
    private GameStats gameStats;
    private string[] excludedTags = { "Player", WILSON_TAG, "Untagged" };

    void Start()
    {
        gameStats = GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameStats>();
    }

    private void OnTriggerEnter(Collider other)
    {
        switch (other.tag)
        {
            case COCONUT_TAG_NAME:
                gameStats.numOfSelfBonks++;
                break;
            case PU_STARFISH_NAME:
            case PU_LIFE_NAME:
                gameStats.pickupsCollected++;
                break;
            default:
                break;
        }

        if (other.tag == WILSON_TAG)
            gameStats.wilsonRescued = true;

        if (!excludedTags.Contains(other.tag))
            Destroy(other.gameObject);
    }
}
