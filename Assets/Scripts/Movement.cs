using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] float mainThrust = 100f;
    [SerializeField] float roationSpeed = 100f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust() 
    {
        if (Input.GetKey(KeyCode.Space)) 
        {
            rb.AddRelativeForce(Vector3.up * Time.deltaTime * mainThrust);
        }
    }

    void ProcessRotation() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(roationSpeed);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            ApplyRotation(-roationSpeed);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }
}
