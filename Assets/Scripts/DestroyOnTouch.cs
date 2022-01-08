using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag != "Ocean" && other.tag != "Player")
            Destroy(other.gameObject);
    }
}
