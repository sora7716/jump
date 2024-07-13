using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornManager : MonoBehaviour
{
    [SerializeField] Vector3 beginPosition;
    [SerializeField] Vector3 endPosition;
    float frame = 0.0f;
    float endFrame = 1.0f;

    void Start()
    {
        transform.position = beginPosition;
        gameObject.SetActive(false);
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            if (frame < endFrame)
            {
                frame += Time.deltaTime;
            }
            gameObject.transform.localPosition = Vector3.Lerp(beginPosition, endPosition, frame);
        }
    }

}
