using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterParticleManager : MonoBehaviour
{
    public ObjectPool particlePool;
    public LinkedList<GameObject> particles = new LinkedList<GameObject>();
    GameObject tempGameObject;
    public int maxParticles = 33;
    public ParticlesToDensity particlesToDensity;

    public void CreateParticle(Vector3 position)
    {
        //Withdraw one, or use the oldest one.
        if (particles.Count < maxParticles)
        {
            tempGameObject = particlePool.Withdraw();
        }
        else
        {
            tempGameObject = particles.First.Value;
            particles.RemoveFirst();
        }

        //particles.AddLast(tempGameObject);
        tempGameObject.SetActive(true);
        tempGameObject.transform.position = position;
        Debug.Log("Particles: " + particles.Count);
    }

    public void RemoveParticle(GameObject particle)
    {
        particlePool.Deposit(particle);
        particles.Remove(particle);
        Debug.Log("Particles: " + particles.Count);
    }
}
