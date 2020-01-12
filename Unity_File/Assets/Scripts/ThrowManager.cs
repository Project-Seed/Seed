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
    public GameSystem.Mode mode;
    public Sprite circle_sprite;
    

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
        Throw();
    }
    private void Throw()
    {
        // 낙하지점 계산
        float throw_angle;
        throw_angle = tc.mouse_move.x;
        float throw_speed;
        throw_speed = 10.0f;
        s = new Vector3(windVec.x * wind_power, 0, ((2.0f * throw_speed * Mathf.Cos(throw_angle)) / gravity) + windVec.z * wind_power);
        throw_at = transform.forward + s;
        Debug.Log("throw at : " + throw_angle + " " + throw_at);

        // 해당 지점에 스프라이트 그리기
        Sprite ThrowAtSprite = Instantiate(circle_sprite, throw_at, Quaternion.AngleAxis(0f, Vector3.right));
        ThrowAtSprite.name = "Sprite";
        if (GameObject.Find("Sprite"))
            System.Console.WriteLine("Draw Sprite!");
    }

    private Vector3 Compute;

    public void ExitThrowMode()
    {
        //던지기모드 종료
        mode = GameSystem.Mode.ThrowMode;

    }

}
