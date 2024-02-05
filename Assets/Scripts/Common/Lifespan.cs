using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lifespan : MonoBehaviour
{
    [SerializeField, Range(0, 10)] float lifespan = 1; 

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, lifespan);
    }
}
