using UnityEngine;

namespace GD.Selection
{
    /// <summary>
    /// Provides a ray that originates from a target GameObject.
    /// </summary>
    public class MouseRayProvider : MonoBehaviour, IRayProvider
    {
        [SerializeField]
        private Camera targetCamera;

        [Header("Debug Gizmo Properties")]
        [SerializeField]
        private Color rayColor = Color.yellow;

        [SerializeField]
        [Range(0.1f, 100)]
        private float rayLength = 10;

        [ReadOnly] private Ray ray;

        // https://www.youtube.com/watch?v=aaYfoe9i5lY
        public Ray CreateRay()
        {
            Ray ray = targetCamera.ScreenPointToRay(Input.mousePosition);
            return ray;
        }

        private void OnDrawGizmos()
        {
            Vector3 mousePos = Input.mousePosition;
            mousePos.z = 100f;

            mousePos = targetCamera.ScreenToWorldPoint(mousePos);

            Debug.DrawRay(targetCamera.transform.position, mousePos - targetCamera.transform.position, rayColor);
        }
    }
}