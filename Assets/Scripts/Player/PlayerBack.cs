using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBack : MonoBehaviour
{
    public void SetPosition(Vector3 newPosition)
    {
        transform.localPosition = newPosition;
    }
}
