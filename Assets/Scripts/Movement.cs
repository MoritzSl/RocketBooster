using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float mainThrust = 100f;
    [SerializeField] float roationSpeed = 100f;
    [SerializeField] AudioClip thrustSound;
    [SerializeField] ParticleSystem mainEngineParticle;
    [SerializeField] ParticleSystem leftThrusterParticle;
    [SerializeField] ParticleSystem rightThrusterParticle;

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
            playParticles(mainEngineParticle);
        } 
        else
        {
            setAudio(false);
            mainEngineParticle.Stop();
        }
    }

    void ProcessRotation() 
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(roationSpeed);
            playParticles(rightThrusterParticle);
            setAudio(true);
        }
        else if (Input.GetKey(KeyCode.D)) 
        {
            rightThrusterParticle.Stop();
            ApplyRotation(-roationSpeed);
            playParticles(leftThrusterParticle);
            setAudio(true);
        }
        else 
        {
            rightThrusterParticle.Stop();
            leftThrusterParticle.Stop();
            setAudio(false);
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

    void playParticles(ParticleSystem particleSystem)
    {
        if (!particleSystem.isPlaying)
        {
            particleSystem.Play();
        }
    }
}
