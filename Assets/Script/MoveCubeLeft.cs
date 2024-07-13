using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCubeLeft : MonoBehaviour
{
    Vector3 _initialPostion = new Vector3(0, 2, 0);
    [SerializeField] float _width = 3f;
    [SerializeField] float _speedX = 2f;

    void Start()
    {
        _initialPostion = transform.position;

    }

    void Update()
    {
        float x = _width * Mathf.Sin(Time.time * _speedX);
        transform.position = _initialPostion + Vector3.right * x;
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
