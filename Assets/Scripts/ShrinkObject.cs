using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShrinkObject : MonoBehaviour
{
    public Vector3 shrinkRate;
    public float minSize;

    void Update()
    {
        // Shrink object until its minimum size
        if (transform.localScale.x > minSize)
            transform.localScale -= shrinkRate * Time.deltaTime;
    }
}
