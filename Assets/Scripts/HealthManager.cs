using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int lives = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (tag == "Player" && (other.tag == "Enemy" || other.tag == "Coconut"))
        {
            lives--;

            if (lives <= 0)
                Debug.Log("YOU DIED");
        }
    }


}
