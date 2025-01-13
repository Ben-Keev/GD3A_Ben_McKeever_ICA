using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEvent that carries an bool parameter.
    /// Used to create an bool-based event that can be raised and responded to.
    /// </summary>
    [CreateAssetMenu(fileName = "BoolGameEvent",
        menuName = "GD/Events/Params/Bool",
        order = 2)]
    public class BoolGameEvent : BaseGameEvent<bool>
    { }
}