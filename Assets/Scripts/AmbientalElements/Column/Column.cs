using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Column : MonoBehaviour
{
    //private void OnEnable()
    //{
    //    if(GetComponentInParent<Swap>().hasFallen)
    //    {
    //        BreakDownColumn();
    //    }
    //}
    
    ////disattiva la colonna in piedi e attiva quella caduta
    ////ho aggiunto il bool hasFallen a Swap per far apparire già caduta la colonna
    ////se l'hai rotta nell'altra versione
    //public void BreakDownColumn()
    //{
    //    gameObject.transform.GetChild(0).gameObject.SetActive(false);
    //    gameObject.transform.GetChild(1).gameObject.SetActive(true);
    //    GetComponentInParent<Swap>().hasFallen = true;
    //}
}
