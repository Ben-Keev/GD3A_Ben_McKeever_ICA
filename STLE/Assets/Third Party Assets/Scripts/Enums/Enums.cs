namespace GD.Types
{
    /// <summary>
    /// Represents the various types of audio groups in the game.
    /// </summary>
    public enum AudioMixerGroupName : sbyte
    {
        [Description("Master audio group")]
        Master,

        [Description("Ambient sounds group")]
        Ambient,

        [Description("Background music group")]
        Background,

        [Description("Sound effects group")]
        SFX,

        [Description("UI sounds group")]
        UI,

        [Description("Voiceover group")]
        Voiceover,

        [Description("Weapon sounds group")]
        Weapon
    }

    /// <summary>
    /// Represents the state of a UI element, such as visible, hidden, or transitioning.
    /// </summary>
    public enum VisibilityState : sbyte
    {
        [Description("The UI element has tween applied.")]
        End,

        [Description("The UI element is transitioning to a visible state.")]
        Showing,

        [Description("The UI element is transitioning to a hidden state.")]
        Hiding,

        [Description("The UI element has not yet had tween applied.")]
        Start
    }

    /// <summary>
    /// Used in the StateManager to determine how to evaluate a condition.
    /// </summary>
    public enum ConditionType : sbyte
    {
        [Description("Evaluate all conditions and return true if all are met.")]
        And,

        [Description("Evaluate all conditions and return true if any are met.")]
        Or,

        [Description("Evaluate all conditions and return true if only one is met.")]
        Xor
    }

    /// <summary>
    /// Used in the StateManager to determine how to evaluate a condition.
    /// </summary>
    public enum EvaluateStrategy : sbyte
    {
        /// <summary>
        /// Always evaluate the condition, regardless of whether it is met.
        /// </summary>
        [Description("Always evaluate the condition, regardless of whether it is met.")]
        EvaluateAlways,

        /// <summary>
        /// Evaluate the condition until it is met, then stop evaluating.
        /// </summary>
        [Description("Evaluate the condition until it is met, then stop evaluating.")]
        EvaluateUntilMet
    }

    /// <summary>
    /// A five-level priority system used to determine the importance of a task, event, or object.
    /// </summary>
    public enum PriorityLevel : sbyte
    {
        [Description("This is the highest priority.")]
        Highest = 1,

        [Description("This is a high priority.")]
        High = 2,

        [Description("This is a medium priority.")]
        Medium = 3,

        [Description("This is a low priority.")]
        Low = 4,

        [Description("This is the lowest priority.")]
        Lowest = 5
    }

    /// <summary>
    /// Defines the various high-level categories of items available in a game.
    /// Each category groups similar item types under one classification.
    /// </summary>
    /// <see cref="GD.Inventory"/>
    public enum ItemCategoryType : sbyte
    {
        [Description("Items that cannot be recycled")]
        Trash = 0,

        [Description("Items that can be recycled")]
        Recyclable = 1,

        [Description("Items made can be composted")]
        Compost = 2
    }

    /// <summary>
    /// Defines the various specific types of items available in a game.
    /// These types are grouped under the broader categories represented by ItemCategoryType.
    /// </summary>
    /// <see cref="GD.Items.ItemData"/>
    /// <see cref="GD.Inventory"/>
    public enum ItemType : sbyte
    {
        // Recyclable Items
        /// <summary>
        /// Represents an armor vest providing additional protection.
        /// </summary>
        [Description("Items that cannot be recycled")]
        TrashItem = 0,

        [Description("Items that cannot be recycled")]
        RecyclableItem = 1,

        [Description("Items that cannot be recycled")]
        CompostItem = 2
    }
}