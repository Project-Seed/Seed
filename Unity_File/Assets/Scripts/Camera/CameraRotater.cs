using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotater : MonoBehaviour
{
    // 캐릭터 주위를 회전. 카메라만 별도로 움직임.
    public Transform head;
    public Transform player;
    public Vector3 offset; 
    public float rotate_speed = 2.0f;
    public float minY = -40f;
    public float maxY = 40f;
    float MouseX;
    float MouseY;
    float input_mouse_wheel;
    Vector3 camera_offset;
    Vector3 origin_camera_offset;
    public float maxZoomin = 1.5f;
    public float maxZoomOut = 8.0f;
    public bool lockZoom;
    private bool isZoomIn;

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
    }
    //private void Update()
    //{
    //    Ray rayFront = new Ray(transform.position, transform.forward);
    //    if(Physics.Raycast(rayFront,out RaycastHit hit, camera_offset.magnitude))
    //    {
    //        if (hit.transform.CompareTag("Player")) return;
    //        //플레이어 말고 다른게 부딪혔다! (지형 지물 빼고 다 Raycast Off해놓기)
    //        if (camera_offset.magnitude > maxZoomin)
    //            camera_offset /= 1.1f;
    //    }
    //}
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

        // po = LineCast(po);

        LineCast();
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
        if (camera_offset.magnitude < origin_camera_offset.magnitude)
        {
            camera_offset *= 1.05f;
            yield return null;
        }
        else if (camera_offset.magnitude >= origin_camera_offset.magnitude)
        {
            camera_offset = origin_camera_offset;
            isZoomIn = false;
            yield break;
        }
    }

    IEnumerator SmoothZoomInCam(float distance)
    {
        if (camera_offset.magnitude > distance)
        {
            camera_offset /= 1.05f; //줌인
            yield return null;
        }
        else if (camera_offset.magnitude <= maxZoomin)
        {
            camera_offset = camera_offset.normalized * maxZoomin;
            isZoomIn = false;
            yield break;
        }
    }
    void LineCast()
    {
        float distance;
        Vector3 desiredCamPos = head.TransformPoint(transform.localPosition.normalized * maxZoomOut);
        if (Physics.Linecast(head.position, desiredCamPos, out RaycastHit hit))
        {
            distance = Mathf.Clamp(hit.distance, maxZoomin, maxZoomOut);
            StartCoroutine(SmoothZoomInCam(distance));
            //distance만큼 줌인.

            //return new Vector3(hit.point.x + hit.normal.x * 0.5f, hit.point.y, hit.point.z + hit.normal.z * 0.5f);
            //조ㅓㅏ표 이동말고 원래 카메라 회전이랑 같은 원리로 이동시켜야함. y를 위로 올리자
        }
        else
            StartCoroutine(SmoothBackCam());
    }

    //IEnumerator CheckBehind()
    //{
    //    //카메라가 줌인된 상태(isZoomin)가 되면 호출.
    //    //프레임마다 뒤에 뭐가 있나 검사하고
    //    //뭐가 없으면 뒤로 Back. magnitude가 최대치가 되면(or 그 이상) 최대치로 고정하고 Zoomin상태 false로 돌림.
    //    while (camera_offset.magnitude < origin_camera_offset.magnitude)
    //    {
    //        //카메라 뒷 공간이 origin offset으로 갈 만큼 있어야함.
    //        //origin이랑 지금 클로즈업 된 카메라 사이의 거리만큼 레이를 쏘고 그만큼 여유 있으면 뒤로감
    //        Ray ray = new Ray(camera_offset, origin_camera_offset - camera_offset);
    //        float backDistance = origin_camera_offset.magnitude - camera_offset.magnitude;
    //        if (!Physics.Linecast(transform.position,head.position,out RaycastHit hit))
    //        {
    //            StartCoroutine(SmoothBackCam());
    //            new Vector3(hit.point.x + hit.normal.x * 0.5f, hit.point.y, hit.point.z + hit.normal.z * 0.5f);
    //        }
    //        else
    //            StopCoroutine(SmoothBackCam());
    //        yield return null;
    //    }
    //}
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) return;
        if (other.CompareTag("Ground"))
        {
            if (camera_offset.magnitude > maxZoomin)
            {
                camera_offset /= 1.05f; //줌인
            }
            else if (camera_offset.magnitude <= maxZoomin)
            {
                camera_offset = camera_offset.normalized * maxZoomin;
                isZoomIn = false;
            }
        }

    }
    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("stay");
    //    //ok = false;
    //    //ok_time = 0;
    //    isCollided = true;
    //    StopCoroutine(SmoothBackCam());

    //    if (other.CompareTag("Player"))
    //    {
    //        camera_offset = origin_camera_offset;
    //    }
    //    else if (camera_offset.magnitude > maxZoomin)
    //        camera_offset /= 1.1f;
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    isZoomIn = false;
    //    Ray ray = new Ray(transform.localPosition, camera_offset);
    //    //카메라 뒷 공간이 origin offset으로 갈 만큼 있어야함.
    //    //origin이랑 지금 클로즈업 된 카메라 사이의 거리만큼 레이를 쏘고 그만큼 여유 있으면 뒤로감
    //    float backDistance = origin_camera_offset.magnitude - camera_offset.magnitude;
    //    Debug.DrawLine(transform.position, transform.position + origin_camera_offset, Color.red, 3.0f);
    //    if (!Physics.Raycast(ray, backDistance))
    //    {
    //        StartCoroutine(SmoothBackCam());
    //    }

    //    if (other.CompareTag("Player")) return;
    //}
}
