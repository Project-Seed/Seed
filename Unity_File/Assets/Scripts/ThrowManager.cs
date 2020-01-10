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

    ThirdCamera tc;

    private void Start()
    {
        tc = GetComponent<ThirdCamera>();
        gravity = 9.8f;
    }

    private void OnMouseDown()
    {
        throw_mode = true;
        //낙하지점예상하기

        float throw_angle;
        throw_angle = tc.mouse_move.x;
        float throw_speed;
        throw_speed = 10.0f;
        s = new Vector3(windVec.x * wind_power, 0, ((2 * throw_speed * Mathf.Cos(throw_angle)) / gravity) + windVec.z * wind_power);
        throw_at = transform.forward + s;
        Debug.Log("throw at : " + throw_angle +" "+throw_at);

    }

    private void OnMouseUp()
    {
        //그 지점으로 던지기.
        

        //던지기모드 종료
        throw_mode = false;
    }
}
