using System;
using UnityEngine;

/// <summary>
/// On call by a game event spawns a particle at a given transform.
/// </summary>
public class ParticleManager : MonoBehaviour
{
    /// <summary>
    /// Instantiate a particle at a given spot
    /// </summary>
    /// <param name="particleData.Item1">Location where item will spawn</param>
    /// <param name="particleData.Item2">What sort of particle (May give feedback or indicate item type)</param>
    public void instantiateParticle(Tuple<Transform, Enum> particleData)
    {
        Material icon = Resources.Load<Material>("Materials/" + particleData.Item2.ToString());

        // https://discussions.unity.com/t/instantiate-an-object-to-a-specific-position/31993
        GameObject particle = Instantiate(Resources.Load<GameObject>("Particle"), particleData.Item1.position, transform.rotation);

        particle.GetComponent<ParticleSystemRenderer>().material = icon;
    }
}
