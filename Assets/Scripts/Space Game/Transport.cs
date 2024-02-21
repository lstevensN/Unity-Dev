using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Transport : MonoBehaviour
{
    [SerializeField] Camera mainCamera;
    [SerializeField] Camera endCamera;
    [SerializeField] Transform spline;

    [SerializeField] FloatVariable tHealth;

    private void OnCollisionEnter(Collision collision)
    {
        tHealth.value -= 10;

        Debug.Log("Transport hit!");
    }

    private void Start()
    {
        mainCamera.enabled = true;
        endCamera.enabled = false;
    }

    private void Update()
    {
        if (spline.position.z > 300)
        {
            endCamera.enabled = true;
            mainCamera.enabled = false;
        }
    }
}
