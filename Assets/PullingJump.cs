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
        //�h���b�N�J�n�����o
        if (Input.GetMouseButtonDown(0))
        {
            clickPosition = Input.mousePosition;//�}�E�X�̍��W����                              

        }
        //�h���b�N���ɏ���
        else if (Input.GetMouseButtonUp(0))
        {
            Vector3 dragVector = clickPosition - Input.mousePosition;
            float size = dragVector.magnitude; //�x�N�g���̒����𓾂�
            rb.velocity = dragVector.normalized * jumpSpeed;
        }
        //Physics.gravity = new Vector3(0, -9.8f, 0);�Q�[�����ɏd�͂�ύX�ł���
    }
}
