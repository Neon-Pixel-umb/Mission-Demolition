using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionBox : MonoBehaviour
{
    public float explosionRadius = 5f; // Radius of the explosion effect
    public float explosionForce = 10f; // Force of the explosion
    public GameObject explosionEffect; // Visual effect prefab (like a particle system)
    public LayerMask affectedLayers; // Layers that the explosion can affect

    void OnCollisionEnter2D(Collision2D collision) // Use OnCollisionEnter for 3D
    {
        // Check if the collision is with a projectile or a structure block
        if (collision.gameObject.CompareTag("Projectile") || collision.gameObject.CompareTag("Block"))
        {
            Explode();
        }
    }

    void Explode()
    {
        // Show explosion effect
        if (explosionEffect != null)
        {
            Instantiate(explosionEffect, transform.position, Quaternion.identity);
        }

        // Get all objects within the explosion radius
        Collider2D[] objectsInRange = Physics2D.OverlapCircleAll(transform.position, explosionRadius, affectedLayers); // Use Physics.OverlapSphere for 3D
        foreach (Collider2D obj in objectsInRange)
        {
            Rigidbody2D rb = obj.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply explosion force
                Vector2 explosionDirection = (rb.transform.position - transform.position).normalized;
                rb.AddForce(explosionDirection * explosionForce, ForceMode2D.Impulse);
            }
        }

        // Destroy the explosion box after exploding
        Destroy(gameObject);
    }

    // Visualize the explosion radius in the editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
