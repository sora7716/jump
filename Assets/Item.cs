using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Item : MonoBehaviour
{

    void Start()
    {
      
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.drag = 15;
       // Destroy(this.gameObject);
        Debug.Log(other.gameObject.name + "Ç™ê⁄êGÇµÇΩ");
    }

    private void OnTriggerExit(Collider other)
    {
        Rigidbody rb = other.GetComponent<Rigidbody>();
        rb.drag = 0;
        Debug.Log(other.gameObject.name + "Ç™ó£ÇÍÇΩ");
    }
}
