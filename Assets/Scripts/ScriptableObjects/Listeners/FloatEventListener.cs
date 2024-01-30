using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FloatEventListener : MonoBehaviour
{
    [SerializeField] private FloatEvent _event = default;

    public UnityEvent<float> listener;

    private void OnEnable()
    {
        _event?.Subscribe(Respond);
    }

    private void OnDisable()
    {
        _event?.Unsubscribe(Respond);
    }

    private void Respond(float value)
    {
        listener?.Invoke(value);
    }
}
