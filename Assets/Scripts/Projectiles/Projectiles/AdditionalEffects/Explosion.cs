using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] Collider2D col;
    public void Explode(float length, Vector3 position, string ownerName, int damage)
    {
        transform.localScale = new Vector3(length, length, 1);
        transform.position = position;
        List<Collider2D> cols = new List<Collider2D>();
        col.OverlapCollider(new ContactFilter2D().NoFilter(),cols);
        foreach (Collider2D col in cols)
        {
            if (ownerName.Equals("Player"))
            {
                if (col.gameObject.CompareTag("Enemy"))
                {
                    col.gameObject.GetComponent<EnemyEntity>().Damage(damage);
                }
            }
            else if (ownerName.Equals("Enemy"))
            {
                if (col.gameObject.CompareTag("Player"))
                {
                    col.gameObject.GetComponent<PlayerHealth>().AddHealth(-1);
                }
            }
        }
    }
}
