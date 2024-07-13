using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;

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
    bool isUpGravity = false;
    public GameObject effects;
    /// <summary>
    /// ジャンプ可否フラグ
    /// </summary>
    bool isJump = false;

    bool isDeath = false;
    Vector3 beginScale = Vector3.one;
    Vector3 endScale = new Vector3(2f, 2f, 2f);
    float frame = 0.0f;
    float endFrame = 1.0f;
    [SerializeField] CinemachineVirtualCamera vCamera;
    float cameraBegin;
    float cameraEnd = 20f;
    float zoom = 0.0f;
    void Start()
    {
        cameraBegin = vCamera.m_Lens.FieldOfView;
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
        EffectUpdate();//死亡時のエフェクト
    }

    private void OnCollisionEnter(Collision collision)
    {


        if (!isUpGravity)
        {
            Vector3 normal = collision.contacts[0].normal; //法線をとってくる
            float angle = Vector3.Angle(normal, Vector3.up);
            if (angle < groundAngleLimit)
            {
                isJump = true;
            }
        }
        else
        {
            isJump = true;
        }
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
        if (!isUpGravity)
        {
            if (angle < groundAngleLimit)
            {
                isJump = true;
            }

        }
        else
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

        //重力が反転する
        if (other.tag == "Floating")
        {
            if (isUpGravity)
            {
                Physics.gravity = new Vector3(0, -9.8f, 0);//ゲーム中に重力を変更できる
                isUpGravity = false;
            }
            else
            {
                Physics.gravity = new Vector3(0, 9.8f, 0);//ゲーム中に重力を変更できる
                isUpGravity = true;
            }
        }

        //プレイヤーが消える
        if (other.tag == "Enemy")
        {
            isDeath = true;
        }

    }

    /// <summary>
    /// フレームの計測
    /// </summary>
    void Frame()
    {
        if (zoom < endFrame)
        {
            zoom += Time.deltaTime;
        }
        else if (frame < endFrame&&zoom>=endFrame)
        {
            frame += Time.deltaTime;
        }
    }

    /// <summary>
    /// 拡縮の線形補間
    /// </summary>
    void ScaleLarp()
    {
        if (isDeath)
        {
            Frame();

            vCamera.m_Lens.FieldOfView = Mathf.Lerp(cameraBegin, cameraEnd, EaseInQuint(zoom));
            gameObject.transform.localScale = Vector3.Lerp(beginScale, endScale, EaseInOutQuad(frame/(endFrame*2)));
        }
    }

    /// <summary>
    /// エフェクトを出す
    /// </summary>
    void EffectUpdate()
    {
        ScaleLarp();
        if (frame >= endFrame)
        {
            var effect = Instantiate(effects);
            effect.transform.position = gameObject.transform.position;
            gameObject.SetActive(false);
        }
    }

    public static float EaseInQuint(float x)
    {
        return x * x * x * x * x;
    }

    public static float EaseInOutQuad(float x)
    {
        return x < 0.5f ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2;
    }

}
