using UnityEngine;

public class PlatformColliderEnter : MonoBehaviour
{
    public EdgeCollider2D edgeCollider;
    //All'entrata spengo il collider del bordo della piattaforma assegnato e all'uscita lo riaccendo
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            edgeCollider.enabled = false;
        }


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            edgeCollider.enabled = true;
        }
    }

}
