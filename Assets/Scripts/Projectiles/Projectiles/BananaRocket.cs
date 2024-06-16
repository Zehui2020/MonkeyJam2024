using Pathfinding;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BananaRocket : MonoBehaviour
{
    //Pathfindingd
    private Path path;
    [SerializeField] private Seeker seeker;
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private float moveSpeed;
    [SerializeField] private int damage;

    private Transform target;
    private int currentChaseWaypoint = 0;

    private void Update()
    {
        FindTarget();
        seeker.StartPath(rb.position, target.position, OnPathComplete);

        if (path == null)
            return;

        Vector2 dir = ((Vector2)path.vectorPath[currentChaseWaypoint] - rb.position);

        float scaleX = transform.localScale.x;
        if (dir.x < 0)
            transform.localScale = new Vector3(scaleX > 0 ? -scaleX : scaleX, transform.localScale.y, transform.localScale.z);
        else if (dir.x > 0)
            transform.localScale = new Vector3(Mathf.Abs(scaleX), transform.localScale.y, transform.localScale.z);

        if (Vector2.Distance(rb.position, (Vector2)path.vectorPath[currentChaseWaypoint]) <= 0.5f)
            currentChaseWaypoint++;

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

    private void OnPathComplete(Path p)
    {
        //check if path has any errors
        if (!p.error)
        {
            //no error = create/assign new path
            path = p;
            currentChaseWaypoint = 0;
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Enemy"))
        {
            if (!col.TryGetComponent<EnemyEntity>(out EnemyEntity enemy))
                return;

            enemy.Damage(damage);
        }
    }
}