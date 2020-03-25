﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour
{
    public float smooth = 2.0f;
    public Transform camera_rig_transform;  //카메라 리그 위치
    private float rotate_speed = 5.0f;
    private Vector3 camera_offset;
    public bool rotate_cam = true;
    public bool look_player = false;

    [SerializeField]
    float MouseX;
    [SerializeField]
    float MouseY;

    public PlayerController GetPlayerController;
    ThrowManager GetThrowManager;
  
    public Vector3 mouse_move;
    Vector3 throw_position;
    Vector3 move;
    Vector3 center;
    Vector3 arc;
    LineRenderer  line_renderer;
    bool switch_mode;

    public float mouse_sensitivity = 10.0f; // 마우스 감도

    // 클릭하면 던지기 모드 on. 
    // 마우스 포인터 가운데로 고정

    private void Start()
    {
        camera_rig_transform = transform;
        //유지할 거리
        camera_offset = camera_rig_transform.localPosition - GetPlayerController.transform.localPosition;
    }


    void LateUpdate()
    {
        if (InputManager.instance.click_mod == 0)
        {
            Vector3 targetVec;
            Vector3 aim = new Vector3(GetPlayerController.transform.localPosition.x +0.5f,
                GetPlayerController.transform.localPosition.y, 
                GetPlayerController.transform.localPosition.z);

            //throw mode 일 때 카메라 위치는 플레이어 옆쪽.
            targetVec = GetPlayerController.throw_mode ? 
                aim /*임시값. 월드로 더해지네..*/
                : GetPlayerController.transform.localPosition;
           
            if (GameSystem.switch_mode)
            {
                //lerp주기.
            }

            //줌인,아웃
            float input_mouse_wheel = Input.GetAxis("Mouse ScrollWheel");

            if (input_mouse_wheel != 0)
                camera_offset += new Vector3(0,0,input_mouse_wheel);

            //마우스로 시야 전환 각도제한은 camera_rig_transform의 rotation값을 제한.
            //쿼터니언 클램프 어캐함; ; ;
            MouseX = Input.GetAxis("Mouse X") * rotate_speed;
            MouseY = Input.GetAxis("Mouse Y") * rotate_speed;
            //MouseY = Mathf.Clamp(MouseY, -5f, 70f); --> 인풋은 -1,1 이라 소용없다. 입력값제한은 아님.

            Quaternion camera_angle_X;
            camera_angle_X = Quaternion.AngleAxis(MouseX, Vector3.up);

            Quaternion camera_angle_Y;
            camera_angle_Y = Quaternion.AngleAxis(MouseY, -Vector3.right);


            ///쿼터니언 클램프 시행착오들...일단 최종 쿼터니언 회전값은 camera_angle이다.
            
            //Quaternion initY = transform.localRotation * camera_angle_Y;
            //if (Quaternion.Angle(transform.rotation, initY) > 70f)
            //    camera_angle_Y = initY;

            //Quaternion camera_angle = camera_angle_X * camera_angle_Y;
            Quaternion camera_angle = Quaternion.Slerp(camera_angle_X, camera_angle_Y, 0.5f);


            //각도 제한을 위해 쿼터니언 x값을 float로 변환하고 clamp 한 다음 다시 변환해서 넣어줌.
            //float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(camera_angle.x);
            //angleY = Mathf.Clamp(angleY, -5f, 30f);
            //camera_angle.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

            //쿼터니언 각을 오일러로 때려넣는 방법. 왜 안될가
            //camera_angle = Quaternion.Euler(Mathf.Clamp(camera_angle.eulerAngles.x, -5f, 30f), 
            //    camera_angle.eulerAngles.y, 
            //    camera_angle.eulerAngles.z);
            //if (camera_rig_transform.localEulerAngles.x > 70f)
            //    camera_rig_transform.localEulerAngles.Set(70f, camera_rig_transform.localEulerAngles.y, camera_rig_transform.localEulerAngles.z);


            ///

            camera_offset = camera_angle * camera_offset;

            Vector3 newPos = targetVec + camera_offset;
            camera_rig_transform.localPosition = Vector3.Slerp(targetVec, newPos, smooth);
            camera_rig_transform.LookAt(targetVec);
           
            //커서 숨기기. **인풋매니저에 넣을것**
            //Ctrl+Shift+c 하면 다시 생김
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    void DrawArc()
    {
        Debug.Log("Arc");
        Plane player_plane = new Plane(Vector3.up, GetPlayerController.transform.position);
        Ray ray = Camera.main.ScreenPointToRay(GetPlayerController.transform.position);
        float hitdist = 0.0f;
        Vector3 target_point = GetPlayerController.transform.position;
        if (player_plane.Raycast(ray,out hitdist))
        {
            center = (GetPlayerController.transform.position + target_point) * 0.5f;
            center.y -= 2.0f;
            Quaternion target_rotation = Quaternion.LookRotation(center - GetPlayerController.transform.position);
            GetPlayerController.transform.rotation = Quaternion.Slerp(GetPlayerController.transform.rotation, target_rotation, 1.0f * Time.deltaTime);
            RaycastHit hit_info;

            if(Physics.Linecast(GetPlayerController.transform.position,target_point,out hit_info))
            {
                target_point = hit_info.point;
            }
        }
        else
        {
            target_point = GetPlayerController.transform.position;
        }
        Vector3 rel_center = GetPlayerController.transform.position - center;
        Vector3 aim_rel_center = target_point - center;

        for(float index = 0.0f,interval = -0.0417f; interval < 1.0f;)
        {
            arc = Vector3.Slerp(rel_center, aim_rel_center, interval += 0.0417f);
            line_renderer.SetPosition((int)index++, arc + center);

        }
        Debug.Log("End Arc");
    }
}


