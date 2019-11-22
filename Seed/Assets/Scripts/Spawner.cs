using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public List<GameObject> item;
    [SerializeField] private float range_min;        // float. 스폰범위 최소값
    [SerializeField] private float range_max;        // float. 스폰범위 최대값
    private bool spawn_item;            // 아이템 스폰 여부(게임이 시작되면 true)

    // 게임이 시작되면 Spawn_Item()호출

    private void Awake()
    {
        spawn_item = true;
        if (spawn_item)
            Spawn_Item();
        range_min = -49.0f;
        range_max = 49.0f;
    }
    
    private void Spawn_Item()
    {
        if (!spawn_item)
            return;
        Debug.Log("Spawn Item!");

        // 아이템 복제 생성
        for (int i = 0; i < 10; ++i)
        {
            float random_x = Random.Range(range_min, range_max);
            float random_z = Random.Range(range_min, range_max);

            int choose = Random.Range(0, 2);
            GameObject items = Instantiate(item[choose], new Vector3(random_x, 0.5f, random_z), Quaternion.identity, transform); // ItemSpawner 밑 자식으로 복제
            items.name = item[choose].name;
            Debug.Log("Spawn : " + random_x + ", " + random_z);
        }
    }
}
