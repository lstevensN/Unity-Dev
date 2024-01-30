using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PhysicsCharacterController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField][Range(1, 20)] float maxForce = 5;
    [SerializeField][Range(1, 20)] float jumpForce = 10;
    [SerializeField] Transform view;

    [Header("Collision")]
    [SerializeField][Range(0, 5)] float rayLength = 1;
    [SerializeField] LayerMask groundLayerMask;

    private AudioSource audioSource;
    Rigidbody rb;
    Vector3 force = Vector3.zero;
    private bool OnGround = true;
    private bool slowFall = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = Vector3.zero;

        direction.x = Input.GetAxis("Horizontal");
        direction.z = Input.GetAxis("Vertical");

        Quaternion yrotation = Quaternion.AngleAxis(view.rotation.eulerAngles.y, Vector3.up);
        force = yrotation * direction * maxForce;

        if (Input.GetButtonDown("Jump") && CheckGround())
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            audioSource.Play();
        }

        if (rb.velocity.y != 0) OnGround = false;
        else OnGround = true;

        if (!OnGround && Input.GetMouseButton(0) && rb.velocity.y < 0) slowFall = true;
        else slowFall = false;
    }

    private void FixedUpdate()
    {
        rb.AddForce(force, ForceMode.Force);

        if (OnGround || slowFall) rb.velocity = new Vector3(rb.velocity.x * 0.96f, rb.velocity.y * 0.7f, rb.velocity.z * 0.96f);
    }

    private bool CheckGround()
    {
        Debug.DrawRay(transform.position, Vector3.down * rayLength, Color.red);
        return Physics.Raycast(transform.position, Vector3.down, rayLength, groundLayerMask);
    }

    public void Reset()
    {
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }
}
