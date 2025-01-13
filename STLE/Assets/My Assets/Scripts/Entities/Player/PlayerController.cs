using GD.Audio;
using GD.Types;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.InputSystem;

/// <summary>
/// Responds to inputs and moves player, showing animation and playing sound as it does so.
/// </summary>
public class PlayerController : MonoBehaviour
{
    [FoldoutGroup("Navagent", expanded: true)]
    [Tooltip("Target layer for raycasts of 'walkable' areas")]
    [SerializeField]
    private LayerMask terrainLayer;

    [FoldoutGroup("Sound", expanded: true)]
    [Tooltip("Made when a player takes a step")]
    [SerializeField]
    private AudioClip footstepSound;

    [FoldoutGroup("Sound", expanded: true)]
    [Tooltip("How often Footstep Sound will play")]
    [SerializeField]
    private float stepRate;

    // How much of the cooldown is left between steps.
    private float stepCoolDown;

    // Components
    private NavMeshAgent agent;
    private Animator animator;
    private bool navMeshMovement;
    private bool playerMoving;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponentInChildren<Animator>();
    }

    /// <summary>
    /// Signal the player to move. Triggers on press of move input.
    /// </summary>
    /// <param name="context">Input context</param>
    public void StartMoving(InputAction.CallbackContext context)
    {
        playerMoving = true;
    }

    /// <summary>
    /// Signal the player to stop moving. Triggers on opening of dialogue box.
    /// </summary>
    public void StopMoving()
    {
        playerMoving = false;
    }

    /// <summary>
    /// Signal the player to stop moving. Triggers on release of move input.
    /// </summary>
    /// <param name="context">Input context</param>
    public void StopMoving(InputAction.CallbackContext context)
    {
        playerMoving = false;
    }

    /// <summary>
    /// NavAgent will target coordinates hit by the raycast
    /// </summary>
    public void Move()
    {
        RaycastHit hit;
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hit, 100, terrainLayer))
            agent.destination = hit.point;
    }

    /// <summary>
    /// Plays footstep sound according to the stepRate (frequency that sound should be played)
    /// </summary>
    private void PlayFootsepSound()
    {
        // https://www.reddit.com/r/Unity3D/comments/2s3iub/good_footstep_tutorials/
        // Footsteps tutorial
        stepCoolDown -= Time.deltaTime;
        if (navMeshMovement && stepCoolDown < 0f)
        {
            AudioManager.Instance.PlaySound(footstepSound, AudioMixerGroupName.SFX);
            stepCoolDown = stepRate;
        }
    }

    /// <summary>
    /// Flip the sprites display depending on the player's trajectory
    /// </summary>
    private void FaceDirection()
    {
        // Flip y based on dirction of X, as indicated by its sign.

        Vector3 direction = (agent.destination - transform.position).normalized;

        if (direction.x > 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
        else if (direction.x < 0)
            transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }

    /// <summary>
    /// Adjust speed by the requested parameter. Useful in administering speed boosts through game events.
    /// </summary>
    /// <param name="difference">How much the speed should increase / Decrease </param>
    public void ChangeSpeed(int difference)
    {
        // We are recieving whole numbers so divide them to be smaller.
        agent.speed += difference/ 16.0f;

        // Step rate cannot go below 0.2
        if (stepRate != 0.2f)
            stepRate -= difference / Mathf.Pow(12.0f, 2.0f);

        if (stepRate < 0.2f)
            stepRate = 0.2f;
    }

    //https://www.youtube.com/watch?v=LVu3_IVCzys
    // Animation tutorial

    private void Update()
    {
        // Determines whether the player is moving according to the navMesh's velocity.
        navMeshMovement = agent.velocity.magnitude > 0.1f;

        // Play animation and footstep
        animator.SetBool("isRun", agent.velocity.magnitude > 0.1f);
        PlayFootsepSound();

        // Determines whether the player made an input to start moving.
        if (playerMoving)
            Move();

        // Always face direction of trajectory
        FaceDirection();
    }
}