using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Player player;
    [SerializeField] Vector3 spawnPosition;

    float playerX;
    float playerY;

    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.GetInt("saved") == 1)
        {
            playerX = PlayerPrefs.GetFloat("checkpointX");
            playerY = PlayerPrefs.GetFloat("checkpointY");
            player.transform.position = new Vector3(playerX, playerY, 0);
        }
        else
        {
            player.transform.position = spawnPosition;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
