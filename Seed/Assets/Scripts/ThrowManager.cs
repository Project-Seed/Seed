using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowManager : MonoBehaviour
{
    public float s;
    public bool throw_mode;

    ThirdCamera tc;

    private void Start()
    {
        tc = GetComponent<ThirdCamera>();
    }

    private void OnMouseDown()
    {
        throw_mode = true;
        //낙하지점예상하기

        float throw_angle;
        throw_angle = tc.mouse_move.x;
        float throw_speed;
        throw_speed = 10.0f;
        s = (2 * throw_speed * Mathf.Cos(throw_angle)) / 9.8f;
        Debug.Log("throw at : " + s);

    }

    private void OnMouseUp()
    {
        //그 지점으로 던지기.

        s = 0.0f;
        //던지기모드 종료
        throw_mode = false;
    }
}
