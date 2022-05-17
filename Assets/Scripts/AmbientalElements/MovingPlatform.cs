using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform pos1, pos2;
    public float speed;
    public Transform startPosition;
    public float timer;
    private float currentTimer;
    public Transform swappedPlatform;
    public bool IsConnectedWithButton;

    Vector3 nextPosition;
    //Per lo swap,metto la posizione della piattaforma a quella della piattaforma che era attiva prima
    private void OnEnable()
    {
        if (IsConnectedWithButton == false)
        {
            gameObject.transform.position = swappedPlatform.position;
            nextPosition = swappedPlatform.gameObject.GetComponent<MovingPlatform>().nextPosition;
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
        nextPosition = startPosition.position;
        currentTimer = 0;
    }
    void Update()
    {
        MovePlatform();
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
