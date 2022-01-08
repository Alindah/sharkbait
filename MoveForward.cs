using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    public float speed = 5.0f;
    public Vector3 direction = Vector3.forward;

    void Update()
    {
        transform.Translate(direction * Time.deltaTime * speed);
    }
}
