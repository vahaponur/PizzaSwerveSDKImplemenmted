using UnityEngine;
using UnityEngine.AI;

public static class NavMeshAgentExtensions
{
    /// <summary>
    /// Sets the destination if the agent doesn't have a path already
    /// </summary>
    /// <param name="agent">Object to extend</param>
    /// <param name="posToFollow">Destination of nav mesh agent</param>
    public static void SetDestinationWithLogic(this NavMeshAgent agent, Vector3 posToFollow)
    {
        if (!agent.hasPath)
        {
            agent.SetDestination(posToFollow);
        }
    }
}