using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ScrollGrid : MonoBehaviour
{
    [SerializeField] private float scrollSpeed;

    [SerializeField] private List<Transform> moneySpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> airEnemySpawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> groundEnemySpawnPoints = new List<Transform>();

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    [SerializeField] private GameObject moneyPrefab;
    [SerializeField] private GameObject airEnemyPrefab;
    [SerializeField] private GameObject groundEnemyPrefab;

    [SerializeField] private Transform player;

    [SerializeField] private Tilemap grid;

    private void Start()
    {
        OnReachThreshold();
    }

    // Update is called once per frame
    void Update()
    {
        float cameraWidth = Camera.main.orthographicSize * 2 * Camera.main.aspect / 2f;
        float thresholdX = grid.transform.position.x + grid.cellBounds.size.x / 2f - cameraWidth;

        float posX = transform.position.x + scrollSpeed * Time.deltaTime;
        transform.position = new Vector3(posX, transform.position.y, transform.position.z);

        if (transform.position.x >= thresholdX)
            OnReachThreshold();
    }

    private void OnReachThreshold()
    {
        grid.transform.parent.position = new Vector3(player.position.x, grid.transform.parent.position.y, grid.transform.parent.position.z);

        foreach (GameObject gameObject in spawnedEnemies)
            Destroy(gameObject);
        spawnedEnemies.Clear();

        SpawnMoney();
        SpawnEnemies();
    }

    private void SpawnMoney()
    {
        int spawnAmount = Random.Range(3, moneySpawnPoints.Count);

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject money = Instantiate(moneyPrefab, moneySpawnPoints[i]);
            money.transform.localPosition = Vector3.zero;
        }
    }

    private void SpawnEnemies()
    {
        int spawnAmount = Random.Range(1, airEnemySpawnPoints.Count);

        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject airEnemy = Instantiate(airEnemyPrefab, airEnemySpawnPoints[i]);
            airEnemy.transform.localPosition = Vector3.zero;
            spawnedEnemies.Add(airEnemy);
        }

        spawnAmount = Random.Range(1, groundEnemySpawnPoints.Count);
        for (int i = 0; i < spawnAmount; i++)
        {
            GameObject groundEnemy = Instantiate(groundEnemyPrefab, groundEnemySpawnPoints[i]);
            groundEnemy.transform.localPosition = Vector3.zero;
            spawnedEnemies.Add(groundEnemy);
        }
    }
}