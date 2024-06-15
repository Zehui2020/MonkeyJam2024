using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollGrid : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;
    [SerializeField] private float thresholdX;

    [SerializeField] private List<Transform> moneySpawnPoints = new List<Transform>();
    [SerializeField] private GameObject moneyPrefab;

    private void Start()
    {
        OnReachThreshold();
    }

    // Update is called once per frame
    void Update()
    {
        float posX = transform.position.x - scrollSpeed * Time.deltaTime;
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);

        if (transform.position.x <= thresholdX)
            OnReachThreshold();
    }

    private void OnReachThreshold()
    {
        transform.position = Vector3.zero;
        SpawnMoney();
    }

    private void SpawnMoney()
    {
        int spawnAmount = Random.Range(3, moneySpawnPoints.Count);

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject money = Instantiate(moneyPrefab, moneySpawnPoints[i]);
            money.transform.position = Vector3.zero;
        }
    }
}
