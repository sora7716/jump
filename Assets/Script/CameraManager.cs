using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;

public class CameraManager : MonoBehaviour
{
    [SerializeField] GameObject mainCamera;
    Vector3 velocity;
    bool isLeftRotate = false;
    bool isRightRotate = false;
    Vector3 speed = Vector3.zero;
    float begin = 0.0f;
    float end = 99.0f;
    float frame = 0.0f;
    float endFrame = 60.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        mainCamera.transform.position += velocity;
        if (Input.GetKey(KeyCode.W))
        {
            velocity.y = 0.01f;
        }
        else if (Input.GetKey(KeyCode.S))
        {
            velocity.y = -0.01f;
        }
        else
        {
            velocity = Vector3.zero;
        }


        //Ž‹“_ˆÚ“®
        if (Input.GetKeyDown(KeyCode.LeftArrow) && !isRightRotate && !isLeftRotate)
        {
            if (mainCamera.transform.eulerAngles.y < end)
            {
                frame = 0.0f;
            }
            isLeftRotate = true;
        }
        if (isLeftRotate)
        {
            begin = 0;
            end = 99;
            if (frame < endFrame)
            {
                frame += Time.deltaTime * 10;
            }
            else
            {
                isLeftRotate = false;
            }
            mainCamera.transform.eulerAngles = new Vector3(0, begin + (end - begin) * (frame / endFrame), 0);
        }


        if (Input.GetKeyDown(KeyCode.RightArrow) && !isLeftRotate && !isRightRotate)
        {
            if (mainCamera.transform.eulerAngles.y > begin)
            {
                frame = 0.0f;
            }
            isRightRotate = true;
        }
        if (isRightRotate)
        {
            begin = 99;
            end = 1;
            if (frame < endFrame)
            {
                frame += Time.deltaTime * 10;
            }
            else
            {
                isRightRotate = false;
            }
            mainCamera.transform.eulerAngles = new Vector3(0, begin + (end - begin) * (frame / endFrame), 0);

        }

        //player‚ÌŒü‚¢‚Ä‚¢‚éŽ‹“_
        //if (mainCamera.gameObject.transform.eulerAngles.y <= 99 && isRotate)
        //{
        //    mainCamera.transform.eulerAngles += speed;
        //}
        //if (mainCamera.gameObject.transform.eulerAngles.y >= 0 && !isRotate)
        //{
        //    mainCamera.transform.eulerAngles -= speed;
        //}
    }

}
