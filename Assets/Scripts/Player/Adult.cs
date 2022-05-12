using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adult : MonoBehaviour
{
    Player player;
    bool stop;
    [HideInInspector]
    public Vector2 climbPosition;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    void OnInteract()
    {
        stop = false;
        if (!stop)
            Climb();
    }

    void Climb()
    {
        //animazione del climb
        if (climbPosition != Vector2.zero && player.isFacing)
        {
            transform.position = climbPosition;
            stop = true;
        }
    }
}
