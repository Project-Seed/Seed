using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Item이 여러개 --> 공통적으로 가지고 있는 함수를 여기에 써두고 각각의 아이템들이 이것을 상속받는다.
// 상속받은 애들은 여기 있는 함수를 무조건 써야한다는 규칙이 있음.
public interface IItem
{
    void Collided();
}