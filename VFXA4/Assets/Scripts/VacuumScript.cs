using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VacuumScript : MonoBehaviour
{
    public float suctionStrength = 5.0f;
    public float shapeZ = 10;
    public float shapeRad = 1.0f;
    public float uDEccentricity = 2.0f;
    public Vector2 sinFrequency = new Vector2(1, 1);
    public Vector2 sinMultiplier = Vector2.one;
    
    
    public Transform singularityTransform;

    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule particleSystemMain;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem.ShapeModule shape;

    private void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMain = particleSystem.main;
        particleSystemMain.simulationSpace = ParticleSystemSimulationSpace.World;
        shape = particleSystem.shape;

        particles = new ParticleSystem.Particle[particleSystemMain.maxParticles];
    }

    private void LateUpdate()
    {
        // Update shape and radius if they change
        if (shapeZ != shape.position.z || shapeRad != shape.radius)
        {
            shape.position = Vector3.forward * shapeZ;
            shape.radius = shapeRad;
        }
        
        // Set start lifetime to be relative to shape and suction strength
        particleSystemMain.startLifetime = shapeZ/suctionStrength;

        int numberOfParticles = particleSystem.GetParticles(particles);

        Vector3 positionDelta, velocity;
        float uD;

        for (int i = 0; i < numberOfParticles; i++)
        {
            // Destroy particles at target
            if (particles[i].position == singularityTransform.position)
            {
                particles[i].remainingLifetime = 0;
                continue;
            }
            
            //positionDelta = particles[i].position;
            
            // Get position delta from singularity transform
            positionDelta = singularityTransform.position - particles[i].position;
            
            // Get normalized delta
            velocity = -positionDelta.normalized;
            
            // Scale velocity
            velocity *= -suctionStrength / velocity.z;
            uD = particles[i].position.z / shapeZ;
            uD = Mathf.Pow(uD, uDEccentricity);
            
            // Apply sin waves for   d a t   w a v e y   f e e l
            velocity.x += positionDelta.x * suctionStrength * Time.deltaTime; //sinMultiplier.x * uD * Mathf.Sin(Time.time * sinFrequency.x + positionDelta.z) * suctionStrength;
            velocity.y += positionDelta.z * suctionStrength * Time.deltaTime; //sinMultiplier.y * uD * Mathf.Sin(Time.time * sinFrequency.y + positionDelta.z) * suctionStrength;
            velocity.z += positionDelta.y * suctionStrength * Time.deltaTime;
            
            // Assign velocity
            particles[i].velocity = velocity;
        }
        
        // Update particles
        particleSystem.SetParticles(particles, numberOfParticles);
    }
}
