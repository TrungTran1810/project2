using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer1 : MonoBehaviour
{



    float horizontal;
    float vertical;
    [SerializeField] float Speed;
    void Update()
    {
       
    }
    private void Moving()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        transform.Translate(horizontal * Speed, 0, 0);
        vertical = Input.GetAxisRaw("Vertical");
        transform.Translate(0, 0, vertical * Speed);

       
    }

    private void CheckWall()
    {

    }
}
