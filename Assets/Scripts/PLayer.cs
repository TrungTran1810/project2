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
    public Material mas;

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

        Vector3 direction = new Vector3(-horizontal, 0, -vertical); 
        transform.Translate(direction * Time.deltaTime * Speed);

    }

    private void EatBrick()
    {
        Vector3 newPosition;
        if (Addbrick.Count == 0)
        {
            newPosition = player.position - player.up *0.3f;
        }
        else
        {                         //lay vien gach cuôi 
            newPosition = Addbrick[Addbrick.Count-1].transform.position-player.right*0.3f;
        }

        GameObject newBrick = Instantiate(AddbrickPrefab, newPosition, Quaternion.identity);
        Addbrick.Add(newBrick);

        newBrick.transform.SetParent(transform);
        newBrick.transform.localPosition = new Vector3(0.7f, (+0.6f * Addbrick.Count ), 0);
        newBrick.transform.localScale = new Vector3(1f, 0.5f, 1f);
    }

    private void LostBrick()
    {
        if (Addbrick.Count > 0)
        {
            GameObject lostBrick = Addbrick[Addbrick.Count - 1]; // Lấy viên gạch cuối cùng
            Addbrick.RemoveAt(Addbrick.Count - 1); // Xóa khỏi danh sách
            Destroy(lostBrick, 0.1f);

        }
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
        if (other.gameObject.tag == "MyBrick")
        {

            other.GetComponent<MeshRenderer>().material = mas;
           


        }
        

    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "MyBrick")
        {
            Debug.Log("Chạm vào cầu, mất gạch!");
            LostBrick();
        }
    }




}
