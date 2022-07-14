using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Adult : MonoBehaviour
{
    Player player;
    [HideInInspector]
    public bool hasCrate;

    Animator animator;
    PlayerInput input;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
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
            animator.SetBool("climbing", true);
            input.enabled = false;
        }
    }

    public void ActualClimb()
    {
        //verrà chiamata da un evento all'ultimo frame dell'animazione
        transform.position = player.interactableObject.transform.position;
        input.enabled = true;
    }

    void CrateInteract()
    {
        if (player.interactableObject.GetComponent<Crate>() != null)
        {
            //stopRotation poi sarà per bloccare l'animazione sul personaggio che
            //spinge la cassa, per ora blocca la rotazione
            if (!hasCrate || hasCrate && !player.obstacleAhead && !Platforms.onPlatform)
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
