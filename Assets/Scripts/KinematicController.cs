using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class KinematicController : MonoBehaviour, Damage.IDamagable
{
    [SerializeField, Range(0, 40)] float speed = 1;
    [SerializeField] float maxDistance = 5;

    // public float health = 100;

    public void ApplyDamage(float damage)
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.y = Input.GetAxis("Vertical");

        Vector3 force = direction * speed * Time.deltaTime;
        transform.localPosition += force;

        transform.localPosition = Vector3.ClampMagnitude(transform.localPosition, maxDistance);
        // transform.localRotation = new Quaternion(-direction.y * 0.2f, direction.x * 0.2f, 0, 1);

        Quaternion qyaw = Quaternion.AngleAxis(direction.x * 20, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(-direction.y * 20, Vector3.right);

        Quaternion rotation = qyaw * qpitch;
        transform.localRotation = Quaternion.Slerp(transform.localRotation, rotation, Time.deltaTime * 10);
    }
}
