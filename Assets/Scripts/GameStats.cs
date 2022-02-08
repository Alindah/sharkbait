using UnityEngine;

public class GameStats : MonoBehaviour
{
    public int seagullsKilled;
    public int swallowsKilled;
    public int sharksKilled;
    public int numOfSelfBonks;
    public int pickupsCollected;
    public bool wilsonRescued;

    void Start()
    {
        Init();
    }

    private void Init()
    {
        seagullsKilled = 0;
        swallowsKilled = 0;
        sharksKilled = 0;
        numOfSelfBonks = 0;
        pickupsCollected = 0;
        wilsonRescued = false;
    }
}
