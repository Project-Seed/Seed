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
    private float throw_power;
    private float gravity;
    private float throw_angle;
    private float throw_speed;
    private bool throw_done;

    public ThirdCamera tc;
    Transform aim;
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
        aim = transform.GetChild(1);
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

    private void CalcThrow()
    {
        // 낙하지점 계산
        float horizonDist;  //수평 도달 거리
        throw_angle = tc.mouse_move.x;
        throw_speed = 10.0f;
        horizonDist = (Mathf.Pow(throw_speed, 2) * Mathf.Sin(2 * throw_angle)) / gravity;
        s = new Vector3(windVec.x * wind_power, 0, horizonDist + windVec.z * wind_power);
        throw_at = transform.forward + s;
        Debug.Log("forward : " + transform.forward);

        Debug.Log("throw at : " + throw_angle + " " + throw_at+"d "+horizonDist);

        // 해당 지점에 스프라이트 그리기
        // 오브젝트 생성
        //if (throw_sprite)
        //    DestroyImmediate(throw_sprite);
        //throw_sprite = Instantiate(circle_sprite, throw_at, Quaternion.AngleAxis(0f, Vector3.right));
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

        //궤도를 따라 움직이는 코루틴 시작
        StartCoroutine(ThrowingSeed(bullet_rig, aimForward));
    }


    IEnumerator ThrowingSeed(Rigidbody bullet_rig, Vector3 aimForward)
    {
        if (tmp.activeSelf == true)
        {
        Debug.Log("Aim : "+aimForward);
            bullet_rig.AddForce(aimForward, ForceMode.Impulse);
             
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(ThrowingSeed(bullet_rig, aimForward));
        }
        else
            Destroy(tmp);

        //if (!tmp) yield break;
        //t += 0.05f;
        //float z = throw_speed * Mathf.Cos(throw_angle) * t;
        //float y = throw_speed * Mathf.Sin(throw_angle) * t - (0.5f * gravity * Mathf.Pow(t, 2));
        //tmp.transform.position = new Vector3(start_transform.x, start_transform.y + y, start_transform.z + z);

            ////아래 조건 착지했을 때(지면 or 오브젝트와 충돌했을 때)로 바꿀 예정
            //if (y <= 0)
            //{
            //    t = 0;
            //    DestroyImmediate(tmp);
            //    PlantSeed(tmp.transform.position);
            //    throw_done = true;
            //    yield break;
            //}
            //else
            //{
            //    throw_done = false;
            //    yield return new WaitForSeconds(0.05f);
            //    StartCoroutine(ThrowingSeed());
            //}
    }

}
