using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Child : MonoBehaviour
{
    Player player;
    Animator animator;
    BulletSpawner spawner;
    public Fade fader;

    private void Start()
    {
        player = gameObject.GetComponent<Player>();
        spawner = GetComponentInChildren<BulletSpawner>();
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
        if (player.interactableObject.GetComponent<NarrowPassage>() != null && player.isFacing)
        {
            fader.GetComponent<Animator>().SetBool("Fading", true);
        }
    }

    public void ActualPassThrough()
    {
        transform.position = player.interactableObject.transform.position;
    }

    public void ActualShoot()
    {
         spawner.ActualShoot();
    }

    public void EndShoot()
    {
        animator.SetBool("shooting", false);
    }
}
