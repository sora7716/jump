using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakWallManager : MonoBehaviour
{
    GameObject obj;
    Renderer rend;
    Material mat;
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {

    }

    //Ç‘Ç¬Ç©Ç¡ÇΩÇÁêFÇïœÇ¶ÇÈ
    private void OnCollisionEnter(Collision collision)
    {
        obj = gameObject;
        rend = obj.GetComponent<Renderer>();
        mat = rend.material;
        if (mat.color == Color.white)
        {
            mat.color = Color.yellow;
            rend.material = mat;
        }
        else if (mat.color == Color.yellow)
        {
            mat.color = Color.red;
            rend.material = mat;
        }
        else
        {
            gameObject.SetActive(false);
        }


    }
}
