using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brick : MonoBehaviour
{
    [SerializeField] private BrickType brickType;


    public BrickType GetData()
    {
        return brickType;
    }
   
}
public enum BrickType
{
    Lostbrick
}



