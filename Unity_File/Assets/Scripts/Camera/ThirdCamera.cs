using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour
{
    [SerializeField]
    private float distance_cur = 3.0f;        // 타겟과의 거리 (현재)
    private float distance_max = 12.0f;                         // 타겟과의 거리 (최대)
    private float distance_min = 3.0f;                          // 타겟과의 거리 (최소)

    [SerializeField]
    private float height_cur = 0.75f;            // 카메라 높이 (현재)
    private float height_max = 3.0f;                             // 카메라 높이 (최대)
    private float height_min = 0.75f;                             // 카메라 높이 (최소)

    //[SerializeField] private float offset = 1.0f;                // 타겟 좌표에서의 offset (타켓의 발 밑이 좌표라서 주는거)
    private float input_mouse_wheel;
    public float smooth = 2.0f;
    Transform camera_rig_transform;  //카메라 리그 위치
    Transform camera_transform;         //카메라 위치

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
        camera_rig_transform = transform;
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

            //
            //줌인,아웃
            //
            input_mouse_wheel = Input.GetAxis("Mouse ScrollWheel");
            distance_cur = Mathf.Clamp(distance_cur - input_mouse_wheel * 4, distance_min, distance_max); // 현재 거리 갱신
            height_cur = Mathf.Clamp(height_cur - input_mouse_wheel, height_min, height_max); // 현재 높이 갱신

            var new_position = targetVec - (GetPlayerController.transform.forward * distance_cur) + GetPlayerController.transform.up * height_cur; // 카메라 위치 설정(타겟과 일정거리 유지)

            Vector3 smooth_postion = Vector3.Lerp(targetVec, new_position, smooth);
            camera_rig_transform.position = smooth_postion;

            mouse_move += new Vector3(-Input.GetAxis("Mouse Y") * mouse_sensitivity,
                                        Input.GetAxis("Mouse X") * mouse_sensitivity, 0);

            //float move_x = mouse_move.y;
            //float move_y = mouse_move.x;

            //mouse_move.y = Mathf.Clamp(mouse_move.y, -60, 60);
            mouse_move.x = Mathf.Clamp(mouse_move.x, -30, 30);

            //rotateAround할 때 캐릭터 앞쪽/뒷쪽에서 축이 반대여야 의도대로 돌아감. ->절댓값으로 앞/뒤 구분 -> 실패
            //중첩문제!!!!
            //부드럽게 돌아가려면? lerp.

            camera_rig_transform.RotateAround(targetVec, upVec, mouse_move.y); // 타겟을 중심으로, y축 회전(공전), 회전각도
            camera_rig_transform.RotateAround(targetVec, rightVec, mouse_move.x); // 타겟을 중심으로, x축 회전(공전), 회전각도

            camera_rig_transform.LookAt(targetVec);
            Debug.Log(targetVec);

            //공전 마우스 인풋 만큼 회전.마우스 중앙에서 모서리로 가는 만큼
            //pc.mouse_move-- > 캐릭터 컨트롤러에서 mouseMove값. 좌우가 y, 상하가 x

            //커서 숨기기. **인풋매니저에 넣을것**
            //Ctrl+Shift+c 하면 다시 생김
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;

            if (GetPlayerController.throw_mode)
            {
                camera_rig_transform.rotation.SetLookRotation(targetVec);

                Debug.Log(targetVec);

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


