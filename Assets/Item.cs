using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{
    Animator _anim;

    void Start()
    {
        _anim = GetComponent<Animator>();
        _anim.SetBool("IsGet", false);
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.V))
        //{
        //    _anim.Play("Idle");
        //}
        //if (Input.GetKeyDown(KeyCode.C))
        //{

        //}
    }

    /// <summary>
    /// �l���A�j���[�V�����̍Đ����I�������Ă�
    /// �I�u�W�F�N�g��j������
    /// </summary>
    void OnGetAnimationFinished()
    {
        Debug.Log("�A�C�e����j�����܂�");
        Destroy(this.gameObject);
    }
    private void OnTriggerEnter(Collider other)
    {
        //_anim.Play("Get");
        _anim.SetBool("IsGet", true);
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.drag = 15;
        //Destroy(this.gameObject);
        Debug.Log(other.gameObject.name + "���ڐG����");
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.drag = 0;
        Debug.Log(other.gameObject.name + "�����ꂽ");
    }
}
