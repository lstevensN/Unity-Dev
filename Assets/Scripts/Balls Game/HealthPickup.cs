using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class HealthPickup : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] GameObject pickupPrefab = null;

    [Header("Properties")]
    [SerializeField, Range(20, 100)] float healthGiven;
    [SerializeField] FloatVariable health;
    [SerializeField] StringVariable description;

    private AudioSource audioSource = null;
    private bool destroy;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (destroy && !audioSource.isPlaying) gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            health.value += healthGiven;
            description.value = "HP healed.";
            audioSource.Play();

            if (health.value > 100) health.value = 100;

            Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            GetComponent<MeshRenderer>().enabled = false;
            GetComponent<Collider>().enabled = false;
            GetComponent<Light>().enabled = false;
            destroy = true;
            
        }
    }

    public void OnGameStart()
    {
        gameObject.SetActive(true);
        GetComponent<MeshRenderer>().enabled = true;
        GetComponent<Collider>().enabled = true;
        GetComponent<Light>().enabled = true;
        destroy = false;
    }
}
