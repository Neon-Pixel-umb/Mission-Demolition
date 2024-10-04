using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryRenderer : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public int resolution = 30; // Number of segments to draw
    public float launchForce = 10f; // Adjust based on your projectile force
    public Transform slingshotAnchor; // The point where the slingshot is anchored

    void Start()
    {
        // Get the LineRenderer if not set
        if (lineRenderer == null)
            lineRenderer = GetComponent<LineRenderer>();
    }

    // Call this function to update the trajectory when dragging
    public void RenderTrajectory(Vector2 launchVelocity)
{
    lineRenderer.positionCount = resolution;
    Vector3[] points = new Vector3[resolution];

    // Set the starting point to the anchor position
    Vector2 startPoint = slingshotAnchor.position;
    Vector2 velocity = launchVelocity;

    for (int i = 0; i < resolution; i++)
    {
        // Calculate the position at each point using physics equations
        float time = i * 0.05f; // Lower the time increment (e.g., 0.05f) to shorten the line
        points[i] = startPoint + velocity * time + 0.5f * Physics2D.gravity * time * time;
    }

    lineRenderer.SetPositions(points);
}


    // Call this function to clear the trajectory
    public void ClearTrajectory()
    {
        lineRenderer.positionCount = 0;
    }
}

