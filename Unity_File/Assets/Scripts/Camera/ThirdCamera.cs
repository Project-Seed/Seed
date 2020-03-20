using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour
{
    [SerializeField]
    private float distance_cur = 1.0f;        // 타겟과의 거리 (현재)
    private float distance_max = 1.0f;                         // 타겟과의 거리 (최대)
    private float distance_min = -1.0f;                          // 타겟과의 거리 (최소)

    [SerializeField]
    private float height_cur = 0.75f;            // 카메라 높이 (현재)
    private float height_max = 3.0f;                             // 카메라 높이 (최대)
    private float height_min = 0.75f;                             // 카메라 높이 (최소)

    //[SerializeField] private float offset = 1.0f;                // 타겟 좌표에서의 offset (타켓의 발 밑이 좌표라서 주는거)
    public float smooth = 2.0f;
    public Transform camera_rig_transform;  //카메라 리그 위치
    Transform camera_transform;         //카메라 위치
    Camera cam;
    private float rotate_speed = 5.0f;
    private Vector3 camera_offset;
    public bool rotate_cam = true;
    public bool look_player = false;
    
    public PlayerController GetPlayerController;
    ThrowManager GetThrowManager;
  
    public Vector3 mouse_move;
    Vector3 throw_position;
    Vector3 move;
    Vector3 center;
    Vector3 arc;
    LineRenderer  line_renderer;

    public float mouse_sensitivity = 10.0f; // 마우스 감도

    // 클릭하면 던지기 모드 on. 
    // 마우스 포인터 가운데로 고정

    private void Start()
    {
        //GetPlayerController = GetComponent<PlayerController>();
        //GetThrowManager = GetComponent<ThrowManager>();
        camera_transform = GetComponentInChildren<Transform>();
        cam = GetComponentInChildren<Camera>();
        camera_rig_transform = transform;
        //유지할 거리
        camera_offset = camera_rig_transform.localPosition - GetPlayerController.transform.localPosition;
    }


    void LateUpdate()
    {
        if (InputManager.instance.click_mod == 0)
        {
            Vector3 targetVec;

            Vector3 upVec = GetPlayerController.upVec;
            Vector3 rightVec = GetPlayerController.rightVec;

            //throw mode 일 때 카메라 위치는 플레이어 옆쪽. LookAt은 착지 예상지점.
            targetVec = GetPlayerController.throw_mode ? 
                GetPlayerController.transform.position + new Vector3(0.5f, 0f, 2f) /*임시값*/
                : GetPlayerController.targetVec;

            if (GameSystem.switch_mode)
            {
                //lerp주기.
            }

            //줌인,아웃
            float input_mouse_wheel = Input.GetAxis("Mouse ScrollWheel");

            if (input_mouse_wheel != 0)
                camera_offset += new Vector3(0,0,input_mouse_wheel);

            Quaternion camera_angle_X = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * rotate_speed, Vector3.up);
            Quaternion camera_angle_Y = Quaternion.AngleAxis(-Input.GetAxis("Mouse Y") * rotate_speed, Vector3.right);
            Quaternion camera_angle = Quaternion.Slerp(camera_angle_X, camera_angle_Y, 0.5f);

            camera_offset = camera_angle * camera_offset;

            Vector3 newPos = GetPlayerController.transform.localPosition + camera_offset;
            
            camera_rig_transform.localPosition = Vector3.Slerp(camera_rig_transform.localPosition, newPos, smooth);

            camera_rig_transform.LookAt(GetPlayerController.transform);
           
            //커서 숨기기. **인풋매니저에 넣을것**
            //Ctrl+Shift+c 하면 다시 생김
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (GetPlayerController.throw_mode)
            {
                camera_rig_transform.localRotation.SetLookRotation(targetVec);

                //착지 지점이 잘 보이는 지 확인하기.
                //던지기 카메라는 줌인 줌아웃 없고 마우스 움직일때 시야가 움직이긴 해야함.
                //UI에 에임점 찍기
                //

                //throw_position = my_transform.forward + new Vector3(0, 0, pc.throw_position);

                //float power; //던지는 힘
                //float m; //질량
                //float a;//가속도
                //float v;//속도
                //Vector3 dir;//방향
                //power = 10.0f;
                //m = 0.1f;
                //dir = new Vector3(mouse_move,)
                //Vector3 throw_vec = new Vector3(0, 0, pc.throw_position);
                //my_transform.position = my_transform.forward + throw_vec;
                //Debug.Log("aim change " + my_transform);

            }
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


