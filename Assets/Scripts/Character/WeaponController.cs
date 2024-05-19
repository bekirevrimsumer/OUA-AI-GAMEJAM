using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    Animator _animator;
    public Transform WeaponTransform;
    private GameObject _selectedNpc;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        var isAim = _animator.GetBool("IsAim");

        // if (Input.GetMouseButtonDown(1))
        // {
        //     _animator.SetBool("IsAim", true);
        //     WeaponTransform.gameObject.SetActive(true);
        // }
        // if(Input.GetMouseButtonUp(1))
        // {
        //     _animator.SetBool("IsAim", false);
        //     WeaponTransform.gameObject.SetActive(false);
        // }

        // if (Input.GetMouseButtonDown(0) && isAim)
        // {
        //     _animator.SetTrigger("Shoot");
        // }

        if(_selectedNpc != null)
        {
            _animator.SetBool("IsAim", true);
        }
        else
        {
            _animator.SetBool("IsAim", false);
        }

        if(Input.GetKeyDown(KeyCode.F) && _selectedNpc != null)
        {
            _animator.SetTrigger("Shoot");
            SoundManager.Instance.PlaySound(SoundManager.Instance.GunShotSound, transform.position, 0.5f);
            _selectedNpc.GetComponent<Outline>().enabled = false;
            var npcState = _selectedNpc.GetComponent<NPCState>();
            if(npcState.IsOutlaw)
            {
                npcState.Die();
            }
            else
            {
                npcState.Die();
            }

            _selectedNpc = null;
        }

        if(Input.GetKeyDown(KeyCode.Escape) && _selectedNpc != null)
        {
            _animator.SetBool("IsAim", false);
            _selectedNpc.GetComponent<Outline>().enabled = false;
            _selectedNpc = null;
        }

        if(isAim)
        {
            if(_selectedNpc != null)
            {
                Vector3 direction = _selectedNpc.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.CompareTag("Enemy"))
                {
                    if(_selectedNpc != null)
                        _selectedNpc.GetComponent<Outline>().enabled = false;

                    SoundManager.Instance.PlaySound(SoundManager.Instance.AimSound, transform.position);
                    _selectedNpc = hit.collider.gameObject;
                    hit.collider.GetComponent<Outline>().enabled = true;
                }
            }
        }
    }
}
