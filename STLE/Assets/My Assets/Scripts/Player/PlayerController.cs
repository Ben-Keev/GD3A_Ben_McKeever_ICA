using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

// https://www.youtube.com/watch?v=cqNBA9Pslg8

public class PlayerController : MonoBehaviour
{
    public float groundDist;

    public LayerMask terrainLayer;
    public Rigidbody rb;
    public SpriteRenderer sr;

    NavMeshAgent agent;
    ParticleSystem clickEffect;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
    }

    // https://www.youtube.com/watch?v=cqNBA9Pslg8
    void Update()
    {
        RaycastHit hit;
        Vector3 casPos = transform.position;
        casPos.y += 1;

        if (Physics.Raycast(casPos, -transform.up, out hit, Mathf.Infinity, terrainLayer))
        {
            if (hit.collider != null)
            {
                Vector3 movePos = transform.position;
                movePos.y = hit.point.y + groundDist;
                transform.position = movePos;
            }
        }

        //if (x != 0 && x < 0)
        //{
        //    sr.flipX = true;
        //}
        //else if (x != 0 && x > 0)
        //{
        //    sr.flipX = false;
        //}
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
}