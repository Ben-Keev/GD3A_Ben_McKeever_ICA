using GD.Audio;
using GD.Types;
using System.Collections;
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

    [SerializeField]
    private AudioClip footstepSound;

    // How often the player steps
    [SerializeField]
    private float stepRate;

    private NavMeshAgent agent;
    private Animator animator;
    private bool navMeshMovement;
    private bool playerMoving;

    private float stepCoolDown;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = transform.GetChild(0).GetComponent<Animator>();
    }

    public void StartMoving(InputAction.CallbackContext context)
    {
        playerMoving = true;
    }

    public void StopMoving()
    {
        playerMoving = false;
    }

    public void StopMoving(InputAction.CallbackContext context)
    {
        playerMoving = false;
    }

    public void Move()
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

    private void FootStep()
    {
        // https://www.reddit.com/r/Unity3D/comments/2s3iub/good_footstep_tutorials/
        // Footsteps
        stepCoolDown -= Time.deltaTime;
        if (navMeshMovement && stepCoolDown < 0f)
        {
            AudioManager.Instance.PlaySound(footstepSound, AudioMixerGroupName.SFX);
            stepCoolDown = stepRate;
        }
    }

    private void FaceTrajectory()
    {
        // Flip y based on dirction of X, as indicated by its sign.

        Vector3 direction = (agent.destination - transform.position).normalized;

        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    //https://www.youtube.com/watch?v=LVu3_IVCzys
    // Animation

    private void Update()
    {
        navMeshMovement = agent.velocity.magnitude > 0.1f;

        animator.SetBool("isRun", agent.velocity.magnitude > 0.1f);
        FootStep();

        if (playerMoving)
            Move();

        FaceTrajectory();
    }
}