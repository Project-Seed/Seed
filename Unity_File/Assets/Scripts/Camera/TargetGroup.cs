using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class TargetGroup : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public CinemachineVirtualCamera vcam;
    GameObject npc;

    private void FixedUpdate()
    {
        if (GameSystem.instance.talk_trigger)
        {
            npc = GameSystem.instance.talk_npc_ob;
            AddTarget();
            
        }
        else
        {
            if (npc != null)
            {
                targetGroup.RemoveMember(npc.transform);
                npc = null;
            }

            vcam.gameObject.SetActive(false);
        }
    }

    public void AddTarget()
    {
        if (targetGroup.m_Targets.Length < 2)
            targetGroup.AddMember(npc.transform, 1, 1.5f);

        if (npc!=null)
        {
            vcam.gameObject.SetActive(true);
        }
    }

}
