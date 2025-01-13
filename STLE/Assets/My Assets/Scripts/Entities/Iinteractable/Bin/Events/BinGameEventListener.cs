using GD.Items;
using System;
using UnityEngine;

namespace GD.Events
{
    /// <summary>
    /// Listens for an NPC Event
    /// </summary>
    /// <see cref="NPCGameEvent"/>
    public class BinGameEventListener : BaseGameEventListener<Tuple<Transform, BinData>>
    { }
}