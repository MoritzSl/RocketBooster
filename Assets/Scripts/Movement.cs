using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float roationSpeed = 100f;
    [SerializeField] AudioClip thrustSound;

    Rigidbody rb;
    AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
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
            setAudio(true);
        } 
        else
        {
            setAudio(false);
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

    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; // freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * Time.deltaTime * rotationThisFrame);
        rb.freezeRotation = false; // unfreezing rotation so the physics system can take over
    }

    void setAudio(bool toggle) 
    {
        if (toggle && !audioSource.isPlaying) 
        {
            audioSource.PlayOneShot(thrustSound);
        } 
        else if (!toggle && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
