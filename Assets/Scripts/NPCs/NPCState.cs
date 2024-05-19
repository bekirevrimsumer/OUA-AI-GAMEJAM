using System.Collections;
using System.Collections.Generic;
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
    }

    public void Rewind()
    {
        IsDead = false;
        GetComponent<Animator>().SetBool("IsReverse", true);
        GetComponent<Animator>().SetBool("IsDie", false);
    }


}
