using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotater : MonoBehaviour
{
    // 캐릭터 주위를 회전. 카메라만 별도로 움직임.
    public Transform head;
    public Transform player;
    [SerializeField] private float rotate_angle = 2.0f;        // 카메라 회전각도 (1초에 2도)
    public Vector3 offset;
    float rotate_speed = 2.0f;
    public float minY = -40f;
    public float maxY = 40f;
    float MouseX;
    float MouseY;
    float input_mouse_wheel;
    Vector3 camera_offset;
    Vector3 origin_camera_offset;
    Camera main_cam;
    Camera sub_cam;

    bool ok = true;
    float ok_time = 0;
    float far;

    private void Start()
    {
        MouseX = transform.eulerAngles.y;
        MouseY = transform.eulerAngles.x;
        camera_offset = transform.localPosition - head.transform.localPosition;
        origin_camera_offset = camera_offset;
        main_cam = GetComponent<Camera>();
        sub_cam = gameObject.GetComponentInChildren<Camera>();

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

        MouseX += Input.GetAxis("Mouse X") * rotate_speed;
        MouseY -= Input.GetAxis("Mouse Y") * rotate_speed;
        //MouseY = Mathf.Clamp(MouseY, minY, maxY);
        MouseY = ClampAngle(MouseY, minY, maxY);
        Quaternion ro = Quaternion.Euler(MouseY, MouseX, 0f);
        Vector3 po = ro * camera_offset;

        transform.localRotation = Quaternion.Slerp(transform.localRotation, ro, 0.5f);
        transform.localPosition = Vector3.Lerp(transform.localPosition, po, 0.5f);
        transform.LookAt(head);
        sub_cam.transform.rotation.SetLookRotation(player.forward);
        //transform.RotateAround(target.position, target.right, MouseY);

        input_mouse_wheel = Input.GetAxisRaw("Mouse ScrollWheel");
        if (input_mouse_wheel > 0 && camera_offset.magnitude >= 2.0f)
            camera_offset /= 1.1f;
        else if (input_mouse_wheel < 0 && camera_offset.magnitude <= 10.0f)
            camera_offset *= 1.1f;

        if (camera_offset.magnitude < 3.0f && ok_time >= 0.5f)
            camera_offset *= 1.1f;

        if (ok)
            ok_time += Time.deltaTime;
    }

    float ClampAngle(float angle, float min, float max)
    {
        if (angle < -360)
            angle += 360;
        if (angle > 360)
            angle -= 360;
        return Mathf.Clamp(angle, min, max);
    }

    public void MainToSub()
    {
        //smooth주기

        minY = -20f;
        sub_cam.enabled = true;
        main_cam.enabled = false;
    }

    public void SubToMain()
    {
        minY = -5f;
        main_cam.enabled = true;
        sub_cam.enabled = false;
    }

    private void OnTriggerStay(Collider other)
    {
        ok = false;
        ok_time = 0;

        if (other.CompareTag("Player"))
        {
            camera_offset = origin_camera_offset;
        }
        else if (camera_offset.magnitude > 2.0f)
            camera_offset /= 1.05f;
    }

    private void OnTriggerExit(Collider other)
    {
        ok = true;
    }
}
