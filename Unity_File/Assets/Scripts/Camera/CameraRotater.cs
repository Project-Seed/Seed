using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    // 캐릭터 주위를 회전
    public Transform target;                                    // 카메라가 따라갈 대상
    private Transform camera_transform;                         // 부모인 CameraRig의 transform == 카메라의 transform
    private bool press_Rkey;
    private bool press_Tkey;
    [SerializeField] private float rotate_angle = 2.0f;        // 카메라 회전각도 (1초에 2도)
    public Vector3 offset;

    private void Start()
    {
        camera_transform = GetComponent<Transform>();
        press_Rkey = false;
        press_Tkey = false;
        offset.Set(3, 0, 0);
    }
    private void Update() // 키 입력
    {
        if (Input.GetKey(KeyCode.R))
        {
            press_Rkey = true;
            Debug.Log("GetKey : R");
            RotateCW();

        }
        else
        {
            press_Rkey = false;
        }

        if (Input.GetKey(KeyCode.T))
        {
            press_Tkey = true;
            Debug.Log("GetKey : T");
            RotateCCW();
        }
        else
        {
            press_Tkey = false;
        }
    }

    private void RotateCW()
    {
        if (press_Rkey == false) return;
        camera_transform.RotateAround(target.position, target.transform.up, rotate_angle * Time.deltaTime); // 타겟을 중심으로, y축 회전(공전), 회전각도
       
        // 쿼터니언 회전이 아니라서 z축이 와리가리함 0으로 고정하는 코드<<안먹힘 ㅠ 왤까
        //Quaternion q = transform.rotation;
        //q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        //transform.rotation = q;
    }

    private void RotateCCW()
    {
        if (press_Tkey == false) return;
        camera_transform.RotateAround(target.position, target.transform.up, -rotate_angle * Time.deltaTime); // 타겟을 중심으로, y축 회전(공전), 회전각도
       
    }
}
