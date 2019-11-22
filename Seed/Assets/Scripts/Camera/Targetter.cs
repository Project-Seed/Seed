using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetter : MonoBehaviour
{
    // 타겟은 캐릭터의 transform + 10 (z)
    // 회전은 카메라 회전과 반대.

    public Transform target;                                    // 카메라가 따라갈 대상 캐릭터+10
    public Transform character;

    // Start is called before the first frame update

    void Start()
    {
        target = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        target.position = character.position + Vector3.forward * 10;
    }
}
