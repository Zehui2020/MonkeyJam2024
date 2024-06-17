using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Drone : MonoBehaviour
{
    [SerializeField] private FollowPosition followPosition;

    [SerializeField] private float fireRate;
    [SerializeField] private float reloadDuration;
    [SerializeField] private float detectRadius;
    private int ammoCount;
    [SerializeField] private int magSize;
    [SerializeField] private int damage;

    [SerializeField] private LayerMask enemyLayer;
    [SerializeField] private Slider reloadSlider;

    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform firePoint;

    private GameObject target;

    private EntityAudioController entityAudioController;

    private void Start()
    {
        ammoCount = magSize;
        reloadSlider.gameObject.SetActive(false);

        //sound
        entityAudioController = GetComponent<EntityAudioController>();
        //check if don't have component
        if (entityAudioController == null)
        {
            //add component
            entityAudioController = gameObject.AddComponent<EntityAudioController>();
        }
    }

    public enum DroneState
    {
        IDLE,
        COMBAT,
        RELOAD
    }
    private DroneState currentState = DroneState.IDLE;

    private Coroutine shootRoutine;
    private Coroutine reloadRoutine;

    private void ChangeState(DroneState newState)
    {
        currentState = newState;
    }

    public void SetupDrone(Transform followPos)
    {
        followPosition.SetFollowPos(followPos);
    }

    public void UpgradeDrone()
    {
        damage = (int)(damage * 0.15f);
        magSize = (int)(magSize * 0.1f);
    }

    private void Update()
    {
        Collider2D[] cols = Physics2D.OverlapCircleAll(transform.position, detectRadius, enemyLayer);
        if (cols.Length > 0 && currentState == DroneState.IDLE)
        {
            target = cols[0].gameObject;
            ChangeState(DroneState.COMBAT);
        }
        else if (cols.Length == 0 && currentState != DroneState.IDLE)
        {
            target = null;
            ChangeState(DroneState.IDLE);
        }

        switch (currentState)
        {
            case DroneState.IDLE:
                break;

            case DroneState.COMBAT:
                if (shootRoutine == null)
                    shootRoutine = StartCoroutine(ShootRoutine());

                if (ammoCount <= 0)
                    ChangeState(DroneState.RELOAD);

                break;

            case DroneState.RELOAD:
                if (reloadRoutine == null)
                    reloadRoutine = StartCoroutine(ReloadRoutine());

                break;
        }
    }

    private IEnumerator ReloadRoutine()
    {
        reloadSlider.maxValue = reloadDuration;
        reloadSlider.gameObject.SetActive(true);
        reloadSlider.value = 0;

        float timer = 0;

        while (timer < reloadDuration)
        {
            timer += Time.deltaTime;
            reloadSlider.value = timer;

            yield return null;
        }
        entityAudioController.PlayAudio("reload");
        ammoCount = magSize;
        reloadSlider.gameObject.SetActive(false);
        ChangeState(DroneState.IDLE);
        reloadRoutine = null;
    }

    private IEnumerator ShootRoutine()
    {
        //shoot sound
        entityAudioController.PlayAudio("droneshot");
        GameObject newProjectile = Instantiate(projectile, firePoint.position, Quaternion.identity);
        newProjectile.TryGetComponent<Rigidbody2D>(out Rigidbody2D projectileRB);
        projectileRB.AddForce(-(transform.position - target.transform.position).normalized * 20, ForceMode2D.Impulse);
        target.TryGetComponent<EnemyEntity>(out EnemyEntity enemy);
        enemy.Damage(damage);
        ammoCount--;

        yield return new WaitForSeconds(fireRate);

        shootRoutine = null;
    }
}