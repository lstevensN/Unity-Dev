using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Descend : MonoBehaviour
{
    [SerializeField, Range(1, 10)] float time = 3;

    private float activateTimer = 0;
    private AudioSource ads;

    [SerializeField] StringVariable description;

    // Update is called once per frame
    void Update()
    {
        if (activateTimer > 0)
        {
            activateTimer -= Time.deltaTime;

            if (activateTimer < 0.3f) description.value = "";
            else description.value = "-- Safe to Descend! --";
        }
    }

    public void OnActivate()
    {
        ads = GetComponent<AudioSource>();
        activateTimer = time;
        ads.Play();
    }
}
