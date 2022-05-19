using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Child>())
        {
            PlayerPrefs.SetFloat("xChild", transform.parent.GetChild(0).position.x);
            PlayerPrefs.SetFloat("yChild", transform.parent.GetChild(0).position.y);
        }
        else if(collision.gameObject.GetComponent<Adult>())
        {
            PlayerPrefs.SetFloat("xAdult", transform.parent.GetChild(1).position.x);
            PlayerPrefs.SetFloat("yAdult", transform.parent.GetChild(1).position.y);
        }

        Scene currentScene = SceneManager.GetActiveScene();
        PlayerPrefs.SetString("lastScene", currentScene.name);

        PlayerPrefs.SetInt("saved", 1);
        PlayerPrefs.Save();
    }
}