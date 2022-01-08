using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwallowController : MonoBehaviour
{
    public GameObject coconut;

    private void OnDestroy()
    {
        coconut.transform.parent = null;
        coconut.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        coconut.GetComponent<ShrinkObject>().enabled = true;
    }
}
