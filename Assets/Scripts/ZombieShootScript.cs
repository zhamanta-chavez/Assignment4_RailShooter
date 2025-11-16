using UnityEngine;
using System;
using System.Collections;

public class ZombieShootScript : MonoBehaviour
{
    public int zombieHealth = 3;
    Animator _anim;
    ZombieController _zombieCTRL;
    PathFollowScript _follow;

    public void Start()
    {
        {
            _anim = GetComponent<Animator>();
            _zombieCTRL = GetComponent<ZombieController>();
            _follow = GameObject.FindFirstObjectByType<PathFollowScript>();
        }
    }

    public void TakeDamage(int _damage, bool _isHead, bool _isShotgun)
    {
        zombieHealth -= _damage; ;
        if (zombieHealth <= 0)
        {
            ZombieDead(_isShotgun);
        }
        else
        {
            if (_isHead)
            {
                _anim.SetTrigger("Head01");
            }
            else
            {
                _anim.SetTrigger("Body01");
            }
            _zombieCTRL.isHurt = true;
            StartCoroutine(ResetDamage());
        }
    }

    public void ZombieDead(bool _isShotgun)
    {
        _zombieCTRL.isDead = true;
        if (_isShotgun)
        {
            _anim.SetTrigger("Dead02");
        }
        else
        {
            _anim.SetTrigger("Dead01");
        }
        _follow.SubtractEnemy();
        Destroy(gameObject, 2f);
    }

    IEnumerator ResetDamage()
    {
        yield return new WaitForSeconds(.25f);
        _zombieCTRL.isHurt = false;
    }
}
