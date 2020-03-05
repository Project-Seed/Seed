using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdCamera : MonoBehaviour
{
    //[SerializeField]
    //float gravity = 9.81f;
    [SerializeField]
    float run_speed = 5.0f; 
    //[SerializeField]
    //float cam_speed = 2.0f; // 카메라 속도
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
    LineRenderer line_renderer;

    Quaternion targetRotation;
    Vector3 gap;
    public float mouse_sensitivity = 10.0f; // 마우스 감도

    // 왜 떨리는가?
    // 마우스 움직일 때마다 카메라가 회전. 동시에 캐릭터도 회전.
    // 떨리지 않으려면 캐릭터의 이동이 끝난 후 카메라가 따라가는 방식이어야함. 회전할 때 카메라가 먼저 돌고 캐릭터 돌리는데 렌더링 순서는 반대라서.
    // 회전 : 에임 돌리면 캐릭터가 회전. 카메라가 그 같은 수치로 (그러나 반대방향으로) 회전. 이 때 회전은 캐릭터 중심 Around회전.
    // 이동 : smoothdamp로 캐릭터 위치로 카메라가 따라감.

    // Around로 회전하고 LookAt으로 쳐다보는데 마우스 위아래 하면 LookAt할 곳이 바뀌기 때문에 문제가 생겼다 위아래 회전해야되는데 룩엣으로 끝나야해. 룩엣에 mouse_move 단순히 더해서는 안됨

    // 클릭하면 던지기 모드 on. 
    // 마우스 포인터 가운데로 고정

    private void Start()
    {
        //GetPlayerController = GetComponent<PlayerController>();
        GetThrowManager = GetComponent<ThrowManager>();
        camera_transform = GetComponentInChildren<Transform>();
        camera_rig_transform = transform;
    }

    void FixedUpdate()
    {
        //mouse_move += new Vector3(-Input.GetAxis("Mouse Y") * mouse_sensitivity,
        //                            Input.GetAxis("Mouse X") * mouse_sensitivity, 0);

        //Debug.Log("카메라 : " + mouse_input);
        //MoveCalc(1.0f);
    }

    void LateUpdate()
    {
        //if (pc.throw_mode)
        //{
        //던지기 모드 활성화 -> 포물선 (힘, 각도) 도착지점 계산. 도착지점에 표시. 그곳으로 카메라 움직임.

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


        //}
        Vector3 targetVec = GetPlayerController.targetVec;
        Vector3 upVec = GetPlayerController.upVec;
        Vector3 rightVec = GetPlayerController.rightVec;


        //이동
        //줌인,아웃
        input_mouse_wheel = Input.GetAxis("Mouse ScrollWheel");
        distance_cur = Mathf.Clamp(distance_cur - input_mouse_wheel*4, distance_min, distance_max); // 현재 거리 갱신
        height_cur = Mathf.Clamp(height_cur - input_mouse_wheel, height_min, height_max); // 현재 높이 갱신

        var new_position = targetVec - (GetPlayerController.transform.forward * distance_cur) + GetPlayerController.transform.up * height_cur; // 카메라 위치 설정(타겟과 일정거리 유지)

        Vector3 smooth_postion = Vector3.Lerp(targetVec, new_position, smooth);  
        camera_rig_transform.position = smooth_postion;


        //공전 마우스 인풋 만큼 회전.마우스 중앙에서 모서리로 가는 만큼
        //pc.mouse_move-- > 캐릭터 컨트롤러에서 mouseMove값. 좌우가 y, 상하가 x

         mouse_move += new Vector3(-Input.GetAxis("Mouse Y") * mouse_sensitivity,
                                    Input.GetAxis("Mouse X") * mouse_sensitivity, 0);

        //float move_x = mouse_move.y;
        //float move_y = mouse_move.x;

        //mouse_move.y = Mathf.Clamp(mouse_move.y, -60, 60);
        mouse_move.x = Mathf.Clamp(mouse_move.x, -30, 30);

        //rotateAround할 때 캐릭터 앞쪽/뒷쪽에서 축이 반대여야 의도대로 돌아감. ->절댓값으로 앞/뒤 구분 -> 실패
        //중첩문제!!!!
        camera_rig_transform.RotateAround(targetVec, upVec, mouse_move.y); // 타겟을 중심으로, y축 회전(공전), 회전각도
        camera_rig_transform.RotateAround(targetVec, rightVec, mouse_move.x); // 타겟을 중심으로, x축 회전(공전), 회전각도
        
        camera_rig_transform.LookAt(targetVec);
        
        //커서 숨기기. **인풋매니저에 넣을것**
        //Ctrl+Shift+c 하면 다시 생김
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Balance()
    {
        if (camera_transform.eulerAngles.z != 0)
            camera_transform.eulerAngles = new Vector3(camera_transform.eulerAngles.x, camera_transform.eulerAngles.y, 0);
    }

    void CameraDistanceCtrl()
    {
        //    Camera.main.transform.localPosition += new Vector3(0, 0, Input.GetAxisRaw("Mouse ScrollWheel") * 2.0f); //휠로 카메라의 거리를 조절한다.
        //    if (-1 < Camera.main.transform.localPosition.z)
        //        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -1);    //최대로 가까운 수치
        //    else if (Camera.main.transform.localPosition.z < -5)
        //        Camera.main.transform.localPosition = new Vector3(Camera.main.transform.localPosition.x, Camera.main.transform.localPosition.y, -5);    //최대로 먼 수치
        //
       
    }

    //void MoveCalc(float ratio)
    //{
    //    float tempMoveY = move.y;
    //    move.y = 0; //이동에는 xz값만 필요하므로 temp에 저장해놓고 일단 0으로 만듦
    //    Vector3 inputMoveXZ = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
    //    //대각선 이동이 루트2 배의 속도를 갖는 것을 막기위해 속도가 1 이상 된다면 노말라이즈 후 속도를 곱해 어느 방향이든 항상 일정한 속도가 되게 한다.
    //    float inputMoveXZMgnitude = inputMoveXZ.sqrMagnitude; //sqrMagnitude연산을 두 번 할 필요 없도록 따로 저장
    //    inputMoveXZ = camera_transform.TransformDirection(inputMoveXZ);
    //    if (inputMoveXZMgnitude <= 1)
    //        inputMoveXZ *= run_speed;
    //    else
    //        inputMoveXZ = inputMoveXZ.normalized * run_speed;

    //    //조작 중에만 카메라의 방향에 상대적으로 캐릭터가 움직이도록 한다.
    //    if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
    //    {
    //        Quaternion cameraRotation = camera_parent_transform.rotation;
    //        cameraRotation.x = cameraRotation.z = 0;    //y축만 필요하므로 나머지 값은 0으로 바꾼다.
    //        //카메라 리그가 돌아가는 대로 카메라 돌리고 그대로 모델도 돌림.
    //        //camera_transform.rotation = Quaternion.Slerp(camera_transform.rotation, cameraRotation, 10.0f * Time.deltaTime);
            
    //        if (move != Vector3.zero)//Quaternion.LookRotation는 (0,0,0)이 들어가면 경고를 내므로 예외처리 해준다.
    //        {
    //            Quaternion characterRotation = Quaternion.LookRotation(move); //보는 방향이 캐릭터 방향임.
    //            characterRotation.x = characterRotation.z = 0;
    //            model.rotation = Quaternion.Slerp(model.rotation, characterRotation, 10.0f * Time.deltaTime);   //그쪽으로 모델 돌림
    //        }

    //        //관성을 위해 MoveTowards를 활용하여 서서히 이동하도록 한다.
    //        move = Vector3.MoveTowards(move, inputMoveXZ, ratio * run_speed);
    //    }
    //    else
    //    {
    //        //조작이 없으면 서서히 멈춘다.
    //        move = Vector3.MoveTowards(move, Vector3.zero, (1 - inputMoveXZMgnitude) * run_speed * ratio);
    //    }
    //    move.y = tempMoveY; //y값 복구
    //}

    void GradientCheck()
    {
        if (Physics.Raycast(camera_transform.position, Vector3.down, 0.2f))
        //경사로를 구분하기 위해 밑으로 레이를 쏘아 땅을 확인한다.
        //CharacterController는 밑으로 지속적으로 Move가 일어나야 땅을 체크하는데 -y값이 너무 낮으면 조금만 경사져도 공중에 떠버리고 너무 높으면 절벽에서 떨어질때 추락하듯 바로 떨어진다.
        //완벽하진 않지만 캡슐 모양의 CharacterController에서 절벽에 떨어지기 직전엔 중앙에서 밑으로 쏘아지는 레이에 아무것도 닿지 않으므로 그때만 -y값을 낮추면 경사로에도 잘 다니고
        //절벽에도 자연스럽게 천천히 떨어지는 효과를 줄 수 있다.
        {
            move.y = -5;
        }
        else
            move.y = -1;
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


