using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("RAFT")]
    public float speed = 2.0f;
    public float xBoulderBoundary = 5.5f;
    public float zBoulderBoundary = 3.7f;
    public float xBoundary = 7.0f;
    public float zBoundary = 4.7f;
    public float drift = 100.0f;         // Drift from residual velocity 
    private float horizontalInput;
    private float verticalInput;
    private Rigidbody rb;

    [Header("WEAPON")]
    public GameObject weapon;
    public GameObject weaponContainer;
    public float weaponYAxis = -3.0f;   // Height at which weapon spawns - should be at same height as birds

    [Header("SURVIVOR")]
    public GameObject survivor;

    [Header("MISC")]
    public GameController gameController;
    public AudioManager audioManager;
    public GameObject border;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        // If borders are active, do not allow player to cross boundaries
        if (border.activeSelf)
            MoveRaftWithinBorders(xBoulderBoundary, zBoulderBoundary);
        else
            MoveRaftOppositeSide(xBoundary, zBoundary);

        MoveRaft();

        if (Input.GetButtonDown("Fire1") && !gameController.GetPauseStatus())
            ThrowSpear();
    }

    private void FixedUpdate()
    {
        // Let raft drift momentarily when player releases movement key
        rb.AddRelativeForce(new Vector3(horizontalInput, 0, verticalInput) * drift);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy" || other.tag == "Coconut")
            gameController.UpdateHealth(-1);

        if (other.tag == "Coconut")
            audioManager.PlaySoundEffect(audioManager.audioBonk);

        if (other.tag == "Enemy")
            audioManager.PlaySoundEffect(audioManager.audioBite);
    }

    // Do not allow player to cross past indicated boundaries
    private void MoveRaftWithinBorders(float xBoundary, float zBoundary)
    {
        if (transform.position.x > xBoundary)
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);
        else if (transform.position.x < -xBoundary)
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);

        if (transform.position.z > zBoundary)
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
        else if (transform.position.z < -zBoundary)
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
    }

    // Player will appear on other side of map if they reach the boundaries in either direction
    private void MoveRaftOppositeSide(float xBoundary, float zBoundary)
    {
        
        if (transform.position.x > xBoundary)
            transform.position = new Vector3(-xBoundary, transform.position.y, transform.position.z);
        else if (transform.position.x < -xBoundary)
            transform.position = new Vector3(xBoundary, transform.position.y, transform.position.z);

        if (transform.position.z > zBoundary)
            transform.position = new Vector3(transform.position.x, transform.position.y, -zBoundary);
        else if (transform.position.z < -zBoundary)
            transform.position = new Vector3(transform.position.x, transform.position.y, zBoundary);
    }

    private void MoveRaft()
    {
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
        // Only spawn weapon there is not another instance of it on the screen
        if (weaponContainer.transform.childCount < 1)
        {
            // Spawn weapon and rotate it towards the clicked position
            GameObject spawnedWeapon = Instantiate(weapon, transform.position, Quaternion.identity, weaponContainer.transform);
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector3 relativePos = new Vector3(mousePosition.x, 0, mousePosition.z) - spawnedWeapon.transform.position;
            Quaternion rotation = Quaternion.LookRotation(relativePos, new Vector3(0, 1, 0));
            spawnedWeapon.transform.rotation = rotation;
            spawnedWeapon.transform.rotation *= Quaternion.FromToRotation(Vector3.up, Vector3.forward);
            spawnedWeapon.transform.Translate(0, 0, weaponYAxis);
        }
    }
}
