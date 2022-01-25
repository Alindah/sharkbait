using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public GameObject audioBonk;
    public GameObject audioBite;
    public GameObject audioSquawk;
    public GameObject audioSplash;
    public GameObject audioJaws;
    public GameObject audioPickupLife;
    public GameObject audioPickupBorder;

    private float destroyDelayTime = 5.0f;

    public void PlaySoundEffect(GameObject audioPrefab)
    {
        GameObject audioObj = Instantiate(audioPrefab, transform);
        StartCoroutine("DestroyAfterDelay", audioObj);
    }

    private IEnumerator DestroyAfterDelay(GameObject obj)
    {
        yield return new WaitForSeconds(destroyDelayTime);
        Destroy(obj);
    }
}
