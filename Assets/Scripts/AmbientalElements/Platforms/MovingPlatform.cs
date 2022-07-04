using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPosition;
    public float timer;
    private float currentTimer;
    public Transform swappedPlatform;
    public bool isConnectedWithFloorButton;
    public bool isConnectedWithWallButton = false;

    Vector3 nextPosition;
    private void OnEnable()
    {
        gameObject.transform.position = swappedPlatform.position;
        nextPosition = swappedPlatform.gameObject.GetComponent<MovingPlatform>().nextPosition;
        if (swappedPlatform.GetComponentInChildren<Player>())
        {
            swappedPlatform.GetComponentInChildren<Player>().transform.parent = gameObject.transform.GetChild(0);
        }

    }
    void Start()
    {
        nextPosition = startPosition.position;
        currentTimer = 0;
    }
    void Update()
    {
        if (isConnectedWithFloorButton == true)
        {
            MovePlatform();
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }

    //se è connesso al pulsante va alla posizione successiva e quando arriva si blocca
    public void MovePlatformToNextPos(bool isConnected)
    {
        if (isConnected == true)
        {
            nextPosition = pos2.position;
        }
        else
        {
            nextPosition = pos1.position;
        }
        //ho messo questo if di nuovo perchè altrimenti si muoveva un po' prima di fermarsi
        if (isConnectedWithWallButton == true)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
        }
    }

    //Quando la piattaforma arriva alla next position parte il timer e scaduto questo cambia la next position
    private void MovePlatform()
    {
        if (transform.position == pos1.position)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= timer)
            {
                nextPosition = pos2.position;
                currentTimer = 0;
            }
        }
        if (transform.position == pos2.position)
        {
            currentTimer += Time.deltaTime;
            if (currentTimer >= timer)
            {
                nextPosition = pos1.position;
                currentTimer = 0;
            }
        }
        transform.position = Vector3.MoveTowards(transform.position, nextPosition, speed * Time.deltaTime);
    }
    //quando il player entra a contatto con la piattaforma diventa figlio del figlio della piattaforma
    //in modo tale che non scali ma segua la posizione della piattaforma

}
