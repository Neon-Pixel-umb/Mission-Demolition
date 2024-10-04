using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlingshotController : MonoBehaviour
{
    public GameObject projectilePrefab;  // The prefab for the projectile (ball or rock)
    public Transform slingshotSpawnPoint;  // The spawn point for the projectile

    private GameObject currentProjectile;  // The currently active projectile
    public Transform slingshotTransform;  // Reference to the slingshot's transform
    public TrajectoryRenderer trajectoryRenderer; // Reference to the trajectory renderer

    public Transform slingshotAnchor;  // Where the slingshot band is anchored
    public float maxStretch = 3.0f;  // Maximum stretch allowed
    public float launchPower = 10f; // Adjust this value to control launch strength

    private Rigidbody2D rb;
    private bool isDragging = false;
    private LineRenderer lineRenderer;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        lineRenderer = FindObjectOfType<LineRenderer>();

        // Ensure the line renderer has at least 2 positions
        lineRenderer.positionCount = 2;
    }

    void Update()
    {
        if (isDragging)
        {
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 direction = mousePos - (Vector2)slingshotTransform.position;

            // Limit the stretch to the maxStretch distance
            if (direction.magnitude > maxStretch)
            {
                direction = direction.normalized * maxStretch; // Clamp the distance
            }

            // Update the projectile position based on the mouse position
            currentProjectile.transform.position = (Vector2)slingshotTransform.position + direction;

            // Update the line renderer to visually represent the band
            if (lineRenderer != null && lineRenderer.positionCount >= 2)
            {
                lineRenderer.SetPosition(0, slingshotTransform.position); // Start at the bandholder
                lineRenderer.SetPosition(1, currentProjectile.transform.position); // End at the projectile
            }

            // Update the trajectory line
            trajectoryRenderer.RenderTrajectory(-direction * launchPower);
        }
    }

    void OnMouseDown()
    {
        // If there's no projectile, create a new one
        if (currentProjectile == null)
        {
            // Instantiate a new projectile at the slingshot's position
            currentProjectile = Instantiate(projectilePrefab, slingshotTransform.position, Quaternion.identity);
            rb = currentProjectile.GetComponent<Rigidbody2D>();
            rb.isKinematic = true; // Disable physics while dragging
        }

        isDragging = true; // Start dragging
    }

    void OnMouseUp()
    {
        isDragging = false;

        // Enable physics and launch the projectile
        rb.isKinematic = false;
        Vector2 launchDirection = (slingshotTransform.position - currentProjectile.transform.position);
        rb.AddForce(launchDirection * launchPower, ForceMode2D.Impulse);

        // Clear the line renderer positions after launching
        if (lineRenderer != null && lineRenderer.positionCount >= 2)
        {
            lineRenderer.SetPosition(0, slingshotTransform.position);
            lineRenderer.SetPosition(1, slingshotTransform.position);
        }

        // Clear the trajectory line after launching
        trajectoryRenderer.ClearTrajectory();

        // Optionally destroy the projectile after some time to clean up
        Destroy(currentProjectile, 15f); // Destroys the projectile after 15 seconds (optional)
        
        currentProjectile = null; // Reset the current projectile reference
    }
}
