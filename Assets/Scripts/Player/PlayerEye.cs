using UnityEngine;

public class PlayerEye : MonoBehaviour
{
    Player player;
    public float posX;
    public float posY;

    void Start()
    {
        posX = transform.localPosition.x;
        player = GetComponentInParent<Player>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        player.isFacing = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        player.isFacing = false;
    }
}
