using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fade : MonoBehaviour
{
    public Player player;
    public bool fading;
    Animator fadeAnim;

    private void Start()
    {
        fadeAnim = GetComponent<Animator>();
    }

    public void FreezePlayer()
    {
        player.Freeze();
    }

    public void UnFreezePlayer()
    {
        fadeAnim.SetBool("Fading", false);
        player.UnFreeze();
    }

    public void MovePlayer()
    {
        player.GetComponent<Child>().ActualPassThrough();
    }
}
