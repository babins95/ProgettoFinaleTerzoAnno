using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableWall : MonoBehaviour
{
    //importante: ho messo il rigidbody del player su Never Sleep per evitare bug
    //con lo swap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Adult>())
        {
            SetPos(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Adult>())
        {
            collision.GetComponentInParent<Adult>().climbPosition = Vector2.zero;
        }
    }

    void SetPos(Collider2D collision)
    {
        int lastChild = gameObject.transform.parent.childCount - 1;
        if (this.gameObject == gameObject.transform.parent.GetChild(lastChild).gameObject)
        {
            collision.GetComponentInParent<Adult>().climbPosition = gameObject.transform.parent.GetChild(0).position;
        }
        else
        {
            collision.GetComponentInParent<Adult>().climbPosition = gameObject.transform.parent.GetChild(1).position;
        }
    }
}
