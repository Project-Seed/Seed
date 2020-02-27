using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public Vector3 throw_at;
    private Vector3 s;
    private Vector3 windVec;
    private float wind_power;
    public bool throw_mode;
    private float throw_power;
    private float gravity;
    private float throw_angle;
    private float throw_speed;
    private bool throw_done;

    ThirdCamera tc;
    public GameSystem.Mode mode;
    public GameObject circle_sprite;
    public GameObject seed;
    private GameObject throw_sprite;    // 임시스프라이트
    private GameObject tmp;  //임시씨앗

    Vector3 start_transform;
    private float t;

    private void Start()
    {
        tc = GetComponent<ThirdCamera>();
        gravity = 9.8f;
        throw_at =
        windVec = 
        s = Vector3.zero;
        start_transform = transform.position + new Vector3(0, 1.3f, 0);
    }

    public void OnThrowMode()
    {
        if (!throw_done) return;
        mode = GameSystem.Mode.ThrowMode;
        throw_done = false;
        CalcThrow();
    }

    private void CalcThrow()
    {
        // 낙하지점 계산
        float horizonDist;  //수평 도달 거리
        throw_angle = tc.mouse_move.x;
        throw_speed = 10.0f;
        horizonDist = (Mathf.Pow(throw_speed, 2) * Mathf.Sin(2 * throw_angle)) / gravity;
        s = new Vector3(windVec.x * wind_power, 0, horizonDist + windVec.z * wind_power);
        throw_at = transform.forward + s;
        Debug.Log("forward : " + transform.forward);

        Debug.Log("throw at : " + throw_angle + " " + throw_at+"d "+horizonDist);

        // 해당 지점에 스프라이트 그리기
        // 오브젝트 생성
        if (throw_sprite)
            DestroyImmediate(throw_sprite);
        throw_sprite = Instantiate(circle_sprite, throw_at, Quaternion.AngleAxis(0f, Vector3.right));
    }
    public void ExitThrowMode()
    {
        //던지기모드 종료
        mode = GameSystem.Mode.BasicMode;

        //씨앗 발사시킴
        Throw();
        DestroyImmediate(throw_sprite);
    }
    private void Throw()
    {
        //씨앗 생성
        tmp = Instantiate(seed);
        
        StartCoroutine(ThrowingSeed());
    }


    IEnumerator ThrowingSeed()
    {
        if (!tmp) yield break;
        t += 0.05f;
        float z = throw_speed * Mathf.Cos(throw_angle) * t;
        float y = throw_speed * Mathf.Sin(throw_angle) * t - (0.5f * gravity * Mathf.Pow(t, 2));
        tmp.transform.localPosition = new Vector3(start_transform.x, start_transform.y + y, start_transform.z + z);
        //Debug.Log("go to " +tmp.transform.localPosition);

        //아래 조건 착지했을 때(지면 or 오브젝트와 충돌했을 때)로 바꿀 예정
        if (y < 0)
        {
            t = 0;
            DestroyImmediate(tmp);
            throw_done = true;
            yield break;
        }
        else
        {
            yield return new WaitForSeconds(0.05f);
            StartCoroutine(ThrowingSeed());
        }
    } 

}
