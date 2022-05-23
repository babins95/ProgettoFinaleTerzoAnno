using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpawnPoint : MonoBehaviour
{
    [SerializeField] bool child;
    [SerializeField] bool adult;

    private void Start()
    {
        //SavePosition();
    }

    public void SavePosition()
    {
        if (child)
        {
            PlayerPrefs.SetFloat("xChild", transform.parent.GetChild(0).position.x);
            PlayerPrefs.SetFloat("yChild", transform.parent.GetChild(0).position.y);
        }
        else if (adult)
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