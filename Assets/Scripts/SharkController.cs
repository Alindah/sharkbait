using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SharkController : MonoBehaviour
{
    public float speed = 1.0f;
    public float yFinalPosition = -0.12f;
    public float riseSpeed = 0.01f;
    private GameObject target;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
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
            transform.Translate(0, riseSpeed, 0);
        }
    }
}
