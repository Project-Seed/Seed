//using System.Collections;
//using UnityEngine;

//public class ThirdCamera : MonoBehaviour
//{
//    public float smooth = 1.0f;
//    public Transform camera_rig_transform;  //카메라 리그 위치
//    public Transform aim;
//    private float rotate_speed = 5.0f;
//    private Vector3 camera_offset;

//    public bool rotate_cam = true;
//    public bool look_player = false;

//    //[SerializeField] float MouseX;
//    //[SerializeField] float MouseY;
//    public float minY = -30f;
//    public float maxY = 30f;
//    public Quaternion camera_rotate_X;
//    Quaternion camera_rotate_Y;

//    public PlayerController GetPlayerController;
//    ThrowManager GetThrowManager;

//    public Vector3 mouse_move;
//    Vector3 throw_position;
//    Vector3 move;
//    Vector3 center;
//    Vector3 arc;
//    LineRenderer line_renderer;
//    bool switch_mode;
//    float scroll;
//    float input_mouse_wheel;

//    public float mouse_sensitivity = 10.0f; // 마우스 감도

//    // 클릭하면 던지기 모드 on. 
//    // 마우스 포인터 가운데로 고정

//    private void Start()
//    {
//        //camera_rig_transform = transform;
//        //유지할 거리
//        //camera_offset = camera_rig_transform.localPosition - GetPlayerController.transform.localPosition;
//        //GetThrowManager = GetPlayerController.GetComponent<ThrowManager>();
//        camera_rotate_X = GetPlayerController.transform.localRotation;
//        camera_rotate_Y = transform.localRotation;
//    }

//    void LateUpdate()
//    {
//        if (InputManager.instance.click_mod == 0)
//        {
//            //Vector3 targetVec = GetPlayerController.transform.localPosition;
//            //Quaternion camera_angle;
//            if (GameSystem.switch_mode)
//            {
//                //lerp주기.
//            }

//            //줌인,아웃 z 단순히 더하는거 안됨. 회전하면 바뀌니까 뒤쪽으로 이동시켜야함 얘도 self 좌표계로.
//            //나중에 제한 두기.
//            //연산은 아래에서.
//            input_mouse_wheel = Input.GetAxisRaw("Mouse ScrollWheel");

//            //camera_rig_transform.Translate(-camera_rig_transform.forward * input_mouse_wheel, Space.Self);

//            //    Vector3.MoveTowards(camera_rig_transform.localPosition, -camera_rig_transform.forward, 0.5f);


//            //마우스로 시야 전환 throwmode 일땐 rotate speed 줄이기.
//            //각도제한은 camera_rig_transform의 rotation값을 제한.
//            //쿼터니언 클램프 어캐함; ; ;
//            float MouseX = Input.GetAxis("Mouse X") * rotate_speed;
//            float MouseY = Input.GetAxis("Mouse Y") * rotate_speed;
//            //MouseY = Mathf.Clamp(MouseY, -5f, 70f); --> 인풋은 -1,1 이라 소용없다. 입력값제한은 아님.

//            //camera_angle_X = Quaternion.AngleAxis(MouseX, Vector3.up); //이러면 월드 축에 맞는 곳에서만 됨.
//            camera_rotate_X *= Quaternion.Euler(0f, MouseX, 0f);

//            camera_rotate_Y *= Quaternion.Euler(-MouseY, 0f, 0f);
//            //camera_rotate_Y = ClampRotation(camera_rotate_Y);

//            ///쿼터니언 클램프 시행착오들...일단 최종 쿼터니언 회전값은 camera_angle이다.

//            //Quaternion initY = transform.localRotation * camera_angle_Y;
//            //if (Quaternion.Angle(transform.rotation, initY) > 70f)
//            //    camera_angle_Y = initY;

//            //Quaternion camera_angle = camera_angle_X * camera_angle_Y;
//            //camera_angle = Quaternion.Slerp(camera_angle_X, camera_angle_Y, 0.5f);

//            //transform.localRotation = Quaternion.Slerp(transform.localRotation, camera_rotate_Y, smooth);
//            transform.RotateAround(aim.position, aim.right, ClampRotation(camera_rotate_Y)*Time.deltaTime);
//            //각도 제한을 위해 쿼터니언 x값을 float로 변환하고 clamp 한 다음 다시 변환해서 넣어줌.
//            //float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(camera_angle.x);
//            //angleY = Mathf.Clamp(angleY, -5f, 30f);
//            //camera_angle.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

