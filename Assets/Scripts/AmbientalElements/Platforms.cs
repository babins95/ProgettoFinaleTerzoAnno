using UnityEngine;

public class Platforms : MonoBehaviour
{
    private LayerMask collisionLayer;

    //Quando il player entra a contatto con la piattaforma cambio il suo layer in modo che non senta il collider della tilemap,
    //Quando esce resetto tutto

    private void Start()
    {
        collisionLayer = LayerMask.GetMask("Player");
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.gameObject.layer = gameObject.layer;
            foreach(Transform child in collision.transform)
            {
                child.gameObject.layer = gameObject.layer;
            }
            collision.transform.parent = gameObject.transform.GetChild(0).transform;
            collision.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
    //all'uscita tolgo la parentela e resetto la rotazione
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.gameObject.layer = collisionLayer;
            foreach (Transform child in collision.transform)
            {
                child.gameObject.layer = collisionLayer;
            }
            collision.transform.parent = null;
            collision.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
