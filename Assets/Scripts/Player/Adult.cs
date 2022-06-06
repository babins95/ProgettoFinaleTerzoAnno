using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Adult : MonoBehaviour
{
    Player player;

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
        if (player.interactableObject.GetComponent<ClimbableWall>() != null && player.isFacing)
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
            player.interactableObject.GetComponent<Crate>().CrateInteraction(gameObject);
            player.stopAnimation = !player.stopAnimation;
        }
    }
}
