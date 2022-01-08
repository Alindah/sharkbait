using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthManager : MonoBehaviour
{
    public int lives = 3;

    private void OnTriggerEnter(Collider other)
    {
        if (tag != "Enemy" && (other.tag == "Enemy" || other.tag == "Coconut"))
            lives--;
    }
}
