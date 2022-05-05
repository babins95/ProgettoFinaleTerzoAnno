using UnityEngine;

public class SwitchCounter : MonoBehaviour
{
    public int InitialSwitchCount;
    private int currentswitchCount;
    public GameManager gameManager;
    public static bool isOnCheckpoint = false;
    // Start is called before the first frame update
    void Start()
    {
        currentswitchCount = InitialSwitchCount;
    }
    private void Update()
    {
        if (isOnCheckpoint)
        {
            ResetCounter();
        }
    }

    //ogni volta che parte lo switch disattivo l'ultimo figlio ancora attivo e all'ultimo switch parte lo spawn
    public void SwitchUsed()
    {
        if (currentswitchCount > 1)
        {
            gameObject.transform.GetChild(currentswitchCount-1).gameObject.SetActive(false);
            currentswitchCount--;
        }
        else
        {
            gameManager.Spawn();
        }
    }
    //resetto il counter e attivo tutti i figli
    public void ResetCounter()
    {
        currentswitchCount = InitialSwitchCount;
        for (int i = 0; i < InitialSwitchCount; i++)
        {
            gameObject.transform.GetChild(i).gameObject.SetActive(true);
        }
    }
}
