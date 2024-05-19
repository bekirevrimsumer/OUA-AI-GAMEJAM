using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Animator _animator;
    public Transform WeaponTransform;
    public Transform RaycastOrigin;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var isAim = _animator.GetBool("IsAim");

        if (Input.GetMouseButtonDown(1))
        {
            _animator.SetBool("IsAim", true);
            WeaponTransform.gameObject.SetActive(true);
        }
        if(Input.GetMouseButtonUp(1))
        {
            _animator.SetBool("IsAim", false);
            WeaponTransform.gameObject.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && isAim)
        {
            _animator.SetTrigger("Shoot");
        }

        //raycast ile ateş etme işlemi
        if (isAim)
        {
            RaycastHit hit;
            if (Physics.Raycast(RaycastOrigin.position, RaycastOrigin.forward, out hit, 100, LayerMask.GetMask("Enemy")))
            {
                Debug.DrawLine(RaycastOrigin.position, hit.point, Color.red);
                Debug.Log(hit.transform.name);
                if(hit.transform.CompareTag("Enemy"))
                {
                    hit.transform.GetComponent<Outline>().enabled = true;
                }

                DOVirtual.DelayedCall(1f, () =>
                {
                    if(hit.transform.CompareTag("Enemy"))
                    {
                        hit.transform.GetComponent<Outline>().enabled = false;
                    }
                });
            }
        }
    }
}
