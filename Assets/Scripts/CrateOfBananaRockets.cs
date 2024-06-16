using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrateOfBananaRockets : MonoBehaviour
{
    [SerializeField] private BananaRocket bananaRocket;
    [SerializeField] private Transform rocketSpawnPoint;
    [SerializeField] private float spawnInterval;

    public void SetupCrate(int rocketSpawnCount)
    {
        StartCoroutine(SpawnRockets(rocketSpawnCount));
    }

    private IEnumerator SpawnRockets(int rocketSpawnCount)
    {
        yield return new WaitForSeconds(0.35f);

        for (int i = 0; i < rocketSpawnCount; i++)
        {
            Debug.Log(Instantiate(bananaRocket, rocketSpawnPoint.position, Quaternion.identity));
            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
