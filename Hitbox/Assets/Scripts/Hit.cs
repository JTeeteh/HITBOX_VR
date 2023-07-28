using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : MonoBehaviour
{
    public AudioClip hitSound; // The audio clip to play when the enemy is hit
    public ParticleSystem hitParticleEffect; // Reference to the Particle System

    // This method is called when a collision occurs
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with a GameObject tagged as "Bullet"
        if (collision.gameObject.CompareTag("Bullet"))
        {
            // Get the AudioSource component attached to this enemy GameObject
            AudioSource audioSource = GetComponent<AudioSource>();

            // Play the hitSound immediately without any delay
            audioSource.PlayOneShot(hitSound);

            // Get the contact point of the collision (where the bullet hit)
            Vector3 hitPoint = collision.contacts[0].point;

            // Play the particle effect at the hitPoint position
            if (hitParticleEffect != null)
            {
                // Create a new instance of the particle effect at the hitPoint position
                ParticleSystem effectInstance = Instantiate(hitParticleEffect, hitPoint, Quaternion.identity);

                // Play the particle effect
                effectInstance.Play();

                // Destroy the particle effect instance after it has finished playing
                Destroy(effectInstance.gameObject, effectInstance.main.duration);
            }

            // You can add other logic here, such as reducing enemy health or applying damage
            // For example, you could access an EnemyHealth script and call a TakeDamage method
            // to apply damage to the enemy.
            // collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(damageAmount);

            // Disable the bullet GameObject so that it no longer affects the scene
            collision.gameObject.SetActive(false);
        }
    }
}
