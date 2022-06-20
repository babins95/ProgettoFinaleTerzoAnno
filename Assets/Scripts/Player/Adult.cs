using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adult : MonoBehaviour
{
    Player player;
    [HideInInspector]
    public bool hasCrate;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
    }

    void OnInteract()
    {
        if (player.interactableObject != null)
        {
            Climb();
            CrateInteract();
        }
    }

    void Climb()
    {
        //animazione del climb
        if (player.interactableObject.GetComponent<ClimbableWall>() != null && player.isFacing && !hasCrate)
        {
            transform.position = player.interactableObject.transform.position;
        }
    }

    void CrateInteract()
    {
        if (player.interactableObject.GetComponent<Crate>() != null)
        {
            //stopRotation poi sarà per bloccare l'animazione sul personaggio che
            //spinge la cassa, per ora blocca la rotazione
            if (!hasCrate || hasCrate && !player.obstacleAhead)
            {
                player.interactableObject.GetComponent<Crate>().CrateInteraction(gameObject);
            }
            else
            {
                //suono del "non puoi"
                Debug.Log("NO!");
            }
            //player.stopAnimation = !player.stopAnimation;
        }
    }
}
