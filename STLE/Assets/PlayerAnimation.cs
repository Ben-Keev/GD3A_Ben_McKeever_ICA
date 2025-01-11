using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAnimation : MonoBehaviour
{
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Animator animator;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
}
