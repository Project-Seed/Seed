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
    public Image cant_shoot_sp; // 쏠 수 없음 스프라이트
    private GameObject throw_sprite;    // 임시스프라이트
    private GameObject tmp;  //임시씨앗
    private bool target_on;

    public Transform sub_cam;
    private Vector3 dest;
    string seed_name;
    public CameraRotater cameraShaker;

    // aim 캐릭터 따라다니도록 했는데,,수정해야될듯. 카메라 위로 올리면 aim도 위로 올라가야해서. 
    // Ray쏴서 2차원->3차원 좌표로 바꾼걸 Aim으로 써야될듯
    //카메라에서 Ray를 쏘고 그 닿은 곳에 씨앗이 가도록. 출발점은 캐릭터 손. 도착지는 카메라 기준 에임점이 닿은곳.
    //발사취소는 우클릭 했을 때만.
    //조준선은 씨앗 조건에 따라 초록/빨강
    //빨강일때도 쏠 수 있는데 그러면 조건 안 맞아서 먼지만 날림
    private IEnumerator FadeIn(Image img)
    {
        for (int i = 0; i <= 30; i++)
        {
            img.color = new Color(1, 1, 1, i / 30f);
            yield return new WaitForSeconds(0.01f);
        }
        StartCoroutine(FadeOut(cant_shoot_sp));
    }

    private IEnumerator FadeOut(Image img)
    {
        yield return new WaitForSeconds(0.3f);

        for (int i = 0; i <= 30; i++)
        {
            img.color = new Color(1, 1, 1, (30 - i) / 30f);
            yield return new WaitForSeconds(0.01f);
        }
        img.enabled = false;
    }

    public bool ThrowSeed(bool option)//좌클릭 발사 or 발사취소
    {
        aim_sp.SetActive(false);
        target_on = false;

        if (option)//option true이면 발사. option false는 발사 취소한 경우.
        {
            if (!isPlantable()) //불가피한 취소
            {
                cant_shoot_sp.enabled = true; //창 띄우기
                cant_shoot_sp.color = new Color(1, 1, 1, 1);
                StartCoroutine(FadeIn(cant_shoot_sp));
                return false;
            }
            else
            {
                //발사
                tmp = Instantiate(seed, aim.position, aim.rotation);
                tmp.GetComponent<Plant>().seed_name = seed_name;
                Rigidbody bullet_rig = tmp.GetComponent<Rigidbody>();
                bullet_rig.AddForce(dest * 20, ForceMode.Impulse);
                return true;
            }
        }
        else
            return false;
    }

    public void TargetOn(string name)//우클릭 조준
    {
        seed_name = name;
        aim_sp.SetActive(true);
    }

    public void Targeting()
    {
        //마우스를 누르고 있는 상태.
        if (isPlantable())
        {
            aim_sp.GetComponent<Image>().color = Color.cyan;
            target_on = true;
        }
        else
        {
            aim_sp.GetComponent<Image>().color = Color.red;
            target_on = false;
        }
    }

    private bool isPlantable()
    {
        float distance = 20.0f;
        if (Physics.Raycast(sub_cam.position, sub_cam.forward, out RaycastHit hit, distance))
        {
            if (checkPlantable(hit))
            { 
                //심기가능
              dest = hit.point - aim.transform.position;
              return true;
            }
            else
              return false;
        }
        else return false;
    }
    private bool checkPlantable(RaycastHit hit)
    {
        if (hit.transform.CompareTag("DontShoot")) return false;

        bool isWall = checkWall(ref hit, seed_name);

        switch (seed_name)
        {
            case "blue_seed":
            case "brown_seed":
                if (isWall)
                    return true;
                break;

            case "red_seed":
            case "white_seed":
                if (!isWall)
                    return true;
                break;

            case "green_seed":
            case "lime_seed":
                if (hit.transform.CompareTag("Plant"))
                    return true;
                break;

            case "yellow_seed":
                if (hit.transform.CompareTag("Yellow"))
                    return true;
                break;

            case "purple_seed":
                //마을에서는 사용 불가
                return true;

            default: return false;

        }
        return false;
    }

    private static bool checkWall(ref RaycastHit hit, string type)
    {
        float angle = Vector3.Angle(hit.normal, Vector3.up);
        Debug.Log("hit " + hit.transform.gameObject.name + "Angle " + angle);
        bool isWall;
        if (type.Equals("blue_seed"))
        {
            if (70.0f <= angle && angle < 130.0f)
                isWall = true;
            else
                isWall = false;
        }
        else if (type.Equals("brown_seed"))
        {
            if (60.0f <= angle && angle < 90.0f)
                isWall = true;
            else
                isWall = false;
        }
        else if (0.0f <= angle && angle < 50.0f)
            isWall = false;
        else //blue, brown 아닌데 50도 이상이면 벽으로 인식
            isWall = true;
        return isWall;
    }

    //IEnumerator ThrowingSeed(Rigidbody bullet_rig, Vector3 dest)
    //{
    //    if (tmp.activeSelf == true)
    //    {
    //        //bullet_rig.AddForce(aimForward, ForceMode.Impulse);
    //        //bullet_rig.position = Vector3.MoveTowards(bullet_rig.position, dest, 0.5f);
    //        bullet_rig.AddForce(dest*5, ForceMode.Impulse);
    //        yield return new WaitForSeconds(0.1f);
    //        StartCoroutine(ThrowingSeed(bullet_rig, dest));
    //    }
    //    else
    //        Destroy(tmp);
    //}



}
