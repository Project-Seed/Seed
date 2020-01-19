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

    ThirdCamera tc;
    public GameSystem.Mode mode;
    public GameObject circle_sprite;
    public GameObject seed;
    private GameObject throw_sprite;    // 임시스프라이트
    private GameObject tmp;  //임시씨앗

    private float t;

    private void Start()
    {
        tc = GetComponent<ThirdCamera>();
        gravity = 9.8f;
        throw_at =
        windVec = 
        s = Vector3.zero;
    }

    public void OnThrowMode()
    {
        mode = GameSystem.Mode.ThrowMode;
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
        Debug.Log("throw at : " + throw_angle + " " + throw_at);

        // 해당 지점에 스프라이트 그리기
        // 오브젝트 생성
        if (throw_sprite)
            DestroyImmediate(throw_sprite);
        throw_sprite = Instantiate(circle_sprite, throw_at, Quaternion.AngleAxis(0f, Vector3.right));
    }
    private void Throw()
    {
        //씨앗 생성
        tmp = Instantiate(seed);
        
        StartCoroutine(ThrowingSeed());
    }

    public void ExitThrowMode()
    {
        //던지기모드 종료
        mode = GameSystem.Mode.ThrowMode;

        //씨앗 발사시킴
        Throw();
        DestroyImmediate(throw_sprite);
    }

    IEnumerator ThrowingSeed()
    {
        if (!tmp) yield break;
        t += 0.1f;
        float z = throw_speed * Mathf.Cos(throw_angle) * t;
        float y = throw_speed * Mathf.Sin(throw_angle) - (0.5f * gravity * Mathf.Pow(t, 2));
        tmp.transform.localPosition = new Vector3(transform.position.x, transform.position.y + y, transform.position.z + z);
        if (y < 0)
            yield break;
        else
        {
            yield return new WaitForSeconds(0.1f);
            StartCoroutine(ThrowingSeed());
        }
    }

}
