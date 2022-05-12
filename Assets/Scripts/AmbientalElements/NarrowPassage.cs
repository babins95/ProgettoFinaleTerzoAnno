using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NarrowPassage : MonoBehaviour
{
    //importante: ho messo il rigidbody del player su Never Sleep per evitare bug
    //con lo swap
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponentInParent<Child>())
        {
            SetPos(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Child>())
        {
            collision.GetComponentInParent<Child>().narrowPosition = Vector2.zero;
        }
    }

    void SetPos(Collider2D collision)
    {
        int lastChild = gameObject.transform.parent.childCount - 1;
        if (this.gameObject == gameObject.transform.parent.GetChild(lastChild).gameObject)
        {
            collision.GetComponentInParent<Child>().narrowPosition = gameObject.transform.parent.GetChild(0).position;
        }
        else
        {
            collision.GetComponentInParent<Child>().narrowPosition = gameObject.transform.parent.GetChild(1).position;
        }
    }
}
