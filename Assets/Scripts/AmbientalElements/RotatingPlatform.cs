using UnityEngine;

public class RotatingPlatform : MonoBehaviour
{
    public bool IsConnectedWithButton;
    public float rotationSpeed;
    private float firstAngle;
    public float rotationAngle;
    private float currentAngle;
    private float angleToReach;
    private bool isRotating = true;
    public float timer;
    private float currentTimer;
    public Transform swappedPlatform;
    //Per lo swap,metto la posizione della piattaforma a quella della piattaforma che era attiva prima
    private void OnEnable()
    {
        if (IsConnectedWithButton == false)
        {
            gameObject.transform.rotation = swappedPlatform.rotation;
            currentAngle = swappedPlatform.gameObject.GetComponent<RotatingPlatform>().currentAngle;
            angleToReach = swappedPlatform.gameObject.GetComponent<RotatingPlatform>().angleToReach;
            currentTimer = 0;
            isRotating = true;
        }
        else
        {
            IsConnectedWithButton = false;
        }
    }
    private void OnDisable()
    {
        if (IsConnectedWithButton == true)
        {
            IsConnectedWithButton = false;
        }
    }
    void Start()
    {
        firstAngle = transform.eulerAngles.z;
        //questi if servono perchè l'enable si attiva prima dello start quindi venivano fuori bug
        if (currentAngle == 0)
        {
            currentAngle = firstAngle;
        }
        if (angleToReach == 0)
        {
            angleToReach = currentAngle + rotationAngle;
        }
    }

    // Update is called once per frame
    void Update()
    {
        IsConnectedWithButton = false;
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
