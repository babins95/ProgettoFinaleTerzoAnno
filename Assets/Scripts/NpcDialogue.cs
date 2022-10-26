using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Spine.Unity;

public class NpcDialogue : MonoBehaviour
{
    public SkeletonDataAsset npcImage;
    public Sprite dialogueBox;
    public string npcDialogue;
    public string npcName;
    public GameObject dialogueCanvas;
    public static bool isActive = false;
    public bool giveShoot;

    //all'interazione do l'immagine e la stringa al canvas del dialogo e lo accendo fermando il tempo
    //premendo di nuovo lo spengo e il tempo riparte
    public void DialogueInteraction()
    {
        dialogueCanvas.transform.GetChild(1).GetComponent<SkeletonGraphic>().skeletonDataAsset = npcImage;
        dialogueCanvas.transform.GetChild(1).GetComponent<SkeletonGraphic>().skeletonDataAsset.Clear();
        dialogueCanvas.transform.GetChild(1).GetComponent<SkeletonGraphic>().Initialize(true);
        dialogueCanvas.transform.GetChild(2).GetComponent<Image>().sprite = dialogueBox;
        dialogueCanvas.transform.GetChild(3).GetComponent<TMP_Text>().text = npcDialogue;
        dialogueCanvas.transform.GetChild(4).GetComponent<TMP_Text>().text = npcName.ToUpper();

        if (isActive == true)
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

        if(Player.canShoot == false && giveShoot == true)
        {
            Player.canShoot = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Child>() || collision.GetComponentInParent<Adult>() && !collision.GetComponentInParent<Adult>().hasCrate)
        {
            if (collision.GetComponentInParent<Player>().gameObject.GetComponent<SpriteRenderer>().color.a != 0.5f)
            {
                collision.GetComponentInParent<Player>().interactableObject = gameObject;
                collision.GetComponentInParent<Player>().ActiveE();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Child>() || collision.GetComponentInParent<Adult>() && !collision.GetComponentInParent<Adult>().hasCrate)
        {
            collision.GetComponentInParent<Player>().interactableObject = null;
            collision.GetComponentInParent<Player>().DeactiveE();
        }
    }

}
