using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableWall : MonoBehaviour
{
    //importante: ho messo il rigidbody del player su Never Sleep per evitare bug
    //con lo swap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //dopo il gamemanager.swap diventerà gameobject.getcomponent<Teen>()
        if(GameManager.swap)
        {
            SetPos(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(GameManager.swap)
        {
            SetPos(collision);
        }
        else
        {
            collision.GetComponent<Player>().climbPosition = Vector2.zero;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<Player>().climbPosition = Vector2.zero;
    }

    void SetPos(Collider2D collision)
    {
        int lastChild = gameObject.transform.parent.childCount - 1;
        if (this.gameObject == gameObject.transform.parent.GetChild(lastChild).gameObject)
        {
            collision.GetComponent<Player>().climbPosition = gameObject.transform.parent.GetChild(0).position;
        }
        else
        {
            collision.GetComponent<Player>().climbPosition = gameObject.transform.parent.GetChild(1).position;
        }
    }
}
