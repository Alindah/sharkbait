using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowController : MonoBehaviour
{
    public GameObject coconut;

    private void OnDestroy()
    {
        // Detach coconut from destroyed bird and place it under grandparent
        coconut.transform.parent = gameObject.transform.parent;
        coconut.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        coconut.GetComponent<ShrinkObject>().enabled = true;
        coconut.GetComponent<DestroyOutOfBounds>().enabled = true;      // This must be reenabled as it will not trigger when bird is destroyed
    }
}
