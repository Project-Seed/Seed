﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotater : MonoBehaviour
{
    // 캐릭터 주위를 회전. 카메라만 별도로 움직임.
    public Transform head;
    public Transform player;
    [SerializeField] private float rotate_angle = 2.0f;        // 카메라 회전각도 (1초에 2도)
    public Vector3 offset;
    public float rotate_speed = 2.0f;
    public float minY = -40f;
    public float maxY = 40f;
    float MouseX;
    float MouseY;
    float input_mouse_wheel;
    Vector3 camera_offset;
    Vector3 origin_camera_offset;
    float maxZoomin = 2.0f;
    float maxZoomOut = 8.0f;
    public bool lockZoom;
    private bool isCollided;

    bool ok = true;
    float ok_time = 0;
    float far;
    public void ToOriginOffset()
    {
        camera_offset = origin_camera_offset;
    }
    public IEnumerator CameraShake(float duration, float mag)
    {
        float time = 0.0f;
        while (time < duration)
        {
            float z = Random.Range(-1f, 1f) * mag;

            camera_offset *= 1.05f;
            time += Time.deltaTime;

            yield return null;
        }
        camera_offset = origin_camera_offset;
    }
    private void Awake()
    {
        MouseX = transform.eulerAngles.y;
        MouseY = transform.eulerAngles.x;
        camera_offset = transform.localPosition - head.transform.localPosition;
        origin_camera_offset = camera_offset;

        far = camera_offset.magnitude;
    }

    private void LateUpdate()
    {
        if (InputManager.instance.click_mod != 0)
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            return;
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (!lockZoom)
        {
            input_mouse_wheel = Input.GetAxisRaw("Mouse ScrollWheel");
            if (input_mouse_wheel > 0 && camera_offset.magnitude >= maxZoomin)
                camera_offset /= 1.1f;
            else if (input_mouse_wheel < 0 && camera_offset.magnitude <= maxZoomOut)
                camera_offset *= 1.1f;
        }

        MouseX += Input.GetAxis("Mouse X") * rotate_speed;
        MouseY -= Input.GetAxis("Mouse Y") * rotate_speed;
        //MouseY = Mathf.Clamp(MouseY, minY, maxY);
        MouseY = ClampAngle(MouseY, minY, maxY);
        Quaternion ro = Quaternion.Euler(MouseY, MouseX, 0f);
        Vector3 po = ro * camera_offset;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, ro, 0.5f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, po, 0.5f);
        transform.LookAt(head);
        //sub_cam.transform.rotation.SetLookRotation(player.forward);
        //transform.RotateAround(target.position, target.right, MouseY);


        //if (camera_offset.magnitude < 3.0f && ok_time >= 0.5f)
        //    camera_offset *= 1.1f;

        //if (ok)
        //    ok_time += Time.deltaTime;
        //레이캐스팅

    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    //public void MainToSub()
    //{
    //    //smooth주기

    //    minY = -20f;
    //    sub_cam.enabled = true;
    //    main_cam.enabled = false;
    //}

    //public void SubToMain()
    //{
    //    minY = -5f;
    //    main_cam.enabled = true;
    //    sub_cam.enabled = false;
    //}

    //카메라가 어디엔가 닿으면
    // 클로즈업(단, 캐릭터와 일정 거리 이상 가까워지면 안됨)
    // 뒤에 아무것도 없다면 다시 줌아웃.
    IEnumerator SmoothBackCam()
    {
        while (camera_offset.magnitude < origin_camera_offset.magnitude)
        {
            if (!isCollided)
                camera_offset *= 1.1f;
            yield return null;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        StopCoroutine(SmoothBackCam());
        isCollided = true;

    }
    private void OnTriggerStay(Collider other)
    {
        //ok = false;
        //ok_time = 0;
        isCollided = true;
        StopCoroutine(SmoothBackCam());

        if (other.CompareTag("Player"))
        {
            camera_offset = origin_camera_offset;
        }
        else if (camera_offset.magnitude > maxZoomin)
            camera_offset /= 1.1f;
    }

    private void OnTriggerExit(Collider other)
    {
        isCollided = false;
        Ray ray = new Ray(transform.localPosition, camera_offset);
        //카메라 뒷 공간이 origin offset으로 갈 만큼 있어야함.
        //origin이랑 지금 클로즈업 된 카메라 사이의 거리만큼 레이를 쏘고 그만큼 여유 있으면 뒤로감
        float backDistance = origin_camera_offset.magnitude - camera_offset.magnitude;
        Debug.DrawLine(camera_offset, origin_camera_offset, Color.black,3.0f);
        if (!Physics.Raycast(ray, backDistance))
        {
            StartCoroutine(SmoothBackCam());
        }

        if (other.CompareTag("Player")) return;
    }
}
