using UnityEngine;

public class ChildBullet : MonoBehaviour
{
    public float childBulletSpeed = 5f;
    public Vector3 bulletAngle;
    public float bulletDeathTimer = 2f;
    private float timer;

    public bool left;
    public bool right = true;

    private void Update()
    {
        timer += Time.deltaTime;
        if (timer >= bulletDeathTimer)
        {
            Destroy(gameObject);
        }
        transform.position += childBulletSpeed * Time.deltaTime * bulletAngle;

        Rotate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(this.gameObject);
    }

    void Rotate()
    {
        if(transform.eulerAngles.z  == 300f)
        {
            left = true;
            right = false;
        }
        else if(transform.eulerAngles.z == 359f)
        {
            right = true;
            left = false;
        }

        if(right)
        {
            transform.Rotate(0, 0, -1f);
        }
        else if(left)
        {
            transform.Rotate(0, 0, 1f);
        }
    }
}
