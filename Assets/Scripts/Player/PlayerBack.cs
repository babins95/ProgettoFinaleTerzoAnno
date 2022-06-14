using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBack : MonoBehaviour
{
    public float posX;
    public float posY;

    void Start()
    {
        posX = transform.localPosition.x;
    }
}
