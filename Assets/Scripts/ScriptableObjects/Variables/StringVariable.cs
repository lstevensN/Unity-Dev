using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Variables/String")]
public class StringVariable : ScriptableObject, ISerializationCallbackReceiver
{
    public string initialValue;

    [NonSerialized]
    public string value;

    public void OnAfterDeserialize()
    {
        value = initialValue;
    }

    public void OnBeforeSerialize()
    {
        //
    }
}
