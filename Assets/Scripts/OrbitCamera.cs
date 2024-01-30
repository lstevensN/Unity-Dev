using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbitCamera : MonoBehaviour
{
    [SerializeField] Transform target = null;
    [SerializeField] Transform spotlight = null;
    [SerializeField][Range(20, 90)] float defaultPitch = 40;
    [SerializeField][Range(2, 10)] float distance = 5;
    [SerializeField][Range(0.1f, 2.0f)] float sensitivity = 1;

    float yaw = 0;
    float pitch = 0;

    private void Start()
    {
        pitch = defaultPitch;
    }

    // Update is called once per frame
    void Update()
    {
        yaw += Input.GetAxis("Mouse X") * sensitivity;
        pitch -= Input.GetAxis("Mouse Y") * sensitivity;

        Quaternion qyaw = Quaternion.AngleAxis(yaw, Vector3.up);
        Quaternion qpitch = Quaternion.AngleAxis(pitch, Vector3.right);
        Quaternion rotation = qyaw * qpitch;

        //if (rotation.z < 0) rotation.z = 0;
        //if (rotation.x < 0) rotation.x = 0;

        transform.position = target.position + (rotation * Vector3.back * distance);

        //if (transform.position.y < target.position.y) transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);

        transform.rotation = rotation;

        if (spotlight) spotlight.rotation = rotation;
    }
}
