using UnityEngine;
using UnityEngine.AI;

public class Robot : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform objective1;
    [SerializeField] private Transform objective2;
    [SerializeField] private float objectiveCollisionDistance = 1f;
    [SerializeField] private float viewDistance = 40f;
    [SerializeField] private float coneOfVisionAngle = 20f;
    [SerializeField] private float chaseDistance = 20f;
    [SerializeField] private string playerTag = "Player";

    private NavMeshAgent _agent;
    private Transform _currentTarget;
    private int _objectiveToTarget = 2;
    private int _objectiveMemory;
    private bool _chasingPlayer;
    private bool _caughtPlayer;

    private void Start()
    {
        _agent = GetComponent<NavMeshAgent>();
        SetObjectiveToFollow(2); // follow objective 2 by default
        _objectiveMemory = _objectiveToTarget;
    }

    private void Update()
    {
        CheckForPlayer();

        if (_chasingPlayer)
        {
            CheckIfCaughtPlayer();
        }

        if (_chasingPlayer && !_caughtPlayer) // if the player is in sight, chase them
        {
            _currentTarget = player;
        }
        else // otherwise, patrol route objectives
        {
            CheckForObjective(_currentTarget);
            if (_currentTarget == player)
            {
                SetObjectiveToFollow(_objectiveMemory);
            }
        }
        
        _agent.SetDestination(_currentTarget.position);
    }

    private void SetObjectiveToFollow(int objectiveNumber)
    {
        if (objectiveNumber == 1)
        {
            _currentTarget = objective1;
            _objectiveToTarget = 1;
        }
        else
        {
            _currentTarget = objective2;
            _objectiveToTarget = 2;
        }
    }

    private void CheckForObjective(Transform checkObjective)
    {
        // get positions on 2D plane
        Vector2 robotPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 checkObjectivePos = new Vector2(checkObjective.position.x, checkObjective.position.z);

        if (Vector2.Distance(robotPos, checkObjectivePos) <= objectiveCollisionDistance)
        {
            SetObjectiveToFollow(_objectiveToTarget == 1 ? 2 : 1); // switch objective to follow 
        }
    }

    private void CheckForPlayer()
    {
        // get positions on 2D plane
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);
        Vector2 robotPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 robotForward = new Vector2(transform.forward.x, transform.forward.z);
        Vector2 playerAngle = (playerPos - robotPos).normalized;

        Vector3 robotEyePosition = transform.position + Vector3.up * 3f;

        // check if in range, then check if in view angle cone
        if (Vector2.Distance(playerPos, robotPos) <= viewDistance && 
            Vector2.Angle(robotForward, playerAngle) <= coneOfVisionAngle)
        {
            // since the player is in range, try to see if theres a wall in the way
            RaycastHit hit;
            if (Physics.Raycast(robotEyePosition, (player.position - robotEyePosition).normalized, out hit))
            {
                // check if the object the raycast his is the player or not
                if (hit.transform.CompareTag(playerTag))
                {
                    if (!_chasingPlayer)
                    {
                        Debug.DrawRay(robotEyePosition, player.position - robotEyePosition, Color.green);
                        _objectiveMemory = _objectiveToTarget;
                        _caughtPlayer = false;
                        _chasingPlayer = true;
                    }
                }
                else
                {
                    _chasingPlayer = false;
                    _caughtPlayer = false;
                }
            }
        }
    }

    private void CheckIfCaughtPlayer()
    {
        // get positions on 2D plane
        Vector2 robotPos = new Vector2(transform.position.x, transform.position.z);
        Vector2 playerPos = new Vector2(player.position.x, player.position.z);

        if (Vector2.Distance(robotPos, playerPos) <= objectiveCollisionDistance)
        {
            _chasingPlayer = false;
            _caughtPlayer = true;
        }
    }
}
