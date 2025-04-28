using System.Collections;
using System.Collections.Generic;
using Platformer.Combat.Damage;
using UnityEngine;

public class CombatTestDummy : MonoBehaviour, IDamageable
{
    [SerializeField] private GameObject hitParticles;

    private Animator anim;

    public void Damage(DamageData amount)
    {
        Instantiate(hitParticles, transform.position, Quaternion.Euler(0.0f,0.0f, Random.Range(0.0f, 360.0f)));
        anim.SetTrigger("damage");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }
}
