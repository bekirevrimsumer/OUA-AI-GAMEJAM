using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class NPCState : MonoBehaviour
{
    public bool IsDead = false;
    public bool IsOutlaw = false;

    public void Die()
    {
        IsDead = true;
        GetComponent<Animator>().SetBool("IsDie", true);
        if(!IsOutlaw)
        {
            DOTween.To(() => Time.timeScale, x => Time.timeScale = x, 0.1f, 0.5f);
        }
    }

    public void Rewind()
    {
        IsDead = false;
        GetComponent<Animator>().SetBool("IsReverse", true);
        GetComponent<Animator>().SetBool("IsDie", false);
    }


}
