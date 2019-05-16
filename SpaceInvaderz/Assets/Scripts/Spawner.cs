using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    [Header("Spawn Information")]
    [SerializeField] private SpawnDetails[] allEnemies;
    [SerializeField] private Transform SpawnPointsContainer;

    private List<int> spawnList;
    private Vector3[] spawnPoints;
    private bool onCooldown;

    private void Awake() {
        InitialiseSpawner();
    }

    private void Update() {
        if (spawnList.Count>0) {
            Spawn();
        } else {
            Debug.Log("All enemies have spawned");
        }
    }

    private void InitialiseSpawner() {
        spawnList = new List<int>();
        spawnPoints = new Vector3[SpawnPointsContainer.childCount];

        for (int x=0; x<allEnemies.Length; x++) {
            spawnList.Add(x);
        }

        for (int x=0; x<SpawnPointsContainer.childCount; x++) {
            spawnPoints[x] = SpawnPointsContainer.GetChild(x).position;
        }

    }
    private void Spawn() {
        if (!onCooldown) {
            int index = Random.Range(0, allEnemies.Length);
            SpawnEnemy(spawnList[index]);
            StartCoroutine(Cooldown(2));
        }
    }

    private void SpawnEnemy(int index) {
        int spawnIndex = Random.Range(0, spawnPoints.Length);

        Instantiate(allEnemies[index].prefab, spawnPoints[spawnIndex], Quaternion.identity);
        allEnemies[index].amount--;

        if (allEnemies[index].amount<=0) {
            spawnList.Remove(index);
        }
    }
    private IEnumerator Cooldown(float time) {
        float elapsedTime = 0;
        onCooldown = true;

        while (elapsedTime < time) {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        onCooldown = false;
    }
}

[System.Serializable]
public struct SpawnDetails {
    public GameObject prefab;
    public int amount;

    public string Name { get { return prefab.name; } }
}