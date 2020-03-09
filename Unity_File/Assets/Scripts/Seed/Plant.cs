using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Plant : Seed
{
    Vector3 position { get; set; }
    Quaternion rotation { get; set; }

    string type;
    public Plant(Vector3 pos, Quaternion rot)
    {
       //식물 모델 위치에 생성
    }
}
