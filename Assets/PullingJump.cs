using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PullingJump : MonoBehaviour
{
    [SerializeField] float jumpSpeed = 10;
    Rigidbody rb;
    Vector3 clickPosition;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //ドラック開始を検出
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;//マウスの座標を代入                              

        }
        //ドラック中に処理
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 dragVector = clickPosition - Input.mousePosition;
            float size = dragVector.magnitude; //ベクトルの長さを得る
            rb.velocity = dragVector.normalized * jumpSpeed;
        }
        //Physics.gravity = new Vector3(0, -9.8f, 0);ゲーム中に重力を変更できる
    }
}
