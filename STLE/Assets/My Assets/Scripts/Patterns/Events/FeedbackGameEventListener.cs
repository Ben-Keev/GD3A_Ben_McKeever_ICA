using GD.Items;
using GD.Types;
using System;
using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEventListener that listens for FeedbackGameEvent.
    /// Listens for events that carry an int parameter and responds accordingly.
    /// </summary>
    /// <see cref="FeedbackGameEvent"/>
    [AddComponentMenu("GD/Events/Feedback Event Listener")]
    public class FeedbackGameEventListener : BaseGameEventListener<Tuple<ItemCategoryType, ItemData, FeedbackType>>
    { }
}