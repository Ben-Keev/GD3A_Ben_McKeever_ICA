using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEventListener that listens for BoolGameEvent.
    /// Listens for events that carry a bool parameter and responds accordingly.
    /// </summary>
    /// <see cref="BoolGameEvent"/>
    [AddComponentMenu("GD/Events/Bool Event Listener")]
    public class BoolGameEventListener : BaseGameEventListener<bool>
    { }
}