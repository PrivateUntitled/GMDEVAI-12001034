using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPComponent : MonoBehaviour
{
    [SerializeField] private int hp;

    public int Hp { get { return hp; } set { if (value < 0) hp = 0; else hp = value; } }

    public void TakeDamage(int dmg)
    {
        Debug.Log("Damage Taken");

        Hp = this.hp - dmg;

        if (hp <= 0)
            Die();
    }

    // Update is called once per frame
    public void Die()
    {
        Destroy(this.gameObject);
    }
}
