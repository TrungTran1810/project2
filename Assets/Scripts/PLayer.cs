using System.Collections.Generic;
using UnityEngine;

public class PLayer : MonoBehaviour
{



    float horizontal;
    float vertical;
    public float Speed;
    public Joystick joystick;
    protected List<GameObject> Addbrick = new List<GameObject>();
    public GameObject AddbrickPrefab;
    public Transform player;
    protected Vector3 Player;
    public Material mas;
    private LayerMask BridgeLayer;
    protected HashSet<GameObject> passedBricks = new HashSet<GameObject>(); // Lưu gạch đã đi qua
    public GameObject Wall;
  
    
    void Start()
    {
        
    }
    void Update()
    {
        Moving();
        //Blockbrick();

    }

    private void Moving()

    {

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        horizontal = joystick.Horizontal;
        vertical = joystick.Vertical;

        Vector3 direction = new Vector3(-horizontal, 0, -vertical);
        //Vector3 checkposition = player.position + direction * 0.6f;

        transform.Translate(direction * Time.deltaTime * Speed);

        //if (BlockPlayer(checkposition))
        //{
        //    transform.Translate(direction * Time.deltaTime * Speed);
        //}
    }


    //protected void EatBrick()
    //{
    //    Vector3 newPosition;
    //    if (Addbrick.Count == 0)
    //    {
    //        newPosition = player.position - player.up * 0.3f;
    //    }
    //    else
    //    {                         //lay vien gach cuôi 
    //        newPosition = Addbrick[Addbrick.Count - 1].transform.position - player.right * 0.3f;
    //    }

    //    GameObject newBrick = Instantiate(AddbrickPrefab, newPosition, Quaternion.identity);
    //    Addbrick.Add(newBrick);

    //    newBrick.transform.SetParent(transform);
    //    newBrick.transform.localPosition = new Vector3(0.7f, (+0.6f * Addbrick.Count), 0);
    //    newBrick.transform.localScale = new Vector3(1f, 0.5f, 1f);


    //}
    protected virtual void EatBrick()
    {
        if (Addbrick.Count >= 5)
            return; // Nếu đã có 5 viên thì không ăn nữa

        Vector3 newPosition;
        if (Addbrick.Count == 0)
        {
            newPosition = player.position - player.up * 0.3f;
        }
        else
        {
            newPosition = Addbrick[Addbrick.Count - 1].transform.position - player.right * 0.3f;
        }

        GameObject newBrick = Instantiate(AddbrickPrefab, newPosition, Quaternion.identity);
        Addbrick.Add(newBrick);

        newBrick.transform.SetParent(transform);
        newBrick.transform.localPosition = new Vector3(0.7f, (+0.6f * Addbrick.Count), 0);
        newBrick.transform.localScale = new Vector3(1f, 0.5f, 1f);
    }



    protected void LostBrick( GameObject brickObject)
    {
        Player = player.position;
        if (Addbrick.Count > 0 && !passedBricks.Contains(brickObject))
        {

            Ray ray = new Ray(Player, Vector3.down);
            //Debug.DrawRay(Player, Vector3.down, Color.red, 10f);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
            {
               
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
            }
        }

    }

 
    protected void Block(GameObject Wall)
    {
        BoxCollider block=Wall.GetComponent<BoxCollider>();
        if(block != null)
        {
            block.enabled = false;
        }
    }
     protected void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            if (Addbrick.Count > 0)
            {
                Block(collision.gameObject);
            }
        }

    }
    protected virtual void OnTriggerEnter(Collider other)
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
            if (Addbrick.Count > 0)
            {
                other.GetComponent<MeshRenderer>().material = mas;
                LostBrick(other.gameObject);
            }

        }

    }
  
}





















