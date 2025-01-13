using System;
using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEvent that carries an item's transform and enum indicating what material to use.
    /// Used to create a particle-based event that can be raised and responded to.
    /// </summary>
    [CreateAssetMenu(fileName = "ParticleGameEvent",
        menuName = "GD/Events/Params/Particle",
        order = 2)]
    public class ParticleGameEvent : BaseGameEvent<Tuple<Transform, Enum>>
    { }
}