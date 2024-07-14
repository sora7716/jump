using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;
using UnityEngine.SceneManagement;
using System;
using Unity.VisualScripting;
using UnityEngine.UI;

public class PullingJump : MonoBehaviour
{
    [SerializeField] float groundAngleLimit = 30;
    [SerializeField] float jumpSpeed = 10;
    Rigidbody rb;
    Vector3 clickPosition;
    [SerializeField] GameObject cameraObject;
    Vector3 cameraBeginPosition;
    [SerializeField] Vector3 cameraGoolPosition;
    public float cameraMoveTime = 0f;
    float returnTime = 0f;
    public bool isVCamera = true;
    bool isObjectFreez = false;
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
    [SerializeField] GameObject[] thorns;
    [SerializeField] GameObject[] thorns2;
    [SerializeField] GameObject[] thorns3;
    [SerializeField] GameObject floatingEffect;
    public float changeDeathTime = 0f;
    bool isGool = false;
    float clearZoom = 0.0f;
    [SerializeField] GameObject[] invisibleWalls;
    [SerializeField] GameObject fedeImage;
    UnityEngine.UI.Image fadeImageComponent;
    Color fadeImageColorBegin;
    Color fadeImageColorEnd;
    float colorChangeTime;
    [SerializeField] GameObject arrowImage;
    void Start()
    {
        Physics.gravity = new Vector3(0f, -9.8f, 0f);
        cameraBegin = vCamera.m_Lens.FieldOfView;
        rb = GetComponent<Rigidbody>();
        initializeScale = transform.localScale;
        // imagesの各要素を非アクティブにする
        foreach (var image in images)
        {
            image.SetActive(false);
        }
        foreach (var invisibleWall in invisibleWalls)
        {
            invisibleWall.SetActive(false);
        }
        fadeImageComponent = fedeImage.GetComponent<UnityEngine.UI.Image>();
        fadeImageComponent.color = new Color(0f, 0f, 0f, 0f);
        fadeImageColorBegin = fadeImageComponent.color;
        fadeImageColorEnd = new Color(0f, 0f, 0f, 1f);
    }

    void Update()
    {
        transform.localScale = initializeScale;
        if (!isDeath || !isGool)
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
        }

        ItemCounterManager itemCounterManager = images[imageNumber].GetComponent<ItemCounterManager>();//スクリプトを持ってくる
        itemCounterManager.Alive();//itemCounterの動き
        ChangeField();//地形を変える棘を出したり重力反転エリアを出現させたり

        EffectUpdate();//死亡時のエフェクト
        if (isDeath)
        {
            changeDeathTime += Time.deltaTime;
            if (changeDeathTime > 5)
            {
                SceneManager.LoadScene("GameOver");
            }
        }

        if (isGool)
        {
            GameClear();
            rb.velocity = Vector3.zero;
            Physics.gravity = new Vector3(0f, 0f, 0f);
            arrowImage.SetActive(false);
        }

        ObjectFreez();
        if (!isVCamera)
        {
            CameraMove();
        }
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

        //プレイヤーが消える
        if (collision.gameObject.tag == "Enemy")
        {
            GameObject obj = gameObject;
            Renderer rend = obj.GetComponent<Renderer>();
            Material mat = rend.material;
            mat.color = Color.red;
            rend.material = mat;
            isDeath = true;
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
        Item itemScript = GetComponent<Item>();
        if (other.tag == "Item")
        {

            itemCounter++; // itemを拾った数を増やす
            imageNumber = itemCounter - 1; // imageのナンバーを検出

            // imageNumberが配列の範囲内であることを確認
            if (imageNumber >= 0 && imageNumber < images.Length)
            {
                images[imageNumber].SetActive(true); // 指定したimageを表示する 
            }
            other.enabled = false;
            if (itemCounter == 1)
            {
                cameraBeginPosition = cameraObject.transform.position;
                isObjectFreez = true;
                isVCamera = false;
                vCamera.gameObject.SetActive(isVCamera);
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
        if (other.tag == "Gool")
        {
            isGool = true;
            foreach (var invisibleWall in invisibleWalls)
            {
                invisibleWall.SetActive(true);
            }
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
        else if (frame < endFrame && zoom >= endFrame)
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
            gameObject.transform.localScale = Vector3.Lerp(beginScale, endScale, EaseInOutQuad(frame / (endFrame * 2)));
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
            Renderer renderer = gameObject.GetComponent<Renderer>();
            if (renderer.enabled)
            {
                var effect = Instantiate(effects);
                effect.transform.position = gameObject.transform.position;
            }
            renderer.enabled = false;
        }
    }

    void ChangeField()
    {
        if (itemCounter == 1)
        {
            foreach (var thorn in thorns)
            {
                thorn.SetActive(true);
            }
        }
        if (itemCounter == 2)
        {
            foreach (var thorn in thorns2)
            {
                thorn.SetActive(true);
            }
        }
        if (itemCounter == 3)
        {
            foreach (var thorn in thorns3)
            {
                thorn.SetActive(true);
            }
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

    public static float EaseOutQuint(float x)
    {
        return 1 - Mathf.Pow(1 - x, 5);
    }

    void ClearZoom()
    {
        if (clearZoom < endFrame)
        {
            clearZoom += Time.deltaTime;
        }
        else
        {
            if (colorChangeTime < endFrame)
            {
                colorChangeTime += Time.deltaTime;
            }
            else
            {
                SceneManager.LoadScene("GameClear");
            }
        }
    }

    void GameClear()
    {
        ClearZoom();
        vCamera.m_Lens.FieldOfView = Mathf.Lerp(cameraBegin, cameraEnd, EaseInQuint(clearZoom));
        fadeImageComponent.color = Vector4.Lerp(fadeImageColorBegin, fadeImageColorEnd, EaseOutQuint(colorChangeTime));
    }

    void ObjectFreez()
    {
        if (isObjectFreez)
        {
            rb.velocity = Vector3.zero;
            Physics.gravity = new Vector3(0f, 0f, 0f);
            arrowImage.SetActive(false);
        }
    }
    void CameraMoveTime()
    {
        if (cameraMoveTime < (endFrame * 3f))
        {
            cameraMoveTime += Time.deltaTime;
        }
        else
        {
            floatingEffect.SetActive(true);
            if (returnTime < (endFrame * 3f))
            {
                returnTime += Time.deltaTime;
            }
            else
            {
                isVCamera = true;
                vCamera.gameObject.SetActive(isVCamera);
                isObjectFreez = false;
                Physics.gravity = new Vector3(0f, -9.8f, 0f);

            }
        }

    }
    void CameraMove()
    {
        CameraMoveTime();
        if (!isVCamera)
        {
            cameraObject.transform.localPosition = Vector3.Lerp(cameraBeginPosition, cameraGoolPosition, EaseInQuint(cameraMoveTime));
        }

    }
}
