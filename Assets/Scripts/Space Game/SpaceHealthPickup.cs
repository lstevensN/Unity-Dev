using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceHealthPickup : MonoBehaviour
{
    [SerializeField] float health;
    [SerializeField] GameObject pickupPrefab;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out PlayerShip player))
        {
            player.AppyHealth(health);
            if (pickupPrefab != null) Instantiate(pickupPrefab, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
