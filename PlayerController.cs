using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Raft Variables
    public float speed = 2.0f;
    public float xBoundary = 7.0f;
    public float zBoundary = 4.7f;
    public float drift = 100.0f;         // Drift from residual velocity 
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    // Weapon Variables
    public GameObject weapon;
    public float weaponYAxis = -3.0f;   // Height at which weapon spawns - should be at same height as birds

    // Survivor Variables
    public GameObject survivor;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveRaft();
        ThrowSpear();
    }

    private void FixedUpdate()
    {
        // Let raft drift momentarily when player releases movement key
        rb.AddRelativeForce(new Vector3(horizontalInput, 0, verticalInput) * drift);
    }

    private void MoveRaft()
    {
        // Player will appear on other side of map if they reach the xBoundary in either direction
        if (transform.position.x > xBoundary)
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        else if (transform.position.x < -xBoundary)
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);

        // Player will appear on other side of map if they reach the zBoundary in either direction
        if (transform.position.z > zBoundary)
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        else if (transform.position.z < -zBoundary)
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);

        // Get player inputs
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        // Move the player in direction they pressed
        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            float horizontalSpeed = Time.deltaTime * horizontalInput * speed;
            float verticalSpeed = Time.deltaTime * verticalInput * speed;

            // Check if player is trying to propel in different X direction from drift and slow down raft if so
            if (rb.velocity.x < 0 && horizontalInput > 0 || rb.velocity.x > 0 && horizontalInput < 0)
                rb.velocity = new Vector3(rb.velocity.x + horizontalSpeed, 0, rb.velocity.z + verticalSpeed);
            else
                transform.Translate(Vector3.right * horizontalSpeed);

            // Check if player is trying to propel in different Z direction from drift and slow down raft if so
            if (rb.velocity.z < 0 && verticalInput > 0 || rb.velocity.z > 0 && verticalInput < 0)
                rb.velocity = new Vector3(rb.velocity.x + horizontalSpeed, 0, rb.velocity.z + verticalSpeed);
            else
                transform.Translate(Vector3.forward * verticalSpeed);
        }
    }

    private void ThrowSpear()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            // Spawn weapon and rotate it towards the clicked position
            GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 relativePos = new Vector3(mousePosition.x, 0, mousePosition.z) - spawnedWeapon.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
            spawnedWeapon.transform.rotation = rotation;
            spawnedWeapon.transform.rotation *= Quaternion.FromToRotation(Vector3.up, Vector3.forward);
            spawnedWeapon.transform.Translate(0, 0, weaponYAxis);
        }
    }
}
