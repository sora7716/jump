using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ArrowDraw : MonoBehaviour
{
    /// <summary> ���̉摜 </summary>
    [SerializeField] Image arrowImage;
    void Start()
    {
        arrowImage.gameObject.SetActive(false);//�����\��
    }
    /// <summary> �h���b�N�J�n���W </summary>
    private Vector3 clickPosition;
    void Update()
    {
        //�h���b�N�J�n�����o
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;//�}�E�X�̍��W����                              
            arrowImage.rectTransform.position = clickPosition; //���̉摜���h���b�N�J�n�ʒu�Ɉړ�����
            arrowImage.gameObject.SetActive(true);//���̉摜��\��

        }
        //�h���b�N���ɏ���
        else if (Input.GetMouseButton(0))
        {
            Vector3 dragVector = clickPosition - Input.mousePosition;
            float size = dragVector.magnitude; //�x�N�g���̒����𓾂�
            arrowImage.rectTransform.right = dragVector; //�����x�N�g���̕����Ɍ�����
            arrowImage.rectTransform.sizeDelta = Vector2.one * size;//�h���b�N���������񂵂���摜��傫���\������
        }
        //�h���b�N�I����
        else if (Input.GetMouseButtonUp(0))
        {
            arrowImage.gameObject.SetActive(false);//�����\��
            arrowImage.rectTransform.sizeDelta = Vector2.zero;//�T�C�Y��0�ɂ���
        }
    }
}
