using System.Collections;
using UnityEngine;

public class FruitSpawner : MonoBehaviour
{
    [Header("Fruits")]
    public GameObject[] fruitPrefabs;
    public GameObject bombPrefab;
    public float bombSpawnChance;

    [Header("Spawners")]
    public Transform[] fruitSpawners;
    public float spawnerOffset;
    public float minForce, maxForce;

    public bool womboCombo;
    private void Start()
    {
        StartCoroutine(SpawnFruitCoroutine());
    }
    public void SpawnFruit()
    {
        bool spawnBomb = Random.Range(0f,100f) < bombSpawnChance;

        if (spawnBomb)
        {
            Vector3 spawnPos = GetSpawnPosition();
            GameObject bomb = Instantiate(bombPrefab, spawnPos, Quaternion.identity, transform);

            Vector3 dir = transform.position - spawnPos;
            float force = Random.Range(minForce, maxForce);
            bomb.GetComponent<Rigidbody>().AddForce(dir.normalized * force);
            FruitRotate(bomb);
        }
        else
        {
            int index = Random.Range(0, fruitPrefabs.Length - 1);
            Vector3 spawnPos = GetSpawnPosition();
            GameObject fruit = Instantiate(fruitPrefabs[index], spawnPos, Quaternion.identity, transform);

            Vector3 dir = transform.position - spawnPos;
            float force = Random.Range(minForce, maxForce);
            fruit.GetComponent<Rigidbody>().AddForce(dir.normalized * force);
            FruitRotate(fruit);
        }
    }

    private Vector3 GetSpawnPosition()
    {
        float offsetX = Random.Range(-spawnerOffset, spawnerOffset);
        float offsetY = Random.Range(-spawnerOffset, spawnerOffset);
        int spawnIndex = Random.Range(0, fruitSpawners.Length - 1);
        Transform spawner = fruitSpawners[spawnIndex];
        return new Vector3(spawner.position.x + offsetX, spawner.position.y + offsetY, spawner.position.z);
    }

    private void FruitRotate(GameObject fruit)
    {
        float rx, ry, rz;
        rx = Random.Range(-1f, 1f);
        ry = Random.Range(-1f, 1f);
        rz = Random.Range(-1f, 1f);
        fruit.GetComponent<Rigidbody>().AddTorque(new Vector3(rx, ry, rz) * 2f);
    }

    public IEnumerator SpawnFruitCoroutine()
    {
        while (true)
        {
            float spawnTime = Random.Range(0.24f, 0.5f);
            if (womboCombo)
            {
                spawnTime /= 10f;
            }
            SpawnFruit();
            yield return new WaitForSeconds(spawnTime);
        }
    }
}