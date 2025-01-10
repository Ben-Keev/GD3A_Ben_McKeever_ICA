using GD.Events;
using GD.FSM;
using System;
using UnityEngine;

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

                // Don't destroy object as want to preserve gameObject transform for particles
                // make item invisible and uninteractible
                gameObject.SetActive(false);
            }
        }

        public void SetInteractible(bool interactible)
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
    }
}