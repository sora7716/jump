using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowDraw : MonoBehaviour
{
    /// <summary> 矢印の画像 </summary>
    [SerializeField] Image arrowImage;
    void Start()
    {
        arrowImage.gameObject.SetActive(false);//矢印を非表示
    }
    /// <summary> ドラック開始座標 </summary>
    private Vector3 clickPosition;
    void Update()
    {
        //ドラック開始を検出
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;//マウスの座標を代入                              
            arrowImage.rectTransform.position = clickPosition; //矢印の画像をドラック開始位置に移動する
            arrowImage.gameObject.SetActive(true);//矢印の画像を表示

        }
        //ドラック中に処理
        else if (Input.GetMouseButton(0))
        {
            Vector3 dragVector = clickPosition - Input.mousePosition;
            float size = dragVector.magnitude; //ベクトルの長さを得る
            arrowImage.rectTransform.right = dragVector; //矢印をベクトルの方向に向ける
            arrowImage.rectTransform.sizeDelta = Vector2.one * size;//ドラックをたくさんしたら画像を大きく表示する
        }
        //ドラック終了時
        else if (Input.GetMouseButtonUp(0))
        {
            arrowImage.gameObject.SetActive(false);//矢印を非表示
            arrowImage.rectTransform.sizeDelta = Vector2.zero;//サイズを0にする
        }
    }
}
