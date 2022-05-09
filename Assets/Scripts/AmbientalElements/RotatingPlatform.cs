using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public float rotationSpeed;
    private float firstAngle;
    public float rotationAngle;
    private float currentAngle;
    private float angleToReach;
    private bool isRotating = true;
    public float timer;
    private float currentTimer;
    // Start is called before the first frame update
    void Start()
    {
        firstAngle = transform.eulerAngles.z;
        currentAngle = firstAngle;
        currentTimer = 0;
        angleToReach = currentAngle + rotationAngle;
    }

    // Update is called once per frame
    void Update()
    {
        if (isRotating == true)
        {
            Rotation();
        }
        //alla fine del timer isRotating viene messo a true,
        //cambio l'angolo di destinazione e resetto il timer
        if (currentTimer >= timer)
        {
            angleToReach = angleToReach + rotationAngle;
            isRotating = true;
            currentTimer = 0;
        }
        //se l'angolo corrente è maggiore o uguale all'angolo di destinazione
        //isRotating è false e parte il timer
        //se è sopra a 360 gradi resetto per non avere numeri troppo alti
        if (currentAngle >= angleToReach)
        {
            if (currentAngle >= 360)
            {
                currentAngle = firstAngle;
                angleToReach = currentAngle;
            }
            currentTimer += Time.deltaTime;
            isRotating = false;
        }
    }
    //aumento l'angolo e setto il transform rotation di conseguenza
    private void Rotation()
    {
        currentAngle += rotationSpeed * Time.deltaTime;
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
    }
    //quando il player entra a contatto con la piattaforma diventa figlio del figlio della piattaforma
    //in modo tale che non scali ma segua la posizione della piattaforma
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.transform.parent = gameObject.transform.GetChild(0).transform;
            collision.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
    //all'uscita tolgo la parentela e resetto la rotazione
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Player>())
        {
            collision.transform.parent = null;
            collision.transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
