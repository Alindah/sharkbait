using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTarget : MonoBehaviour
{
    public GameObject target;
    public Vector3 orientation;
    public Vector3 correctOrientation;
    private Vector3 targetPosition;

    void Update()
    {
        targetPosition = target == null ? Camera.main.ScreenToWorldPoint(Input.mousePosition) : target.transform.position;
        Vector3 relativePos = targetPosition - transform.position;
        relativePos.y = 0;      // Set y to 0 to prevent object tilting
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = rotation;

        // Fix the rotation of the object if it is facing the wrong way
        if (orientation != Vector3.zero)
            transform.rotation *= Quaternion.FromToRotation(orientation, correctOrientation);
    }
}