//            //쿼터니언 각을 오일러로 때려넣는 방법. 왜 안될가
//            //camera_angle = Quaternion.Euler(Mathf.Clamp(camera_angle.eulerAngles.x, -5f, 30f), 
//            //    camera_angle.eulerAngles.y, 
//            //    camera_angle.eulerAngles.z);
//            //if (camera_rig_transform.localEulerAngles.x > 70f)
//            //    camera_rig_transform.localEulerAngles.Set(70f, camera_rig_transform.localEulerAngles.y, camera_rig_transform.localEulerAngles.z);


//            ///

//            //camera_offset = camera_angle * camera_offset;

//            transform.LookAt(aim.position);

//            //여기에 throwmode 일때 클로즈업 하는 거 추가하기.

//            //클로즈업
//            //if (input_mouse_wheel != 0 && !GetPlayerController.throw_mode)
//            //    if (input_mouse_wheel > 0)
//            //        camera_offset /= 1.1f;
//            //    else
//            //        camera_offset *= 1.1f;

//            //if (GetPlayerController.throw_mode)
//            //{
//            //    aim = aim_transform.position;
//            //    newPos = aim + camera_offset / 2;
//            //}
//            //else
//            //{
//            //    aim = targetVec;
//            //    newPos = targetVec + camera_offset;
//            //}
//            //camera_rig_transform.localPosition = Vector3.Slerp(camera_rig_transform.localPosition, newPos, smooth);
//            //camera_rig_transform.LookAt(targetVec);

//            //커서 숨기기. **인풋매니저에 넣을것**
//            //Ctrl+Shift+c 하면 다시 생김
//            Cursor.visible = false;
//            Cursor.lockState = CursorLockMode.Locked;
//        }
//        else
//        {
//            Cursor.visible = true;
//            Cursor.lockState = CursorLockMode.None;
//        }
//    }
//    private float ClampRotation(Quaternion quaternion)
//    {
//        quaternion.x /= quaternion.w;
//        quaternion.y /= quaternion.w;
//        quaternion.z /= quaternion.w;
//        quaternion.w = 1.0f;

//        float angleY = 2.0f * Mathf.Rad2Deg * Mathf.Atan(quaternion.x);
//        angleY = Mathf.Clamp(angleY, minY, minY);
//        //quaternion.x = Mathf.Tan(0.5f * Mathf.Deg2Rad * angleY);

//        return angleY;
//    }

//    private void OnCollisionEnter(Collision collision)
//    {
//        if (collision.transform.CompareTag("Ground"))
//            Debug.Log("Collder");
//    }


//    private IEnumerator SubCam()
//    {
//        if (!GetPlayerController.throw_mode)
//            yield break;

//        camera_rig_transform.Translate(-camera_rig_transform.forward * input_mouse_wheel, Space.Self);
//        yield return null;
//    }

//    void DrawArc()
//    {
//        Debug.Log("Arc");
//        Plane player_plane = new Plane(Vector3.up, GetPlayerController.transform.position);
//        Ray ray = Camera.main.ScreenPointToRay(GetPlayerController.transform.position);
//        float hitdist = 0.0f;
//        Vector3 target_point = GetPlayerController.transform.position;
//        if (player_plane.Raycast(ray, out hitdist))
//        {
//            center = (GetPlayerController.transform.position + target_point) * 0.5f;
//            center.y -= 2.0f;
//            Quaternion target_rotation = Quaternion.LookRotation(center - GetPlayerController.transform.position);
//            GetPlayerController.transform.rotation = Quaternion.Slerp(GetPlayerController.transform.rotation, target_rotation, 1.0f * Time.deltaTime);
//            RaycastHit hit_info;

//            if (Physics.Linecast(GetPlayerController.transform.position, target_point, out hit_info))
//            {
//                target_point = hit_info.point;
//            }
//        }
//        else
//        {
//            target_point = GetPlayerController.transform.position;
//        }
//        Vector3 rel_center = GetPlayerController.transform.position - center;
//        Vector3 aim_rel_center = target_point - center;

//        for (float index = 0.0f, interval = -0.0417f; interval < 1.0f;)
//        {
//            arc = Vector3.Slerp(rel_center, aim_rel_center, interval += 0.0417f);
//            line_renderer.SetPosition((int)index++, arc + center);

//        }
//        Debug.Log("End Arc");
//    }

//}


