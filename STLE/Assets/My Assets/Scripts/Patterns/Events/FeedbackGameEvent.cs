using GD.Items;
using GD.Types;
using System;
using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEvent that carries an int parameter.
    /// Used to create an integer-based event that can be raised and responded to.
    /// </summary>
    [CreateAssetMenu(fileName = "FeedbackGameEvent",
        menuName = "GD/Events/Params/Feedback",
        order = 2)]
    public class FeedbackGameEvent : BaseGameEvent<Tuple<ItemCategoryType, ItemData, FeedbackType>>
    { }
}