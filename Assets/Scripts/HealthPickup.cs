using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    [Range(5, 100)]
    public int healingAmount = 20;

    [SerializeField] GameObject pickupPrefab = null;
    [SerializeField] IntEvent healthPickupEvent = null;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Player player))
        {
            healthPickupEvent.RaiseEvent(healingAmount);
        }

        Instantiate(pickupPrefab, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
