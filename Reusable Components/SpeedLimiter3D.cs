﻿using UnityEngine;

/// <summary>
/// Limits the speed at which any 2D physics object with a rigidbody can travel. This can help avoid issues with physics objects "teleporting" (traveling so fast that they move past other objects in a frame an fail to interact them). This isn't a hard limit, but does dampen speeds above the max speed.
/// </summary>

[RequireComponent(typeof(Rigidbody))]
public class SpeedLimiter3D : MonoBehaviour
{
    public float maxSpeed = 10f;
    [Range(0.1f, 10f)]
    public float dampeningFactor = 5f;

    private Rigidbody rigidBody;

    /// <summary>
    /// Method does not directly set the velocity of the object as doing so interferes with the physics engine. It instead applies a counter force to the object to balance the speed out to the maximum value.
    /// </summary>
    public void LimitMaxVelocity()
    {
        float currentSpeed = Vector3.Magnitude(rigidBody.velocity);

        if (currentSpeed > maxSpeed)
        {
            float speedDifference = (currentSpeed - maxSpeed) / currentSpeed;
            Vector3 brakeForce = -rigidBody.velocity * speedDifference;
            rigidBody.AddForce(brakeForce * dampeningFactor);
        }
    }

    void Awake()
    {
        rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        LimitMaxVelocity();
    }
}
