using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidStopper : MonoBehaviour
{
    [SerializeField] VoidEvent stopAsteroids;

    private void OnTriggerEnter(Collider other)
    {
        stopAsteroids.RaiseEvent();
    }
}
