using Unity.Cinemachine;
using UnityEngine;

public class Rock : MonoBehaviour
{
    [Header("Elements")]
    [SerializeField] private ParticleSystem collisionParticleSystem;
    [SerializeField] private AudioSource boulderSmashAudioSource;

    [Header("Settings")]
    [SerializeField] private float shakeModifer = 10f;
    [SerializeField] private float collisionCooldown = 1f;

    private CinemachineImpulseSource cinemachineImpulseSource;

    private float collisionTimer = 1f;

    private void Awake()
    {
        cinemachineImpulseSource = GetComponent<CinemachineImpulseSource>();
    }

    private void Update()
    {
        collisionTimer += Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collisionTimer < collisionCooldown) return;

        FireImpulse();
        CollisionFX(collision);
        collisionTimer = 0f;
    }

    private void FireImpulse()
    {
        float distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        float shakeIntensity = (1f / distance) * shakeModifer;
        shakeIntensity = Mathf.Min(shakeIntensity, 1f);
        cinemachineImpulseSource.GenerateImpulse(shakeIntensity);
    }

    private void CollisionFX(Collision collision)
    {
        ContactPoint contactPoint = collision.contacts[0];
        collisionParticleSystem.transform.position = contactPoint.point;
        collisionParticleSystem.Play();
        boulderSmashAudioSource.Play();
    }
}
