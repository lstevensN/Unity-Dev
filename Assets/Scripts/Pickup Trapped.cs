using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupTrapped : MonoBehaviour
{
    [SerializeField] VoidEvent alertPelican = default;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            alertPelican.RaiseEvent();
        }
    }
}
