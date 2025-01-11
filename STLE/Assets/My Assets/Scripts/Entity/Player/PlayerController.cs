using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

// https://www.youtube.com/watch?v=cqNBA9Pslg8

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private LayerMask terrainLayer;
    [SerializeField]
    private ParticleSystem clickEffect;

    private NavMeshAgent agent;
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void Move(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, terrainLayer))
        {
            agent.destination = hit.point;

            if (clickEffect != null)
            {
                Instantiate(clickEffect, hit.point += new Vector3(0, 0.1f, 0), clickEffect.transform.rotation);
            }
        }

    }

    //https://www.youtube.com/watch?v=LVu3_IVCzys
    // Animation
    private void Update()
    {
        animator.SetBool("isRun", agent.velocity.magnitude > 0.1f);

        Vector3 direction = (agent.destination - transform.position).normalized;

        if(direction.x > 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));

        // Flip y based on dirction of X, as indicated by its sign.
    }
}