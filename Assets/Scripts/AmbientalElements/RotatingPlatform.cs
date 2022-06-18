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
        gameObject.transform.rotation = swappedPlatform.rotation;
        currentAngle = swappedPlatform.gameObject.GetComponent<RotatingPlatform>().currentAngle;
        angleToReach = swappedPlatform.gameObject.GetComponent<RotatingPlatform>().angleToReach;
        currentTimer = 0;
        isRotating = true;
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
        if (IsConnectedWithButton == true)
        {
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
            if (isRotating == true)
            {
                Rotation();
            }
        }
    }
    //aumento l'angolo e setto il transform rotation di conseguenza
    private void Rotation()
    {
        transform.rotation = Quaternion.Euler(0, 0, currentAngle);
        currentAngle += rotationSpeed * Time.deltaTime;
    }
    //quando il player entra a contatto con la piattaforma diventa figlio del figlio della piattaforma
    //in modo tale che non scali ma segua la posizione della piattaforma
    
}
