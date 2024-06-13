using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PullingJump : MonoBehaviour
{
    [SerializeField] float groundAngleLimit = 30;
    [SerializeField] float jumpSpeed = 10;
    Rigidbody rb;
    Vector3 clickPosition;
    /// <summary>
    /// ジャンプ可否フラグ
    /// </summary>
    bool isJump = false;
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
        else if (isJump && Input.GetMouseButtonUp(0))
        {
            Vector3 dragVector = clickPosition - Input.mousePosition;
            float size = dragVector.magnitude; //ベクトルの長さを得る
            rb.velocity = dragVector.normalized * jumpSpeed;
        }
        //Physics.gravity = new Vector3(0, -9.8f, 0);ゲーム中に重力を変更できる
    }

    private void OnCollisionEnter(Collision collision)
    {

        //ぶつかったら色を変える
        //var obj=collision.gameObject;
        //var rend = obj.GetComponent<Renderer>();
        //var mat = rend.material;
        //mat.color=Color.yellow;
        //rend.material = mat;    

        Vector3 normal =  collision.contacts[0].normal; //法線をとってくる
        float angle = Vector3.Angle(normal, Vector3.up);
        if(angle < groundAngleLimit)
        {
        isJump = true;
        }
        Debug.Log(collision.gameObject.name + "にぶつかった");
    }

    private void OnCollisionExit(Collision collision)
    {
        isJump = false;
        Debug.Log(collision.gameObject.name + "と離れた");
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal; //法線をとってくる
        float angle = Vector3.Angle(normal, Vector3.up);
        if (angle < groundAngleLimit)
        {
            isJump = true;
        }
        //Debug.Log("くっついてる");
    }
}
