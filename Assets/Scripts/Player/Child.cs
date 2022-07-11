using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    Player player;
    Animator animator;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        player.bulletSpawner = GetComponentInChildren<BulletSpawner>();
        animator = GetComponent<Animator>();
    }

    void OnInteract()
    {
        if (player.interactableObject != null)
        {
            PassThrough();
        }
    }

    void PassThrough()
    {
        //animazione del passaggio
        if (player.interactableObject.GetComponent<NarrowPassage>() != null && player.isFacing)
        {
            transform.position = player.interactableObject.transform.position;
        }
    }

    public void EndShoot()
    {
        animator.SetBool("shooting", false);
    }
}
