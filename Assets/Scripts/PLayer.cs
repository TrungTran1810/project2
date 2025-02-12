using System.Collections.Generic;
using UnityEngine;

//public class PLayer : MonoBehaviour
//{



//    float horizontal;
//    float vertical;
//    public float Speed;
//    public Joystick joystick;
//    private List<GameObject> Addbrick = new List<GameObject>();
//    public GameObject AddbrickPrefab;
//    public Transform player;
//    private Vector3 Player;
//    public Material mas;
//    [SerializeField] private GameObject Wall;
//    private HashSet<GameObject> passedBricks = new HashSet<GameObject>(); // Lưu gạch đã đi qua

//    private bool isBlockedForward = false;
//    private bool isOnBridge = false; // Kiểm tra có đang trên cầu không
//    //void Start()
//    //{
//    //    if (Wall != null)
//    //    {
//    //        Wall.SetActive(false); // Ẩn vật cản ban đầu7
//    //    }
//    //}
//    void Update()
//    {
//        Moving();


//    }

//    private void Moving()

//    {

//        float horizontal = Input.GetAxisRaw("Horizontal");
//        float vertical = Input.GetAxisRaw("Vertical");

//        horizontal = joystick.Horizontal;
//        vertical = joystick.Vertical;

//        Vector3 direction = new Vector3(-horizontal, 0, -vertical);
//        //if (isBlockedForward && direction.z < 0)
//        //{
//        //    Debug.Log("Không thể tiến lên vì bị chặn!");
//        //    return;
//        //}
//        transform.Translate(direction * Time.deltaTime * Speed);


//    }





//    private void EatBrick()
//    {
//        Vector3 newPosition;
//        if (Addbrick.Count == 0)
//        {
//            newPosition = player.position - player.up * 0.3f;
//        }
//        else
//        {                         //lay vien gach cuôi 
//            newPosition = Addbrick[Addbrick.Count - 1].transform.position - player.right * 0.3f;
//        }

//        GameObject newBrick = Instantiate(AddbrickPrefab, newPosition, Quaternion.identity);
//        Addbrick.Add(newBrick);

//        newBrick.transform.SetParent(transform);
//        newBrick.transform.localPosition = new Vector3(0.7f, (+0.6f * Addbrick.Count), 0);
//        newBrick.transform.localScale = new Vector3(1f, 0.5f, 1f);


//    }

//    private void LostBrick(GameObject brickObject)
//    {
//        Player = player.position;
//        if (Addbrick.Count > 0 && !passedBricks.Contains(brickObject))
//        {

//            Ray ray = new Ray(Player, Vector3.down);
//            //Debug.DrawRay(Player, Vector3.down, Color.red, 10f);
//            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity))
//            {
//                // Debug.Log("Raycast trúng: " + hit.transform.name);
//                Brick brick = hit.transform.GetComponent<Brick>();
//                if (brick == null)
//                {
//                    return;
//                }
//                if (brick.GetData() == BrickType.Lostbrick)
//                {
//                    GameObject lastBrick = Addbrick[Addbrick.Count - 1];
//                    Addbrick.RemoveAt(Addbrick.Count - 1);
//                    Destroy(lastBrick);
//                }
//                passedBricks.Add(brickObject);
//            }
//        }

//    }

//    private void OnTriggerEnter(Collider other)
//    {

//        if (other.gameObject.tag == "Green")
//        {

//            EatBrick();
//            Destroy(other.gameObject);

//            //Invoke("EatBrick", 5f);


//        }

//        else if (other.gameObject.tag == null)
//        {
//            return;

//        }


//        if (other.gameObject.tag == "MyBrick")
//        {

//            other.GetComponent<MeshRenderer>().material = mas;
//            LostBrick(other.gameObject);


//        }

//    }






//    private void OnCollisionEnter(Collision collision)
//    {

//        if (collision.gameObject.CompareTag("Bridge"))
//        {

//            CheckForwardStep();
//        }
//    }

//    private void CheckForwardStep()
//    {
//        // Bắn Raycast thẳng về phía trước, từ vị trí nhân vật, cao hơn một chút để tránh va chạm mặt đất
//        Vector3 rayStart = transform.position + Vector3.up * 0.5f; // Nâng ray lên để tránh quét mặt đất
//        Vector3 rayDirection = -transform.forward; // Raycast hướng về phía trước

