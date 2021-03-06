using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public float speed = 1.0f;
    public float yFinalPosition = -0.12f;
    public float riseSpeed = 1.0f;
    private GameObject target;
    private AudioManager audioManager;
    private const string AUDIO_MANAGER_NAME = "AudioManager";

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        audioManager = GameObject.Find(AUDIO_MANAGER_NAME).GetComponent<AudioManager>();
        audioManager.PlaySoundEffect(audioManager.audioJaws);
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < yFinalPosition)
        {
            // Raise shark to top of water to allow player some time to move away from it once is spawns
            RaiseShark();
        }
        else
        {
            // Once fully risen, make shark chase player
            Vector3 updatedPosition = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            transform.position = new Vector3(updatedPosition.x, yFinalPosition, updatedPosition.z);     // yFinalPosition ensures shark remains at same level even when bumped by another shark
        }
    }

    private void RaiseShark()
    {
        float currentY = transform.position.y;
        
        // Move shark up until it reaches its final Y position
        if (currentY + riseSpeed >= yFinalPosition)
        {
            transform.position = new Vector3(transform.position.x, yFinalPosition, transform.position.z);
            transform.GetChild(0).gameObject.SetActive(false);      // Turn off detection light
        }
        else
        {
            transform.Translate(0, riseSpeed * Time.deltaTime, 0);
        }
    }
}
