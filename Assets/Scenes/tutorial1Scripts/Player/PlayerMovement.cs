using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent),typeof(AgentLinkMover))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera cam;
    private NavMeshAgent agent;
    private Animator playerAnimator;
    private string isMovingParameter = "isMoving";
    private string jumpParameter = "isJumping";
    private AgentLinkMover linkMover;

    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
        playerAnimator = GetComponentInChildren<Animator>();
        linkMover = GetComponent<AgentLinkMover>();
        linkMover.OnStartJump += StartJumpAnimation;
        linkMover.OnEndJump += StopJumpAnimation;
    
    }

    public void StartJumpAnimation()
    {
        playerAnimator.SetBool(jumpParameter, true);
    }

    public void StopJumpAnimation()
    {
        playerAnimator.SetBool(jumpParameter , false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray cameraToMouse = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(cameraToMouse,out hit))
            {
                agent.SetDestination(hit.point);
            }
        }


        playerAnimator.SetBool(isMovingParameter,agent.velocity.magnitude > 0.1f);
    }
}
