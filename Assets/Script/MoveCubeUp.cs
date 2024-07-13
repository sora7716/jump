using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeUp : MonoBehaviour
{
    Vector3 _initialPostion = new Vector3(0, 2, 0);
    [SerializeField] float _height = 3f;
    [SerializeField] float _speedY = 2f;

    void Start()
    {
        _initialPostion = transform.position;

    }

    void Update()
    {
        float y = _height * Mathf.Sin(Time.time * _speedY);
        transform.position = _initialPostion + Vector3.up * y;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(transform);
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.transform.SetParent(null);
        }
    }
}
