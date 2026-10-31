using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
    public GameObject prefab;

    public float startTime, endTime;

    public float spawnRate;

    void Start()
    {
        WaveManager.SharedInstance.AddWave(this);
        InvokeRepeating("SpawnEnemy", startTime, spawnRate);
        Invoke("EndWave", endTime);
    }

    void SpawnEnemy()
    {
        Instantiate(prefab, transform.position, transform.rotation);
    }

    void EndWave()
    {
        WaveManager.SharedInstance.RemoveWave(this);
        CancelInvoke();
    }
}