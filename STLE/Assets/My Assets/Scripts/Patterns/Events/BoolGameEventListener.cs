using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEventListener that listens for IntGameEvent.
    /// Listens for events that carry an int parameter and responds accordingly.
    /// </summary>
    /// <see cref="IntGameEvent"/>
    [AddComponentMenu("GD/Events/Bool Event Listener")]
    public class BoolGameEventListener : BaseGameEventListener<bool>
    { }
}