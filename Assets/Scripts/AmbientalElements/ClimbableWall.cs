using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbableWall : MonoBehaviour
{
    //nel gameObject ho messo due collider, uno per la collisione con il giocatore
    //uno che funge da trigger per la scalata
    public float newY;

    public float backY;
    public bool goBack;
}
