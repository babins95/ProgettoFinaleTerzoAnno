using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swap : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //se in fase "negativa" il secondo figlio è attivo e il primo no
        if(GameManager.swap)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
        //se in fase "positiva" il contrario
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        //ogni volta che cambiano le fasi cambia il figlio attivo
        if (GameManager.swap)
        {
            transform.GetChild(0).gameObject.SetActive(false);
            transform.GetChild(1).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
