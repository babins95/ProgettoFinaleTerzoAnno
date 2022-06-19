using UnityEngine;

public class PlatformColliderEnter : MonoBehaviour
{
    private bool playerIsOnEdge = false;
    public EdgeCollider2D edgeCollider;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            edgeCollider.enabled = false;
        }
        else
        {
            edgeCollider.enabled = true;
        }

    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            playerIsOnEdge = false;
        }
        if (collision.GetComponent<EdgeCollider2D>())
        {
            collision.GetComponent<EdgeCollider2D>().enabled = true;
        }
    }
}
