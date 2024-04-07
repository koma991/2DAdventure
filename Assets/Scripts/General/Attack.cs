using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    [Header("��������")]
    public int attackDamage;
    public int attackRange;


    private void OnTriggerStay2D(Collider2D collision)
    {
        collision.GetComponent<Character>()?.TakeDamage(this);
    }
}
