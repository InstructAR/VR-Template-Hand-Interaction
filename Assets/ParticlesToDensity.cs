using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlesToDensity : MonoBehaviour
{
    public WaterParticleManager waterParticleManager;
    public DensityField densityField;
    public Vector3 tempVector3;
    public float delay = 0.1f;
    private float distance;

    private void Start()
    {
        StartCoroutine("UpdateWater");
    }

    private IEnumerator UpdateWater()
    {
        yield return new WaitForSeconds(delay);

        for (int xi = 0; xi < densityField.dimension; xi++)
        {
            for (int yi = 0; yi < densityField.dimension; yi++)
            {
                for (int zi = 0; zi < densityField.dimension; zi++)
                {
                    densityField.SetDensity((0.2f - distance) * Time.deltaTime, xi, yi, zi);
                }
            }
        }
        if (densityField != null && waterParticleManager.particles.Count > 0)
        {
            foreach(GameObject particle in waterParticleManager.particles)
            {
                if(particle.transform.position.x < 0 || particle.transform.position.x >= 1.0f
                    || particle.transform.position.y < 0 || particle.transform.position.y >= 1.0f
                    || particle.transform.position.z < 0 || particle.transform.position.z >= 1.0f)
                {
                    continue;
                }
                for (int xi = 0; xi < densityField.dimension; xi++)
                {
                    for (int yi = 0; yi < densityField.dimension; yi++)
                    {
                        for (int zi = 0; zi < densityField.dimension; zi++)
                        {
                            tempVector3.x = xi;
                            tempVector3.y = yi;
                            tempVector3.z = zi;
                            distance = Mathf.Abs(Vector3.Distance(particle.transform.position, tempVector3 / 10.0f));
                            if(distance < 0.2f)
                            {
                                densityField.AddToDensity((0.2f - distance), xi, yi, zi);
                                Debug.Log((0.2f - distance));
                            //if (Vector3.Distance(targetFinger.position, targetThumb.position) < 0.01f && distance < 0.1f)
                                //densityField.AddToDensity((0.3f - distance) * Time.deltaTime, xi, yi, zi);
                            }
                        }
                    }
                }
            }
        }
        StartCoroutine("UpdateWater");
    }
}