//        Debug.DrawRay(rayStart, rayDirection * 1f, Color.yellow, 2f); // Vẽ ray kiểm tra

//        if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hit, 1f))
//        {
//            Debug.Log("Raycast trúng: " + hit.collider.name);
//            if (hit.collider.CompareTag("MyBrick") && Addbrick.Count == 0)
//            {
//                isBlockedForward = false;
//                if (Wall != null) Wall.SetActive(true);
//                Debug.Log("Chặn đường vì hết gạch và phía trước là MyBrick!");
//            }

//        }
//    }





//}


public class Player : MonoBehaviour
{
    public float Speed;
    public Joystick joystick;
    public GameObject AddbrickPrefab;
    public Transform player;
    public Material mas; // Material đổi màu
    [SerializeField] private GameObject Wall; // Vật cản
    private List<GameObject> Addbrick = new List<GameObject>(); // Danh sách gạch đã nhặt
    private HashSet<GameObject> passedBricks = new HashSet<GameObject>(); // Ghi nhớ gạch đã đi qua

    private bool isBlockedForward = false;
    private bool isOnBridge = false;
    private bool isOnGround = true;
    void Start()
    {
        if (Wall != null)
        {
            Wall.SetActive(false); // Ẩn vật cản ban đầu
        }
    }

    void Update()
    {
        Moving();
        if (isOnBridge)
        {
            CheckForwardStep();
        }
    }

    private void Moving()
    {
        float horizontal = joystick.Horizontal;
        float vertical = joystick.Vertical;

        Vector3 direction = new Vector3(-horizontal, 0, -vertical);
        if (isBlockedForward && direction.z < 0 )
        {
            Debug.Log("Không thể tiến lên vì bị chặn!");
            return;
        }
        transform.Translate(direction * Time.deltaTime * Speed);
    }

    private void EatBrick()
    {
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

        isBlockedForward = false;
        if (Wall != null) Wall.SetActive(false);
        Debug.Log("Nhặt gạch - mở khóa di chuyển!");
    }

    private void LostBrick(GameObject brickObject)
    {
        if (Addbrick.Count > 0 && !passedBricks.Contains(brickObject))
        {
            GameObject lastBrick = Addbrick[Addbrick.Count - 1];
            Addbrick.RemoveAt(Addbrick.Count - 1);
            Destroy(lastBrick);
            passedBricks.Add(brickObject);
        }

        if (isOnBridge && Addbrick.Count == 0)
        {
            isBlockedForward = true;
            if (Wall != null) Wall.SetActive(true);
            Debug.Log("Hết gạch trên cầu - Chặn đường!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Green"))
        {
            EatBrick();
            Destroy(other.gameObject);
        }

        if (other.CompareTag("MyBrick"))
        {
            if (Addbrick.Count > 0)
            {
                other.GetComponent<MeshRenderer>().material = mas;
                LostBrick(other.gameObject);
            }
            else
            {
                isBlockedForward = true;
                if (Wall != null) Wall.SetActive(true);
                Debug.Log("Không thể đổi màu vì hết gạch!");
            }
        }
        else if (other.CompareTag("Bridge"))
        {
            isOnBridge = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Bridge"))
        {
            isOnBridge = false;
            isBlockedForward = false;
            if (Wall != null) Wall.SetActive(false);
        }
    }

    private void CheckForwardStep()
    {
        Vector3 rayStart = transform.position + Vector3.up * 0.5f;
        Vector3 rayDirection = transform.forward;

        Debug.DrawRay(rayStart, rayDirection * 1f, Color.yellow, 2f);

        if (Physics.Raycast(rayStart, rayDirection, out RaycastHit hit, 1f))
        {
            Debug.Log("Raycast trúng: " + hit.collider.name);

            if (hit.collider.CompareTag("MyBrick"))
            {
                if (Addbrick.Count > 0)
                {
                    hit.collider.GetComponent<MeshRenderer>().material = mas;
                }
                else
                {
                    isBlockedForward = true;
                    if (Wall != null) Wall.SetActive(true);
                    Debug.Log("Chặn đường vì hết gạch và phía trước là MyBrick!");
                    return;
                }
            }
        }
        else
        {
            isBlockedForward = false;
            if (Wall != null) Wall.SetActive(false);
        }
    }
}






