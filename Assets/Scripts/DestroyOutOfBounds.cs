using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    public float xBoundary = 10.0f;
    public float yBoundary = 10.0f;
    public float zBoundary = 9.0f;

    void Update()
    {
        if (Mathf.Abs(transform.position.x) > xBoundary || Mathf.Abs(transform.position.y) > yBoundary || Mathf.Abs(transform.position.z) > zBoundary)
            Destroy(gameObject);
    }

    // This cleans up objects that were not cleaned up on quit
    private void OnApplicationQuit()
    {
        Destroy(gameObject);
    }
}
