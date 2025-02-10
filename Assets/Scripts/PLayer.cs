using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{



    float horizontal;
    float vertical;
    public float Speed;
    public Joystick joystick;
    private List<GameObject> Addbrick = new List<GameObject>();
    public GameObject AddbrickPrefab;
    public Transform player;
    private Vector3 Player;
    public Material mas;
    [SerializeField] private GameObject Wall;
    private HashSet<GameObject> passedBricks = new HashSet<GameObject>(); // Lưu gạch đã đi qua
    [SerializeField] private LayerMask groundLayer;
    bool isGrounded;
    void Update()
    {
        Moving();

    }
    //private void FixedUpdate()
    //{
    //    CheckGround();
    //}
    private void Moving()
    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        Vector3 direction = new Vector3(-horizontal, 0, -vertical);
        transform.Translate(direction * Time.deltaTime * Speed);

    }
    private void CheckGround()
    {
        Ray ray = new Ray(transform.position, Vector3.down);
        Debug.DrawRay(Player, Vector3.down, Color.red, 10f);
        if (Physics.Raycast(ray, out RaycastHit hit, 1f, groundLayer))
        {
            Debug.Log("on Ground");
            isGrounded = true;
        }

    }


    private void EatBrick()
    {
        Vector3 newPosition;
        if (Addbrick.Count == 0)
        {
            newPosition = player.position - player.up * 0.3f;
        }
        else
        {                         //lay vien gach cuôi 
            newPosition = Addbrick[Addbrick.Count - 1].transform.position - player.right * 0.3f;
        }

        GameObject newBrick = Instantiate(AddbrickPrefab, newPosition, Quaternion.identity);
        Addbrick.Add(newBrick);

        newBrick.transform.SetParent(transform);
        newBrick.transform.localPosition = new Vector3(0.7f, (+0.6f * Addbrick.Count), 0);
        newBrick.transform.localScale = new Vector3(1f, 0.5f, 1f);

        if (Addbrick.Count > 0)
        {
            // Debug.Log("Có gạch, mở đường!");
            Wall.SetActive(false);
        }
    }

    private void LostBrick(GameObject brickObject)
    {
        Player = player.position;
        if (Addbrick.Count > 0 && !passedBricks.Contains(brickObject))
        {

            Ray ray = new Ray(Player, Vector3.down);
            Debug.DrawRay(Player, Vector3.down, Color.red, 10f);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
                // Debug.Log("Raycast trúng: " + hit.transform.name);
                Brick brick = hit.transform.GetComponent<Brick>();
                if (brick == null)
                {
                    return;
                }
                if (brick.GetData() == BrickType.Lostbrick)
                {
                    GameObject lastBrick = Addbrick[Addbrick.Count - 1];
                    Addbrick.RemoveAt(Addbrick.Count - 1);
                    Destroy(lastBrick);
                }
                passedBricks.Add(brickObject);

                // Nếu không còn gạch, chặn di chuyển
                if (Addbrick.Count == 0)
                {
                    //Debug.Log("Hết gạch, chặn Player!");
                    Wall.SetActive(true);
                }
            }
        }
    }
    //private void LostBrick()
    //{
    //    Player = player.position;

    //    if (Addbrick.Count > 0)
    //    {
    //        Ray ray = new Ray(Player, Vector3.down);
    //        Debug.DrawRay(Player, Vector3.down, Color.red, 10f);

    //        if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
    //        {
    //            Brick brick = hit.transform.GetComponent<Brick>();

    //            // Nếu không tìm thấy Brick hoặc không phải Lostbrick, không làm gì cả
    //            if (brick == null || brick.GetData() != BrickType.Lostbrick)
    //            {
    //                return;
    //            }

    //            // Nếu có gạch, trừ 1 viên
    //            if (Addbrick.Count > 0)
    //            {
    //                GameObject lastBrick = Addbrick[Addbrick.Count - 1];
    //                Addbrick.RemoveAt(Addbrick.Count - 1);
    //                Destroy(lastBrick);
    //                Debug.Log("Mất 1 viên gạch!");
    //            }

    //            // Nếu không còn gạch, chặn di chuyển
    //            if (Addbrick.Count == 0)
    //            {
    //                Debug.Log("Hết gạch, chặn Player!");
    //                Wall.SetActive(true);
    //            }
    //        }
    //    }


    //}





    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Green")
        {

            EatBrick();
            Destroy(other.gameObject);

            //Invoke("EatBrick", 5f);


        }

        else if (other.gameObject.tag == null)
        {
            return;

        }
        if (other.gameObject.tag == "MyBrick")
        {

            other.GetComponent<MeshRenderer>().material = mas;
            LostBrick(other.gameObject);


        }


    }


    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            Debug.Log("cham ground");
            isGrounded = true;
        }
    }





}

