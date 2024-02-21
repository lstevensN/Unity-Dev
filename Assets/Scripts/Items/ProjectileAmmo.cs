using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class ProjectileAmmo : Ammo
{
    [SerializeField] Action action;

    private void Start()
    {
        if (action != null)
        {
            action.onEnter += OnInteractStart;
            action.onStay += OnInteractActive;
        }

        RaycastHit hit;
        Ray target = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(target, out hit)) 
        {

            Vector3 point = hit.point;
            Vector3 force = (point - transform.position).normalized;
            transform.rotation = Quaternion.LookRotation(hit.point);

            if (force.z < 0f) { force = (transform.position - hit.point).normalized; force.y = -force.y; }

            if (ammoData.force != 0) GetComponent<Rigidbody>().AddRelativeForce(force * ammoData.force, ammoData.forceMode);
            Destroy(gameObject, ammoData.lifetime);
        }
    }
}
