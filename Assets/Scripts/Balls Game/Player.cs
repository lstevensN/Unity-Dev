using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] PhysicsCharacterController characterController = default;
    [SerializeField] OrbitCamera cameraController = default;

    [Header("Properties")]
    [SerializeField] Transform respawnLocation = default;

    [Header("Game Elements")]
    [SerializeField] Light powerLight = default;
    [SerializeField] GameObject damageFX = default;

    private Collider collide;

    public void Start()
    {
        collide = GetComponent<Collider>();

        characterController.enabled = false;
        cameraController.enabled = false;
    }

    public void Update()
    {
        // wrapping
        if (transform.position.x <= -100) transform.position = new Vector3(100, transform.position.y, transform.position.z);
        else if (transform.position.x >= 100) transform.position = new Vector3(-100, transform.position.y, transform.position.z);

        if (transform.position.z <= -100) transform.position = new Vector3(transform.position.x, transform.position.y, 100);
        else if (transform.position.z >= 100) transform.position = new Vector3(transform.position.x, transform.position.y, -100);
    }


    // Event Listener Functions
    public void OnStartGame()
    {
        OnRespawn();
    }

    public void OnWinGame()
    {
        cameraController.enabled = false;
    }

    public void OnLoseGame()
    {
        characterController.enabled = false;
        cameraController.enabled = false;

        collide.enabled = false;
    }

    public void OnDead()
    {
        characterController.enabled = false;
        cameraController.enabled = false;
    }

    public void OnRespawn()
    {
        transform.position = respawnLocation.position;
        transform.rotation = respawnLocation.rotation;

        collide.enabled = true;
        characterController.enabled = true;
        cameraController.enabled = true;
        characterController.Reset();
    }

    public void OnScore(int points)
    {
        powerLight.intensity += points * 0.1f;
    }

    public void OnDamage(float damage)
    {
        Instantiate(damageFX, transform.position, Quaternion.identity);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("KillZone"))
        {
            collide.enabled = false;
        }
    }
}
