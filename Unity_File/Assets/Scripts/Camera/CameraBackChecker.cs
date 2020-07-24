using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBackChecker : MonoBehaviour
{
    //Vector3 camera_offset;
    //Vector3 origin_camera_offset;
    //public Transform head;
    //private void Awake()
    //{
    //    camera_offset = transform.localPosition - head.transform.localPosition;
    //    origin_camera_offset = camera_offset;
    //}
    //public IEnumerator CameraShake(float duration, float mag)
    //{
    //    float time = 0.0f;
    //    while (time < duration)
    //    {
    //        //float z = Random.Range(-1f, 1f) * mag;

    //        camera_offset *= 1.1f;
    //        time += Time.deltaTime;

    //        yield return null;
    //    }
    //    camera_offset = origin_camera_offset;
    //}
    bool isBackClear;
    private void OnTriggerEnter(Collider other)
    {
        isBackClear = false;
    }

    private void OnTriggerExit(Collider other)
    {
        isBackClear = true;
    }

}
