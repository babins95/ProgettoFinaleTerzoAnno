using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NpcDialogue : MonoBehaviour
{
    public Sprite npcImage;
    public string npcDialogue;
    public GameObject dialogueCanvas;
    public static bool isActive = false;

    //all'interazione do l'immagine e la stringa al canvas del dialogo e lo accendo fermando il tempo
    //premendo di nuovo lo spengo e il tempo riparte
    public void DialogueInteraction()
    {
        dialogueCanvas.transform.GetChild(1).GetComponent<Image>().sprite = npcImage;
        dialogueCanvas.transform.GetChild(3).GetComponent<TMP_Text>().text = npcDialogue;
        if(isActive == true)
        {
            Time.timeScale = 1;
            dialogueCanvas.SetActive(false);
            isActive = false;
        }
        else
        {
            Time.timeScale = 0;
            dialogueCanvas.SetActive(true);
            isActive = true;
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() && !collision.GetComponentInParent<Adult>().hasCrate)
        {
            collision.GetComponentInParent<Player>().interactableObject = gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() && !collision.GetComponentInParent<Adult>().hasCrate)
        {
            collision.GetComponentInParent<Player>().interactableObject = null;
        }
    }

}
