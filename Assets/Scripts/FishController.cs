using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class FishController : MonoBehaviour
{

    private NavMeshAgent navMeshAgent;
    public Fish fish;
    private Vector3 initialPosition;

    // Start is called before the first frame update
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updateRotation = false;
        navMeshAgent.updateUpAxis = false;
        initialPosition = transform.position;
    }


    private void Update()
    {
        // Debug.Log("spa" + fish.deepingSpawn);
        // Debug.Log("sped" + fish.speed);
        if (navMeshAgent.enabled)
        {
            navMeshAgent.speed = fish.speed;
            bool isAproxToArrive = navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance;
            if (isAproxToArrive)
            {
                Vector3 point;
                if (RandomPoint(initialPosition, 10f, out point)) //pass in our centre point and radius of area
                {
                    Debug.DrawRay(point, Vector3.up, Color.blue, 1.0f); //so you can see with gizmos
                    navMeshAgent.SetDestination(point);
                }
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {
        Vector3 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas))
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector3.zero;
        return false;
    }


}
