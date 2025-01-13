using GD.Audio;
using GD.Events;
using GD.Types;
using System;
using System.Collections;
using UnityEngine;
using GD.State;
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

        [SerializeField]
        [Tooltip("The layer that the item can be picked up by")]
        private LayerMask targetLayer;

        private bool interactible;

        /// <summary>
        /// Called when the item is interacted with (Most likely right clicked on)
        /// </summary>
        /// <param name="interactor">Reference to interactor object</param>
        public void Interact(GameObject interactor)
        {

            if (interactible)
            {
                //raise the event to notify listeners
                onItemEvent?.Raise(itemData);

                Tuple<Transform, Enum> particleData = new Tuple<Transform, Enum>(gameObject.transform, itemData.ItemCategory);

                onParticleEvent?.Raise(particleData);

                AudioManager.Instance.PlaySound(itemData.AudioClip, AudioMixerGroupName.SFX);

                // Don't destroy object as want to preserve gameObject transform for particles
                // make item invisible and uninteractible

                UpdateActivation(false);

                // Tutorial items will not respawn.
                // Prevents grinding in the tutorial to cheat.
                if (gameObject.name != "Tutorial")
                    StartCoroutine(RespawnAfterTime(interactor.GetComponent<Player>().StopWatch));
            }
        }

        public void SetInteractable(bool interactible)
        {
            this.interactible = interactible;
        }

        public void OnHover()
        {
            if (interactible)
                GetComponent<Outline>().enabled = true;
        }

        public void OnDehover()
        {
            GetComponent<Outline>().enabled = false;
        }

        // Activates or disactivates the item by making it invisible
        private void UpdateActivation(bool activated)
        {
            interactible = activated;
            transform.GetChild(0).gameObject.SetActive(activated);

            string layer = activated ? "Item" : "Ignore Raycast";

            //Debug.Log(layer);

            gameObject.layer =  LayerMask.NameToLayer(layer); // Fix a bug where the item is STILL selectable when using "move" as input.
        }

        IEnumerator RespawnAfterTime(Stopwatch stopwatch)
        {
            // More valuable items take longer to respawn
            // Use the stopwatch to check when to respawn. This prevents items from spawning while dialogue is active
            // Also negates the need to use TimeScale
            // https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/operators/lambda-expressions

            float threshold = stopwatch.TimeLeft - Random.Range(2.0f + itemData.Value, 10.0f + itemData.Value * 2);
            //Debug.Log($"{stopwatch.TimeLeft} must be less than {threshold}");

            Func<bool> timeEqual = () => stopwatch.TimeLeft <= threshold;

            yield return new WaitUntil(timeEqual);

            //Debug.Log(interactible);

            UpdateActivation(true);
        }
    }
}