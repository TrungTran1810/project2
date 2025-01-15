using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{



    float horizontal;
    float vertical;
    public float Speed;
    
    void Update()
    {
        Moving();
    }
    private void Moving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0, vertical); // Horizontal and Vertical map to X and Z
        transform.Translate(direction * Time.deltaTime*Speed);

    }

    private void CheckWall()
    {

    }
}
