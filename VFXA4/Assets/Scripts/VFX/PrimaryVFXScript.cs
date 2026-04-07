using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrimaryVFXScript : MonoBehaviour
{
    public Transform singularityTransform;
    public float particleVelocityMultiplier = 1.0f;
    public float lifeTimeMultiplier = 2.0f;
    
    private ParticleSystem particleSystem;
    private ParticleSystem.MainModule particleSystemMain;
    private ParticleSystem.Particle[] particles;
    private ParticleSystem.ShapeModule shape;
    
    
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        particleSystemMain = particleSystem.main;
        particleSystemMain.simulationSpace = ParticleSystemSimulationSpace.World;
        
        particles = new ParticleSystem.Particle[particleSystemMain.maxParticles];
    }

    void LateUpdate()
    {
        particleSystemMain.startLifetime = 1 * lifeTimeMultiplier;
        
        int numberOfParticles = particleSystem.GetParticles(particles);

        Vector3 dir, vel;

        for (int i = 0; i < numberOfParticles; i++)
        {
            
            
            /*
            // Destroy particle within range of singularity
            if (bHelper.AtPositionInRange(singularityTransform, particles[i].position, 0.5f))
            {
                particles[i].remainingLifetime = 0;
                continue;
            }
            */
            
            dir = bHelper.GetNormalizedDir(particles[i].position, singularityTransform.position);

            if (Vector3.Dot(dir, singularityTransform.position - particles[i].position) <= 0)
            {
                particles[i].remainingLifetime = 0;
                continue;
            }
            
            vel = dir * particleVelocityMultiplier;
            
            particles[i].velocity += vel * Time.deltaTime;
        }
        
        particleSystem.SetParticles(particles, numberOfParticles);
    }
}
