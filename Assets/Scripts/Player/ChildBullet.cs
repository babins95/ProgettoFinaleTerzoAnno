using UnityEngine;

public class ChildBullet : MonoBehaviour
{
    public float childBulletSpeed = 5f;
    public Vector3 bulletAngle;
    public float bulletDeathTimer = 2f;
    private float timer;
    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bulletDeathTimer)
        {
            Destroy(gameObject);
        }
        transform.position += childBulletSpeed * Time.deltaTime * bulletAngle;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }
}
