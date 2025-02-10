using System.Collections.Generic;
using UnityEngine;

public class MapWord : MonoBehaviour
{
    //    public List<GameObject> prefListBrick = new List<GameObject>(); // Danh sách 4 Prefab
    //    public int numberOfBricks = 50; // Số lượng khối cần tạo
    //    public float spacing = 2.0f;    // Khoảng cách giữa các khối
    //    public float heightOffset = 5.0f; // Độ cao trên trục Y

    //    private List<GameObject> listBrick = new List<GameObject>(); // Danh sách chứa các khối được tạo

    //    void Start()
    //    {
    //        if (prefListBrick.Count < 4)
    //        {
    //            Debug.LogError("Bạn cần thêm ít nhất 4 Prefab vào danh sách prefListBrick.");
    //            return;
    //        }

    //        // Tạo các khối ngẫu nhiên
    //        for (int i = 0; i < numberOfBricks; i++)
    //        {
    //            GameObject randomBrick = prefListBrick[Random.Range(0, prefListBrick.Count)];
    //            listBrick.Add(randomBrick);
    //        }

    //        // Hiển thị các khối trong một lưới
    //        CreateGrid();
    //    }

    //    void CreateGrid()
    //    {
    //        int columns = Mathf.CeilToInt(Mathf.Sqrt(numberOfBricks)); // Số cột
    //        int rows = Mathf.CeilToInt((float)numberOfBricks / columns); // Số hàng

    //        // Tính toán tọa độ căn giữa
    //        float offsetX = (columns - 1) * spacing / 2.0f; // Căn giữa theo trục X
    //        float offsetZ = (rows - 1) * spacing / 2.0f;    // Căn giữa theo trục Z

    //        for (int i = 0; i < rows; i++)
    //        {
    //            for (int j = 0; j < columns; j++)
    //            {
    //                int index = i * columns + j;
    //                if (index >= numberOfBricks) break; // Dừng nếu đủ số khối

    //                GameObject brick = Instantiate(listBrick[index]);
    //                brick.transform.position = new Vector3(
    //                    i * spacing - offsetX,  // Điều chỉnh để căn giữa theo trục X
    //                    heightOffset,           // Đặt độ cao trên trục Y
    //                    j * spacing - offsetZ   // Điều chỉnh để căn giữa theo trục Z
    //                );
    //                brick.transform.parent = transform; // Gắn khối vào đối tượng cha
    //                brick.transform.localScale = Vector3.one * 0.2f; // Thiết lập kích thước
    //            }
    //        }
    //   }


    public List<GameObject> prefListBrick = new List<GameObject>();
    private int col = 10;
    private int row = 10;
    public float r1 = 1;
    public List<GameObject> listBrick = new List<GameObject>();

    Vector3 temp1 = Vector3.zero;
    void Start()
    {
        int soluong = col * row / prefListBrick.Count;

        for (int i = 0; i < prefListBrick.Count; i++)
        {
            for (int j = 0; j < soluong; j++)
            {
                listBrick.Add(prefListBrick[i]);
            }
        }
        Soft();
        for (int i = 0; i < row; i++)
        {
            for (int j = 0; j < col; j++)
            {
                //Instantiate(listBrick[i * 10 + j], new Vector3(i, 0.1f, j), Quaternion.identity, this.transform);
                GameObject a = Instantiate(listBrick[i * 10 + j]);
                a.transform.localPosition = new Vector3(i - (r1 * 5) + 0.5f, 0.1f, j - (r1 * 5) + 0.5f);
                a.transform.parent = gameObject.transform;
                a.transform.localScale = new Vector3(0.3f, 0.2f, 0.2f);

            }
        }


    }
    private void Soft()
    {
        for (int i = 0; i < listBrick.Count; i++)
        {
            int tron = Random.Range(0, 99);
            GameObject tmp = listBrick[i];
            listBrick[i] = listBrick[tron];
            listBrick[tron] = tmp;

        }
    }
}
