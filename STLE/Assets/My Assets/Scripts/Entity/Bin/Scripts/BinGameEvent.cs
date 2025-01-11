using GD.Items;
using System;
using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Concrete implementation of BaseGameEvent that carries an int parameter.
    /// Used to create an integer-based event that can be raised and responded to.
    /// </summary>
    [CreateAssetMenu(fileName = "BinGameEvent",
        menuName = "GD/Events/Params/Bin",
        order = 5)]
    public class BinGameEvent : BaseGameEvent<Tuple<Transform, BinData>>
    { }
}