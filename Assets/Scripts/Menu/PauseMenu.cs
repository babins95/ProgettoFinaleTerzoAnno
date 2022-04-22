using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public static Canvas thisPauseMenu;
    void Awake()
    {
        thisPauseMenu = GetComponent<Canvas>();
        gameObject.SetActive(false);
    }
}
