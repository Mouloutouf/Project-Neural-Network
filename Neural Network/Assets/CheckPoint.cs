using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    public Transform nextCheckpoint;

    void OnTriggerEnter(Collider other)
    {
        Agent agent = other.GetComponent<Agent>();

        if (agent)
        {
            if(agent.nextCheckpoint == transform)
            {
                agent.CheckPointReached(nextCheckpoint);
            }
        }
    }

    /*
    [ContextMenu("Set Spawn Point")]
    public void SetAsSpawnPoint()
    {
        CheckPointManager.instance.firstCheckPoint = this.transform;
        GetComponent<MeshRenderer>().material.color = Color.red;
    }
    */
}
