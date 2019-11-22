using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FixedCamera : MonoBehaviour
{
    // 동물의 숲 카메라 시점
    // 마우스 휠 직선으로 안움직임->distance height 비례해서 식짜기
    
    public Transform target;                                    // 카메라가 따라갈 대상 캐릭터+10
    private Transform camera_transform;                         // 카메라의 transform
    //[SerializeField] private float move_speed = 10.0f;          // 카메라 이동속도

    [SerializeField] private float distance_cur = 15.0f;        // 타겟과의 거리 (현재)
    private float distance_max = 35.0f;                         // 타겟과의 거리 (최대)
    private float distance_min = 15.0f;                          // 타겟과의 거리 (최소)

    [SerializeField] private float height_cur = 5.0f;            // 카메라 높이 (현재)
    private float height_max = 8.0f;                             // 카메라 높이 (최대)
    private float height_min = 5.0f;                             // 카메라 높이 (최소)

    //[SerializeField] private float offset = 1.0f;                // 타겟 좌표에서의 offset (타켓의 발 밑이 좌표라서 주는거)
    private float input_mouse_wheel;
    public float smooth = 1.0f;

    void Start()
    {
        camera_transform = GetComponent<Transform>();
    }


  
    private void FixedUpdate()       // 캐릭터의 움직임에 따라 
    {
        input_mouse_wheel = Input.GetAxis("Mouse ScrollWheel");
        //if (input_mouse_wheel != 0)
            //Debug.Log("wheel : " + input_mouse_wheel);
        //var min_position = target.position - (target.forward * distance_min) + target.up * height_min;
        //var max_position = target.position - (target.forward * distance_max) + target.up * height_max;
        //var new_position = Mathf.Clamp(5.0f, min_position, max_position);

        // 마우스 휠 조작
        distance_cur = Mathf.Clamp(distance_cur - input_mouse_wheel*5, distance_min, distance_max); // 현재 거리 갱신
        height_cur = Mathf.Clamp(height_cur - input_mouse_wheel, height_min, height_max); // 현재 높이 갱신
        

        var new_position = target.position - (target.forward * distance_cur) + target.up * height_cur; // 카메라 위치 설정(타겟과 일정거리 유지)

        //new position으로 lemp
        Vector3 smooth_postion = Vector3.Lerp(transform.position, new_position, smooth);
        //var new_position = target.position - (target.forward * distance) + target.up * height; // 카메라 위치 설정(타겟과 일정거리 유지)

        //camera_transform.position = Vector3.Slerp(camera_transform.position, new_position, Time.deltaTime * move_speed);           // 카메라 이동         
        camera_transform.position = smooth_postion;
        camera_transform.LookAt(target.position);        // 타겟을 쳐다본다

    }
}
