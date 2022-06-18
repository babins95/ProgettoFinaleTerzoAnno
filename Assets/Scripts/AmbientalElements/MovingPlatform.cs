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
    //Per lo swap,metto la posizione della piattaforma a quella della piattaforma che era attiva prima
    private void OnDisable()
    {
        GetComponentInChildren<Player>().transform.parent = swappedPlatform;
    }
    private void OnEnable()
    {
        gameObject.transform.position = swappedPlatform.position;
        nextPosition = swappedPlatform.gameObject.GetComponent<MovingPlatform>().nextPosition;

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
        if (isConnectedWithWallButton == true)
        {
            MovePlatformToNextPos();
        }
    }

    //se è connesso al pulsante va alla posizione successiva e quando arriva si blocca
    private void MovePlatformToNextPos()
    {
        if (transform.position == nextPosition)
        {
            isConnectedWithWallButton = false;
            if (transform.position == pos1.position)
            {
                nextPosition = pos2.position;
            }
            else if (transform.position == pos2.position)
            {
                nextPosition = pos1.position;
            }
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
