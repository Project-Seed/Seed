using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    // 캐릭터 주위를 회전. 카메라만 별도로 움직임.
    public Transform target;      
    [SerializeField] private float rotate_angle = 2.0f;        // 카메라 회전각도 (1초에 2도)
    public Vector3 offset;
    float rotate_speed = 2.0f;
    public float minY = -2f;
    public float maxY = 60f;
    float MouseX;
    float MouseY;
    float input_mouse_wheel;
    Vector3 camera_offset;

    private void Start()
    {
        MouseX = transform.eulerAngles.y;
        MouseY = transform.eulerAngles.x;
        camera_offset = transform.localPosition - target.transform.localPosition;
    }

    private void LateUpdate()
    {
        MouseX += Input.GetAxis("Mouse X") * rotate_speed;
        MouseY -= Input.GetAxis("Mouse Y") * rotate_speed;
        //MouseY = Mathf.Clamp(MouseY, minY, maxY);
        MouseY = ClampAngle(MouseY, minY, maxY);
        Quaternion ro = Quaternion.Euler(MouseY, MouseX, 0f);
        Vector3 po = ro * camera_offset;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, ro, 0.5f);
        transform.localPosition = Vector3.Slerp(transform.localPosition, po, 0.5f); ;
        transform.LookAt(target);
        //transform.RotateAround(target.position, target.right, MouseY);
       
        input_mouse_wheel = Input.GetAxisRaw("Mouse ScrollWheel");
        if (input_mouse_wheel > 0)
            camera_offset /= 1.1f;
        else if (input_mouse_wheel < 0)
            camera_offset *= 1.1f;

    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }
}
