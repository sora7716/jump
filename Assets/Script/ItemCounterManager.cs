using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ItemCounterManager : MonoBehaviour
{
    Vector3 beginScale = Vector3.zero;
    Vector3 endScale = Vector3.one;
    float frame = 0;
    float endFrame = 1;
    RectTransform rectTransform;
    // Start is called before the first frame update
    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        rectTransform.localScale = Vector3.one;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void FrameAdd()
    {
        if (frame < endFrame)
        {
            frame += Time.deltaTime;
        }
    }

    //表示するときの動き
    public void Alive()
    {
        if (!gameObject.activeSelf)
        {
            return;
        }
        FrameAdd();
        rectTransform.localScale = Vector3.Lerp(beginScale, endScale, EaseOutBounce(frame / endFrame));
    }

    //rectTransformのゲッター
    public RectTransform GetRectTransform()
    {
        return rectTransform;
    }

    // easeOutElastic 関数をC#に変換
    float EaseOutElastic(float x)
    {
        const float c4 = (2 * Mathf.PI) / 3;

        if (x == 0)
        {
            return 0;
        }
        else if (x == 1)
        {
            return 1;
        }
        else
        {
            return Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
        }
    }

    // easeOutBounce 関数をC#に変換
    float EaseOutBounce(float x)
    {
        float n1 = 7.5625f;
        float d1 = 2.75f;

        if (x < 1 / d1)
        {
            return n1 * x * x;
        }
        else if (x < 2 / d1)
        {
            x -= 1.5f / d1;
            return n1 * x * x + 0.75f;
        }
        else if (x < 2.5f / d1)
        {
            x -= 2.25f / d1;
            return n1 * x * x + 0.9375f;
        }
        else
        {
            x -= 2.625f / d1;
            return n1 * x * x + 0.984375f;
        }
    }
}
