using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class PlayerMovement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private Camera cam;
    private NavMeshAgent agent;
    void Start()
    {
        agent= GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Hello");
        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            Ray cameraToMouse = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            Debug.Log("Hello");
            if (Physics.Raycast(cameraToMouse,out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
