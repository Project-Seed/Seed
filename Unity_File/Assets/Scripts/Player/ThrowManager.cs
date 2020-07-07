﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class ThrowManager : MonoBehaviour
{
    public Transform aim; // 실제 씨앗이 나가는 총구.
    public GameObject seed; // 씨앗 모델
    public GameObject aim_sp; // 조준선 스프라이트
    private GameObject throw_sprite;    // 임시스프라이트
    private GameObject tmp;  //임시씨앗
    private bool target_on;

    public Transform sub_cam;
    private Vector3 dest;
    string seed_name;
    public CameraRotater cameraRotater;

    private void Start()
    {
        // aim 캐릭터 따라다니도록 했는데,,수정해야될듯. 카메라 위로 올리면 aim도 위로 올라가야해서. 
        // Ray쏴서 2차원->3차원 좌표로 바꾼걸 Aim으로 써야될듯
        //카메라에서 Ray를 쏘고 그 닿은 곳에 씨앗이 가도록. 출발점은 캐릭터 손. 도착지는 카메라 기준 에임점이 닿은곳.
    }

    public void mouse_down(string name)
    {
        seed_name = name;
        aim_sp.SetActive(true);
        
    }

    public void mouse_up(bool option)
    {
        if (option && target_on)//조준하고 option true이면 발사. option false는 발사 취소한 경우.
            Throw();

        aim_sp.SetActive(false);
        target_on = false;
    }

    public void Targeting()
    {
        //마우스를 누르고 있는 상태.
        if (isPlantable())
        {
            aim_sp.GetComponent<Image>().color = Color.cyan;
            target_on = true;
            Debug.Log("Target on");
        }
        else
        {
            aim_sp.GetComponent<Image>().color = Color.magenta;
            target_on = false;
        }

    }

    private bool isPlantable()
    {
        float distance = 100.0f;
        Debug.DrawRay(sub_cam.position, sub_cam.forward * distance, Color.green, 10.0f);
        if (Physics.Raycast(sub_cam.position, sub_cam.forward, out RaycastHit hit, distance))
        {
           // if (hit.transform.CompareTag("Plantable") || hit.transform.CompareTag("Ground"))
           // {
                Debug.DrawLine(sub_cam.position, hit.point, Color.red, 3.0f) ;
                dest = hit.point - aim.transform.position;
                Debug.Log(dest);
                return true;
           // }
           // else
                //return false;
        }
        else return false;
    }

    private void Throw()
    {
        //aim이 캐릭터 자식으로 있어서 캐릭터 움직이면 별수없이 움직임.
        //씨앗이 처음 발사될때 빼고는 캐릭터와 분리되어야함.
        //씨앗 생성
        tmp = Instantiate(seed, aim.position, aim.rotation);
        //tmp.transform.position = aim.localPosition;
        
        //씨앗 이름 넘겨주기
        tmp.GetComponent<Plant>().seed_name = seed_name;

        Rigidbody bullet_rig = tmp.GetComponent<Rigidbody>();

        //Vector3 aimForward = aim.forward; //발사하는 시점의 aim 받아놓음
        //Debug.Log("Throw Aim : " + aimForward);

        //tmp.GetComponent<Plant>().red_go = aimForward;

        //궤도를 따라 움직이는 코루틴 시작
        StartCoroutine(ThrowingSeed(bullet_rig, dest));
    }

    IEnumerator ThrowingSeed(Rigidbody bullet_rig, Vector3 dest)
    {
        if (tmp.activeSelf == true)
        {
            //bullet_rig.AddForce(aimForward, ForceMode.Impulse);
            //bullet_rig.position = Vector3.MoveTowards(bullet_rig.position, dest, 0.5f);
            bullet_rig.AddForce(dest*10, ForceMode.Impulse);
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ThrowingSeed(bullet_rig, dest));
        }
        else
            Destroy(tmp);


    }

}
