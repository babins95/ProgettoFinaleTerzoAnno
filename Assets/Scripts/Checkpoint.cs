using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerPrefs.SetFloat("checkpointX", this.transform.position.x);
        PlayerPrefs.SetFloat("checkpointY", this.transform.position.y);
        PlayerPrefs.SetInt("saved", 1);
        PlayerPrefs.Save();

        Debug.Log("Spawn set");
    }
}
