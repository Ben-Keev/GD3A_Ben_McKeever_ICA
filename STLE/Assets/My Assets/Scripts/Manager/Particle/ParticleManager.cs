using GD.Items;
using GD.Types;
using System;
using Unity.VisualScripting;
using UnityEngine;

public class ParticleManager : MonoBehaviour
{
    /// <summary>
    /// Gives feedback as to whether the player made the right choice
    /// </summary>
    /// <param name="bin"></param>
    public void instantiateParticle(Tuple<Transform, Enum> particleData)
    {
        Debug.Log(particleData.Item2.ToString());

        Material icon = Resources.Load<Material>("Materials/" + particleData.Item2.ToString());

        // https://discussions.unity.com/t/instantiate-an-object-to-a-specific-position/31993
        GameObject particle = Instantiate(Resources.Load<GameObject>("Particle"), particleData.Item1.position, transform.rotation);

        particle.GetComponent<ParticleSystemRenderer>().material = icon;
    }
}
