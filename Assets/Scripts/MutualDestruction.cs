using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutualDestruction : MonoBehaviour
{
    private AwardPoints awardPoints;
    private const string GAME_CONTROLLER_NAME = "GameController";

    private void OnTriggerEnter(Collider other)
    {
        if (tag != other.tag && other.tag != "Player" && other.tag != "Untagged")
        {
            // Check which game object awards points upon triggered, if any
            awardPoints = GetComponent<AwardPoints>() != null ? GetComponent<AwardPoints>() : other.GetComponent<AwardPoints>();

            // Give points based on the game object's assigned points
            if (awardPoints != null)
                GameObject.Find(GAME_CONTROLLER_NAME).GetComponent<GameController>().UpdateScore(awardPoints.pointsAwarded);

            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
