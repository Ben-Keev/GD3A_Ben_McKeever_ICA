using GD.Items;
using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEvent that carries an int parameter.
    /// Used to create an integer-based event that can be raised and responded to.
    /// </summary>
    [CreateAssetMenu(fileName = "NPCGameEvent",
        menuName = "GD/Events/Params/NPC",
        order = 5)]
    public class NPCGameEvent : BaseGameEvent<NPCData>
    { }
}