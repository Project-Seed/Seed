using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class TargetGroup : MonoBehaviour
{
    public CinemachineTargetGroup targetGroup;
    public CinemachineVirtualCamera vcam;

    private void FixedUpdate()
    {
        if (GameSystem.instance.talk_trigger)
        {
            AddTarget();
            
        }
        else
        {
            targetGroup.RemoveMember(GameSystem.instance.talk_npc_ob.transform);
            vcam.gameObject.SetActive(false);
        }
    }

    public void AddTarget()
    {
        if (targetGroup.m_Targets.Length < 2)
            targetGroup.AddMember(GameSystem.instance.talk_npc_ob.transform, 2, 0.5f);

        if (targetGroup.m_Targets.Length == 2)
        {
            vcam.gameObject.SetActive(true);
        }
    }

}
