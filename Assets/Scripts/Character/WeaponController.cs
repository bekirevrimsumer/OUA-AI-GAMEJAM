using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    private Animator _animator;
    public Transform WeaponTransform;
    private GameObject _selectedNpc;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        HandleAiming();
        HandleShooting();
        HandleEscape();
        HandleMouseInput();
    }

    private void HandleAiming()
    {
        var isAim = _animator.GetBool("IsAim");

        if (_selectedNpc != null)
        {
            UIManager.Instance.OpenPanel(UIManager.Instance.ShootInfoPanel);
            _animator.SetBool("IsAim", true);
            WeaponTransform.gameObject.SetActive(true);

            if (isAim)
            {
                Vector3 direction = _selectedNpc.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
            }
        }
        else
        {
            UIManager.Instance.ClosePanel(UIManager.Instance.ShootInfoPanel);
            _animator.SetBool("IsAim", false);
            WeaponTransform.gameObject.SetActive(false);
        }
    }

    private void HandleShooting()
    {
        if (Input.GetKeyDown(KeyCode.F) && _selectedNpc != null)
        {
            _animator.SetTrigger("Shoot");
            SoundManager.Instance.PlaySound(SoundManager.Instance.GunShotSound, transform.position, 0.25f);
            HandleNPCInteraction();
            _selectedNpc = null;
        }
    }

    private void HandleNPCInteraction()
    {
        _selectedNpc.GetComponent<Outline>().enabled = false;
        var npcState = _selectedNpc.GetComponent<NPCState>();
        if (npcState.IsOutlaw)
        {
            SoundManager.Instance.PlaySound(SoundManager.Instance.MissionSuccessSound, transform.position, 0.15f);
            UIManager.Instance.OpenPanel(UIManager.Instance.SuccessMissionPanel);
        }
        else
        {
            UIManager.Instance.OpenPanel(UIManager.Instance.FailedMissionPanel);
            SoundManager.Instance.PlaySound(SoundManager.Instance.MissionFailSound, transform.position, 0.15f);
        }
        npcState.Die();
    }

    private void HandleEscape()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && _selectedNpc != null)
        {
            _animator.SetBool("IsAim", false);
            _selectedNpc.GetComponent<Outline>().enabled = false;
            _selectedNpc = null;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, LayerMask.GetMask("Enemy")))
            {
                if (hit.collider.CompareTag("Enemy"))
                {
                    if (_selectedNpc != null)
                        _selectedNpc.GetComponent<Outline>().enabled = false;

                    SoundManager.Instance.PlaySound(SoundManager.Instance.AimSound, transform.position, 0.3f);
                    _selectedNpc = hit.collider.gameObject;
                    hit.collider.GetComponent<Outline>().enabled = true;
                }
            }
        }
    }
}