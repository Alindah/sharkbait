using System.Linq;
using UnityEngine;

public class DestroyOnTouch : MonoBehaviour
{
    private string[] excludedTags = { "Player", "Wilson", "Untagged" };

    private void OnTriggerEnter(Collider other)
    {
        if (!excludedTags.Contains(other.tag))
            Destroy(other.gameObject);
    }
}
