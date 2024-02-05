using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Damage : MonoBehaviour
{
    [Header("Properties")]
    [SerializeField] float damage = 1;
    [SerializeField] bool oneTime = true;

    [Header("Events")]
    [SerializeField] FloatEvent damageEvent;

    private void OnTriggerEnter(Collider other)
    {
        if (oneTime && other.gameObject.TryGetComponent<Player>(out Player player))
        {
             damageEvent.RaiseEvent(damage);
        }     
    }

    private void OnTriggerStay(Collider other)
    {
        if (!oneTime && other.gameObject.TryGetComponent<Player>(out Player player))
        {
            damageEvent.RaiseEvent(damage * Time.deltaTime);
        }
    }

    public interface IDamagable
    {
        void ApplyDamage(float damage);
    }
}
