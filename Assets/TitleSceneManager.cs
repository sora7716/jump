using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleSceneManager : MonoBehaviour
{
    [SerializeField] RectTransform titleFontRect;
    public Vector3 titleFontGoolPosition;
    public Vector3 beginPosition;
    float frame=0f;
    float endFrame = 1;
    Vector3 titleFontBeginPosition;
    [SerializeField] GameObject[] cubes;
    float cubeSpawnTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        foreach (var cube in cubes)
        {
            cube.SetActive(false);
        }
        titleFontRect=gameObject.GetComponent<RectTransform>();
        titleFontBeginPosition = beginPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if(frame< endFrame)
        {
            frame += Time.deltaTime;
        }
        titleFontRect.position = Vector3.Lerp(titleFontBeginPosition, titleFontGoolPosition, EaseOutElastic(frame));
        if (frame >= endFrame)
        {
            cubes[0].SetActive(true);
            if (cubeSpawnTime > 0)
            {
                cubeSpawnTime-= Time.deltaTime;
            }
            else
            {
                cubes[1].SetActive(true);
            }
        }
    }
    public static float EaseOutElastic(float x)
    {
        const float c4 = (2 * Mathf.PI) / 3;

        if (x == 0)
        {
            return 0;
        }
        if (x == 1)
        {
            return 1;
        }

        return Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * c4) + 1;
    }
}
