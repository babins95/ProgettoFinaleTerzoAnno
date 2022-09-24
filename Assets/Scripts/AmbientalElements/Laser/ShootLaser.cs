using UnityEngine;

public class ShootLaser : MonoBehaviour
{
    public GameObject Laser;
    public LaserBeam laser;

    public ShootLaser nextLaser;
    public bool laserOn;
    private void Start()
    {
        if (laserOn == true)
        {
            CreateLaser();
        }
    }
    public void CreateLaser()
    {
        laser = Instantiate(Laser, GetComponentInParent<Transform>()).GetComponent<LaserBeam>();
    }
    private void Update()
    {
        if (laserOn == false)
        {
            if (GetComponentInChildren<LaserBeam>())
            {
                Destroy(GetComponentInChildren<LaserBeam>().gameObject);
            }
        }
    }

    public void GoNextLaser()
    {
        if (laserOn == true)
        {
            laser = Instantiate(Laser, GetComponentInParent<Transform>()).GetComponent<LaserBeam>();
        }
    }
}
