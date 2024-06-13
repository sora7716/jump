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
    /// �W�����v�ۃt���O
    /// </summary>
    bool isJump = false;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        //�h���b�N�J�n�����o
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;//�}�E�X�̍��W����                              

        }
        //�h���b�N���ɏ���
        else if (isJump && Input.GetMouseButtonUp(0))
        {
            Vector3 dragVector = clickPosition - Input.mousePosition;
            float size = dragVector.magnitude; //�x�N�g���̒����𓾂�
            rb.velocity = dragVector.normalized * jumpSpeed;
        }
        //Physics.gravity = new Vector3(0, -9.8f, 0);�Q�[�����ɏd�͂�ύX�ł���
    }

    private void OnCollisionEnter(Collision collision)
    {

        //�Ԃ�������F��ς���
        //var obj=collision.gameObject;
        //var rend = obj.GetComponent<Renderer>();
        //var mat = rend.material;
        //mat.color=Color.yellow;
        //rend.material = mat;    

        Vector3 normal =  collision.contacts[0].normal; //�@�����Ƃ��Ă���
        float angle = Vector3.Angle(normal, Vector3.up);
        if(angle < groundAngleLimit)
        {
        isJump = true;
        }
        Debug.Log(collision.gameObject.name + "�ɂԂ�����");
    }

    private void OnCollisionExit(Collision collision)
    {
        isJump = false;
        Debug.Log(collision.gameObject.name + "�Ɨ��ꂽ");
    }

    private void OnCollisionStay(Collision collision)
    {
        Vector3 normal = collision.contacts[0].normal; //�@�����Ƃ��Ă���
        float angle = Vector3.Angle(normal, Vector3.up);
        if (angle < groundAngleLimit)
        {
            isJump = true;
        }
        //Debug.Log("�������Ă�");
    }
}
