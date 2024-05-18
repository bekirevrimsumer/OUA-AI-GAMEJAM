using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            _animator.SetBool("IsAim", true);
        }
        if(Input.GetMouseButtonUp(1))
        {
            _animator.SetBool("IsAim", false);
        }

        if (Input.GetMouseButtonDown(0) && _animator.GetBool("IsAim"))
        {
            _animator.SetTrigger("Shoot");
        }
    }
}
