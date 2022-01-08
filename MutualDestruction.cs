using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MutualDestruction : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (tag != other.tag && other.tag != "Player")
        {
            Destroy(gameObject);
            Destroy(other.gameObject);
        }
    }
}
