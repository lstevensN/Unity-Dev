using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class TimePickup : MonoBehaviour
{
    [Header("VFX")]
    [SerializeField] GameObject pickupPrefab = null;

    [Header("Variables")]
    [SerializeField] FloatVariable timer;
    [SerializeField] StringVariable description;
    [SerializeField, Range(20, 30)] float timeGiven;

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
            timer.value += timeGiven;
            description.value = "Timer extended.";
            audioSource.Play();

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
