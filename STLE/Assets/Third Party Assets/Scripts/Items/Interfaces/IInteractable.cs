using UnityEngine;

namespace GD.Items
{
    /// <summary>
    /// Items implementing this interface can be interacted with by other objects.
    /// </summary>
    public interface IInteractable
    {
        void SetInteractable(bool interactible);

        void Interact(GameObject interactor);

        void OnHover();

        void OnDehover();
    }
}