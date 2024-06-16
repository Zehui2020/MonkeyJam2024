using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaRocket : MonoBehaviour
{
    //Pathfinding
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;

    private Transform target;

    private EntityAudioController entityAudioController;

    private void Start()
    {
        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    private void Update()
    {
        FindTarget();

        Vector2 dir = ((Vector2)target.position - rb.position);
        float scaleX = transform.localScale.x;
        if (dir.x > 0)
            transform.localScale = new Vector3(scaleX > 0 ? -scaleX : scaleX, transform.localScale.y, transform.localScale.z);
        else if (dir.x < 0)
            transform.localScale = new Vector3(Mathf.Abs(scaleX), transform.localScale.y, transform.localScale.z);

        transform.position = Vector3.MoveTowards(transform.position, target.position, Time.deltaTime * moveSpeed);
    }

    private void FindTarget()
    {
        Entity closestEntity = null;
        float closestDistance = float.MaxValue;

        foreach (Entity entity in EntitiesController.Instance._entities)
        {
            if (entity is EnemyEntity enemyEntity)
            {
                float distance = Vector3.Distance(transform.position, enemyEntity.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestEntity = enemyEntity;
                }
            }
        }

        target = closestEntity.transform;
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!col.TryGetComponent<EnemyEntity>(out EnemyEntity enemy))
            return;
            
            enemy.Damage(damage);
            entityAudioController.PlayAudio("explode");
            Destroy(gameObject);
    }
}