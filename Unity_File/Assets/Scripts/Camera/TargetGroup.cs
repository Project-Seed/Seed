using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class TargetGroup : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public NpcController npc;
    CinemachineTargetGroup.Target target;
    public CinemachineVirtualCamera vcam;
    private void FixedUpdate()
    {
        if (GameSystem.instance.talk_trigger)
        {
            AddTarget();
            
        }
        else
        {
            targetGroup.RemoveMember(npc.transform);
            vcam.gameObject.SetActive(false);
        }
    }

    public void AddTarget()
    {
        if (targetGroup.m_Targets.Length < 2)
            targetGroup.AddMember(npc.transform, 2, 0.5f);

        if (targetGroup.m_Targets.Length == 2)
        {
            vcam.gameObject.SetActive(true);
        }
    }

}
