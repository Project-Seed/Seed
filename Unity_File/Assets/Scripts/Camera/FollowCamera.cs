using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowCamera : MonoBehaviour
{
    // 뒤통수에 고정되어 따라다니는 카메라
    public Transform target;                                    // 카메라가 따라갈 대상
    private Transform camera_transform;                         // 카메라의 transform
    [SerializeField] private float distance = 5.0f;             // 타겟과의 거리
    [SerializeField] private float height = 4.0f;               // 카메라 높이
    //[SerializeField] private float move_damping = 2.0f;         // 카메라 이동 속도
    [SerializeField] private float offset = 2.0f;               // 타겟 좌표에서의 offset (타켓의 발 밑이 좌표라서 주는거)
                                                               //[SerializeField] private float smooth_time = 1.0f;          // 카메라가 지정시간만큼 늦게 따라감 (떨림현상 발생)                                                            //private Vector3 camara_velocity;
    private bool press_Rkey;
    private bool press_Tkey;
    [SerializeField] private float rotate_angle = 2.0f;        // 카메라 회전각도 (1초에 2도)
   


    void Start()
    {
        camera_transform = GetComponent<Transform>();
        press_Rkey = false;
        press_Tkey = false;
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

    void LateUpdate()       // 캐릭터 움직임이 끝나고 카메라가 움직여야 하므로 Late.
    {
        var new_position = target.position - (target.forward * distance) + target.up * height; // 목표로 할 좌표계산 (카메라가 따라갈 곳)

        camera_transform.position = new_position;                                                                                           //바로 따라감

        //camera_transform.position = Vector3.SmoothDamp(camera_transform.position, new_position, ref camara_velocity, smooth_time);        //SmoothDamp 보간

        //camera_transform.position = Vector3.Slerp(camera_transform.position, new_position, Time.deltaTime * move_damping);                //Slerp 구면 선형보간 (회전 시 사용)

        //camera_transform.position = Vector3.Lerp(camera_transform.position, new_position, Time.deltaTime * move_damping);                 //lerp 선형보간

        camera_transform.LookAt(target.position + (target.up * offset));        // 플레이어의 정수리를 쳐다봄
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
