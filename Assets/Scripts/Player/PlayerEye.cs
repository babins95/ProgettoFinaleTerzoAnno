using UnityEngine;

public class PlayerEye : MonoBehaviour
{
    Player player;
    public float posX;
    public float posY;

    [HideInInspector]
    public bool onHole;
    [HideInInspector]
    public GameObject holeColliding;

    void Start()
    {
        posX = transform.localPosition.x;
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.isFacing = true;
        if (collision.gameObject.GetComponent<Hole>())
        {
            onHole = true;
            holeColliding = collision.gameObject;
        }

        //if(collision.gameObject.layer == 13)
        //{
        //    player.obstacleAhead = true;
        //}
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.isFacing = false;
        //player.obstacleAhead = false;

        if (collision.GetComponent<Hole>())
        {
            onHole = false;
        }
    }
}
