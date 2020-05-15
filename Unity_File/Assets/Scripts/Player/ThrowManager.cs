using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThrowManager : MonoBehaviour
{
    public Vector3 throw_at;
    private Vector3 s;
    private Vector3 windVec;
    private float wind_power;
    public bool throw_mode;
    public float throw_power;

    private float gravity;
    private float throw_angle;
    private float throw_speed;
    private bool throw_done;

    public ThirdCamera tc;
    public Transform aim;
    public GameSystem.Mode mode;
    public GameObject seed;
    private GameObject throw_sprite;    // 임시스프라이트
    private GameObject tmp;  //임시씨앗

    Vector3 start_transform;
    private float t;

    public GameObject aim_sp;

    string seed_name;

    private void Start()
    {
        // aim 캐릭터 따라다니도록 했는데,,수정해야될듯. 카메라 위로 올리면 aim도 위로 올라가야해서. 
        // Ray쏴서 2차원->3차원 좌표로 바꾼걸 Aim으로 써야될듯
        throw_power = 2f;
        gravity = 9.8f;
        throw_at =
        windVec =
        s = Vector3.zero;
        start_transform = transform.position + new Vector3(0, 1.3f, 0);
    }

    public void mouse_down(string name)
    {
        seed_name = name;
        //StopCoroutine("ThrowingSeed");
        aim_sp.SetActive(true);
    }

    public void mouse_up(bool option)
    {
        if (option && Targeting())
            Throw();
        
        aim_sp.SetActive(false);
    }

    public bool Targeting()
    {
        //마우스를 누르고 있는 상태.
        if (isPlantable())
        {
            aim_sp.GetComponent<Image>().color = Color.cyan;
            return true;
        }
        else
        {
            aim_sp.GetComponent<Image>().color = Color.magenta;
            return false;
        }

    }

    private bool isPlantable()
    {
        float distance = 20.0f;
        Debug.DrawRay(aim.transform.position, aim.transform.forward * distance, Color.green, 1.0f);

        if (Physics.Raycast(aim.transform.position, aim.transform.forward, out RaycastHit hit, distance))
        {
            if (hit.transform.CompareTag("Plantable") || hit.transform.CompareTag("Ground"))
                return true;
            else
                return false;
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

        Vector3 aimForward = aim.forward; //발사하는 시점의 aim 받아놓음
        Debug.Log("Throw Aim : " + aimForward);

        tmp.GetComponent<Plant>().red_go = aimForward;

        //궤도를 따라 움직이는 코루틴 시작
        StartCoroutine(ThrowingSeed(bullet_rig, aimForward));
    }

    IEnumerator ThrowingSeed(Rigidbody bullet_rig, Vector3 aimForward)
    {
        if (tmp.activeSelf == true)
        {
        Debug.Log("Aim : "+aimForward);
            bullet_rig.AddForce(aimForward * throw_power, ForceMode.Impulse);
             
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(ThrowingSeed(bullet_rig, aimForward));
        }
        else
            Destroy(tmp);
    }

}
