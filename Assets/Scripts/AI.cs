using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AI : MonoBehaviour
{
    private NavMeshAgent agent;
    public Transform targer;
    private float searchRadius = 10f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        //FindNearsBrick();
    }

    // Update is called once per frame
    void Update()
    {
        //if (agent != null && agent.isOnNavMesh)
        //{
        //    agent.SetDestination(targer.position);
        //    //FindNearsBrick();
        //}
        agent.SetDestination(targer.position);
    }
    //void FindNearsBrick()
    //{
    //    GameObject[] bricks = GameObject.FindGameObjectsWithTag("Black"); // Tìm tất cả gạch trên bản đồ
    //    GameObject nearestBrick = null;
    //    float minDistance = Mathf.Infinity;

    //    foreach (GameObject brick in bricks)
    //    {
    //        float distance = Vector3.Distance(transform.position, brick.transform.position);
    //        if (distance < minDistance && distance <= searchRadius)
    //        {
    //            minDistance = distance;
    //            nearestBrick = brick;
    //        }
    //    }

    //    if (nearestBrick != null)
    //    {
    //        agent.SetDestination(nearestBrick.transform.position);
    //    }

    //}
    //protected void BlockAI(GameObject Wall)
    //{
    //    Debug.Log("vao day");
    //    Block(Wall);
    //}


    //protected void Block(GameObject Wall)
    //{

    //    BoxCollider block = Wall.GetComponent<BoxCollider>();
    //    if (block != null)
    //    {

    //        block.enabled = false;
    //    }
    //}
    //protected void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {

    //        if (Addbrick.Count > 0)
    //        {
    //            Debug.Log("1");
    //            Block(collision.gameObject);
    //        }
    //    }

    //}
    //protected void EatBrickAI()
    //{
    //    EatBrick();
    //}
    //protected void LostBrickAI(GameObject bricObject)
    //{
    //    LostBrick(bricObject);
    //}

    //protected override void OnTriggerEnter(Collider other)
    //{
    //    base.OnTriggerEnter(other);

    //    if (other.gameObject.tag == "Black")
    //    {

    //        EatBrick();
    //        // Xóa khỏi danh sách nếu tồn tại
    //        //if (Addbrick.Contains(other.gameObject))
    //        //{
    //        //    Addbrick.Remove(other.gameObject);
    //        //}
    //        Destroy(other.gameObject);
    //        //FindNearsBrick();
    //    }


    //    else if (other.gameObject.tag == null)
    //    {
    //        return;

    //    }

    //    if (other.gameObject.tag == "MyBrick")
    //    {
    //        if (Addbrick.Count > 0)
    //        {
    //            other.GetComponent<MeshRenderer>().material = mas;
    //            LostBrick(other.gameObject);
    //        }

    //    }

    //}
    //  protected override void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.CompareTag("Wall"))
    //    {
    //        if (Addbrick.Count > 0)
    //        {
    //            Block(collision.gameObject);
    //        }
    //    }

    //}

}



