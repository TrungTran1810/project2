using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{



    float horizontal;
    float vertical;
    public float Speed;
    public Joystick joystick;
    private List<GameObject> Addbrick=new List<GameObject>();
    public GameObject AddbrickPrefab;
    public Transform player;

    void Update()
    {
        Moving();
    }
    private void Moving()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        Vector3 direction = new Vector3(horizontal, 0, vertical); // Horizontal and Vertical map to X and Z
        transform.Translate(direction * Time.deltaTime * Speed);

    }

    private void EatBrick()
    {
        Vector3 newPosition;
        if (Addbrick.Count == 0)
        {
            newPosition = player.position - player.up * 03f;
        }
        else
        {
            newPosition = Addbrick[Addbrick.Count-1].transform.position-player.right*0.3f;
        }

        GameObject newBrick = Instantiate(AddbrickPrefab, newPosition, Quaternion.identity);
        Addbrick.Add(newBrick);

        newBrick.transform.SetParent(transform);
        newBrick.transform.localPosition = new Vector3(0.7f, (+0.2f * Addbrick.Count ), 0);

    }



    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.tag == "Green")
        {
           
            EatBrick();
            Destroy(other.gameObject);
            //Invoke("EatBrick", 5f);
          

        }
        else if(other.gameObject.tag==null)
        {
            return;

        }
    }
   
}
