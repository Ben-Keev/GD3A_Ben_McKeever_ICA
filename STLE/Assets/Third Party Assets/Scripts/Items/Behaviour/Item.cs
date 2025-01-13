using GD.Audio;
using GD.Events;
using GD.Types;
using Sirenix.OdinInspector;
using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;
using Stopwatch = GD.State.Stopwatch;

namespace GD.Items
{
    /// <summary>
    /// Represents an item that can be consumed by a game object on the correct layer
    /// </summary>
    /// <see cref="ItemData"/>
    /// <see cref="ItemGameEvent"/>
    public class Item : MonoBehaviour, IInteractable
    {
        [SerializeField]
        [Tooltip("The item data that represents this item")]
        private ItemData itemData;

        [SerializeField]
        [Tooltip("The event that is raised when this item is consumed")]
        private ItemGameEvent onItemEvent;

        [SerializeField]
        [Tooltip("The event that is raised to trigger particles where this item is consumed")]
        private ParticleGameEvent onParticleEvent;

        [FoldoutGroup("Runtime Info")]
        private bool interactable;

        public bool Interactable { get => interactable; set => interactable = value; }

        /// <summary>
        /// Called when the item is interacted with (Most likely right clicked on)
        /// </summary>
        /// <param name="interactor">Reference to interactor object</param>
        public void Interact(GameObject interactor)
        {

            if (Interactable)
            {
                //raise the event to notify listeners
                onItemEvent?.Raise(itemData);

                Tuple<Transform, Enum> particleData = new Tuple<Transform, Enum>(gameObject.transform, itemData.ItemCategory);

                onParticleEvent?.Raise(particleData);

                AudioManager.Instance.PlaySound(itemData.AudioClip, AudioMixerGroupName.SFX);

                // Don't destroy object as want to preserve gameObject transform for particles
                // make item invisible and uninteractible

                ChangeActivation(false);

                // Tutorial items will not respawn.
                // Prevents grinding in the tutorial to cheat.
                if (gameObject.name != "Tutorial")
                    StartCoroutine(ActivateAfterTime(interactor.GetComponent<Player>().StopWatch));
            }
        }


        /// <summary>
        /// Enables or disables interactability
        /// </summary>
        /// <param name="interactable">Enable or disable paramater</param>
        public void SetInteractable(bool interactable)
        {
            this.Interactable = interactable;
        }

        /// <summary>
        /// An outline is drawn around the item
        /// </summary>
        public void OnHover()
        {
            if (Interactable)
                GetComponent<Outline>().enabled = true;
        }

        /// <summary>
        /// An outline is drawn around the item
        /// </summary>
        public void OnDehover()
        {
            GetComponent<Outline>().enabled = false;
        }

        
        /// <summary>
        /// Toggles whether the item is visible and interactable
        /// Can be used to make item appear 'consumed'
        /// </summary>
        /// <param name="activated"></param>
        private void ChangeActivation(bool activated)
        {
            Interactable = activated;

            // Hide the item's model
            transform.GetChild(0).gameObject.SetActive(activated);

            // Determine what layer the item will be moved to
            // Fixes a bug where the item is still interactable when deactivated if using "move" as input.
            string layer = activated ? "Item" : "Ignore Raycast";

            gameObject.layer =  LayerMask.NameToLayer(layer); 
        }

        /// <summary>
        /// Wait for the onscreen stopwatch to reach a given time before activating the item again
        /// </summary>
        /// <param name="stopwatch"></param>
        /// <returns></returns>
        IEnumerator ActivateAfterTime(Stopwatch stopwatch)
        {
            // More valuable items take longer to respawn
            // Use the stopwatch to check when to respawn. This prevents items from spawning while dialogue is active
            // Also negates the need to use TimeScale
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions
            float threshold = stopwatch.TimeLeft - Random.Range(2.0f + itemData.Value, 10.0f + itemData.Value * 2);

            Func<bool> timeEqual = () => stopwatch.TimeLeft <= threshold;

            yield return new WaitUntil(timeEqual);

            ChangeActivation(true);
        }
    }
}