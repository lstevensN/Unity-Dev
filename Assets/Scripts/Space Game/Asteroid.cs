using cakeslice;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    [SerializeField] private Transform scale;

    private float force;

    private void Start()
    {
        // Random Force
        if (force == 0) force = Random.Range(600f, 2500f);
        body.AddRelativeForce(-Vector3.forward * force);

        // Random Size
        float s = Random.Range(0.1f, 1.5f);
        scale.localScale *= s;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Transport"))
        {
            Destroy(this.gameObject);
        }
    }
}
