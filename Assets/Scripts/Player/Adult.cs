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

    Transform toReach;
    Transform starting;
    float timeElapsed = 5f;
    float lerpDuration;
    float currentY;


    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        animator = GetComponent<Animator>();
        input = GetComponent<PlayerInput>();
        GetAnimationTime();
    }

    private void Update()
    {
        if(timeElapsed < lerpDuration)
        {
            currentY = Mathf.Lerp(starting.position.y, toReach.position.y, timeElapsed / lerpDuration);
            timeElapsed += Time.deltaTime;
            transform.position = new Vector2(transform.position.x, currentY);
        }

    }

    void GetAnimationTime()
    {
        AnimationClip[] clips = animator.runtimeAnimatorController.animationClips;
        foreach(AnimationClip clip in clips)
        {
            if(clip.name == "Climb")
            {
                lerpDuration = clip.length;
            }
        }
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
        if (player.interactableObject.GetComponent<ClimbableWall>() != null && !hasCrate)
        {
            ClimbableWall wallToClimb = player.interactableObject.GetComponent<ClimbableWall>();
            
            animator.SetBool("climbing", true);
            player.moveVector = Vector2.zero;
            input.DeactivateInput();
            if(!wallToClimb.goingDown)
            {
                starting = wallToClimb.transform.parent.GetChild(1).gameObject.transform;
                toReach = wallToClimb.transform.parent.GetChild(0).gameObject.transform;
                timeElapsed = 0f;
            }
            else
            {
                starting = wallToClimb.transform.parent.GetChild(0).gameObject.transform;
                toReach = wallToClimb.transform.parent.GetChild(1).gameObject.transform;
                timeElapsed = 0f;
            }
        }
    }

    public void ActualClimb()
    {
        //verrà chiamata da un evento all'ultimo frame dell'animazione
        input.ActivateInput();
        animator.SetBool("climbing", false);
        transform.position = new Vector2(toReach.position.x, toReach.position.y);
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
