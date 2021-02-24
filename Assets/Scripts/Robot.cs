using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] private GameObject player;

    private NavMeshAgent _agent;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _agent.SetDestination(player.transform.position);
    }
}
