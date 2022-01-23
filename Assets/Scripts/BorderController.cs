using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BorderController : MonoBehaviour
{
    private GameObject border;
    public float actionDuration = 10.0f;
    private const string BORDER_GAME_OBJ = "Border";

    void Start()
    {
        border = GameObject.Find(BORDER_GAME_OBJ);
    }

    public void DisableBorderTemporarily()
    {
        StartCoroutine("BorderTimer");
    }

    private IEnumerator BorderTimer()
    {
        border.SetActive(false);
        yield return new WaitForSeconds(actionDuration);
        border.SetActive(true);
    }
}
