using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMgr : MonoBehaviour
{
    public List<Color> CarColors = new List<Color>();
    public List<CarSpawner> CarSpawnPoints = new List<CarSpawner>();
    public List<GameObject> CarPrefabs;
    public PowerUpSpawnMgr powerUpManager;
    public float SpawnTime = 5f, ArrowWarnTime = 2f;

    private int colorIndex = 0;

    void Start()
    {
        StartCoroutine(SpawnCarsAtRandom());
    }

    IEnumerator SpawnCarsAtRandom()
    {
        while(true)
        {
            int curCarIndex = colorIndex % CarColors.Count;

            CarSpawner spawner = CarSpawnPoints[Random.Range(0, CarSpawnPoints.Count)];
            spawner.ShowOut(CarColors[curCarIndex]);

            yield return new WaitForSeconds(ArrowWarnTime);

            powerUpManager.SpawnRandomPowerUp(CarColors[curCarIndex]);
            Instantiate<GameObject>(CarPrefabs[curCarIndex], spawner.transform.position, spawner.transform.rotation, null);
            colorIndex++;

            yield return new WaitForSeconds(0.5f);
            spawner.ShowNone();

            yield return new WaitForSeconds(SpawnTime);
        }
    }
}
