using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PullingJump : MonoBehaviour
{
    [SerializeField] float groundAngleLimit = 30;
    [SerializeField] float jumpSpeed = 10;
    Rigidbody rb;
    Vector3 clickPosition;
    [SerializeField] GameObject cameraObject;
    private Vector3 initializeScale;
    int itemCounter = 0;
    [SerializeField] GameObject[] images;
    int imageNumber;
    /// <summary>
    /// ジャンプ可否フラグ
    /// </summary>
    bool isJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        initializeScale = transform.localScale;
        // imagesの各要素を非アクティブにする
        foreach (var image in images)
        {
            image.SetActive(false);
        }
    }

    void Update()
    {
        transform.localScale = initializeScale;
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
            float x = dragVector.normalized.x * jumpSpeed;
            float y = dragVector.normalized.y * jumpSpeed;
            float z = dragVector.normalized.z * jumpSpeed;
            if (cameraObject.transform.eulerAngles.y >= 99)
            {
                jumpSpeed = 5;
                rb.velocity = new Vector3(y, y, -x);
            }
            else
            {
                jumpSpeed = 10;
                rb.velocity = new Vector3(x, y, z);
            }


        }

        ItemCounterManager itemCounterManager = images[imageNumber].GetComponent<ItemCounterManager>();//スクリプトを持ってくる
        itemCounterManager.Alive();//itemCounterの動き
       // Physics.gravity = new Vector3(0, 9.8f, 0);//ゲーム中に重力を変更できる
    }

    private void OnCollisionEnter(Collision collision)
    {

        Vector3 normal = collision.contacts[0].normal; //法線をとってくる
        float angle = Vector3.Angle(normal, Vector3.up);
        if (angle < groundAngleLimit)
        {
            isJump = true;
        }
        //  Debug.Log(collision.gameObject.name + "にぶつかった");
    }

    private void OnCollisionExit(Collision collision)
    {
        isJump = false;
        // Debug.Log(collision.gameObject.name + "と離れた");
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Item")
        {
           
            itemCounter++; // itemを拾った数を増やす
            imageNumber = itemCounter - 1; // imageのナンバーを検出

            // imageNumberが配列の範囲内であることを確認
            if (imageNumber >= 0 && imageNumber < images.Length)
            {
                images[imageNumber].SetActive(true); // 指定したimageを表示する 
            }
        }

        if (other.tag == "Floating")
        {
            Physics.gravity = new Vector3(0, 9.8f, 0);//ゲーム中に重力を変更できる
        }
    }
}
