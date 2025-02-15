using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class brickOnbridge : MonoBehaviour
{
    [SerializeField] private BrickOnBridgeType brickOnBridgeType;

    public BrickOnBridgeType GetBrick()
    {
        return brickOnBridgeType;
    }
}

public enum BrickOnBridgeType
{
    Brick1 = 1,
    Brick2 = 2,
    Brick3 = 3,
    Brick4 = 4,
    Brick5 = 5,
    Brick6 = 6,
    Brick7 = 7,
    Brick8 = 8,
    Brick9 = 9,
    Brick10 = 10,
    Brick11 = 11



}
