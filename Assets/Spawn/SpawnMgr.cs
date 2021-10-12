using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SpawnMgr : MonoBehaviour
{
    public static SpawnMgr Instance { get; private set; }

    public List<Color> CarColors = new List<Color>();
    public List<CarSpawner> CarSpawnPoints = new List<CarSpawner>();
    private List<CarSpawner> CarSpawnPointsInUse = new List<CarSpawner>();
    public List<GameObject> CarPrefabs;
    public PowerUpSpawnMgr powerUpManager;
    public float SpawnTime = 5f, ArrowWarnTime = 2f;

    private int colorIndex = 0;

    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        StartCoroutine(SpawnCarsAtRandom());
    }

    public CarSpawner GetRandomCarSpawner()
    {
        var index = Random.Range(0, CarSpawnPoints.Count);
        CarSpawner spawner = CarSpawnPoints[index];

        CarSpawnPointsInUse.Add(spawner);
        CarSpawnPoints.RemoveAt(index);

        return spawner;
    }

    public void ReturnCarSpawner(CarSpawner spawner)
    {
        if (CarSpawnPointsInUse.Remove(spawner))
        {
            CarSpawnPoints.Add(spawner);
            spawner.ShowNone();
        }
    }

    IEnumerator SpawnCarsAtRandom()
    {
        while(true)
        {
            int curCarIndex = colorIndex % CarColors.Count;

            CarSpawner spawner = GetRandomCarSpawner();
            Color color = CarColors[curCarIndex];
            spawner.ShowOut(color);

            yield return new WaitForSeconds(ArrowWarnTime);

            GameObject carInstance = Instantiate<GameObject>(CarPrefabs[curCarIndex], spawner.transform.position, spawner.transform.rotation, null);
            var carController = carInstance.GetComponent<CarController>();

            List<PowerUp> powerUpList = powerUpManager.SpawnRandomPowerUp(color);
            carController.listOfPowerUps = powerUpList;
            carController.carColor = color;

            colorIndex++;

            yield return new WaitForSeconds(0.5f);
            ReturnCarSpawner(spawner);

            //wait for no cars or Spawn time up before spawning new car
            
            float SpawnCounter = 0f;

            while (SpawnCounter < SpawnTime)
            {
                int activeCarCount = FindObjectsOfType<CarController>().Where(car => car.hasCrashed == false).Count();
                if (activeCarCount <= 0) break;

                yield return new WaitForSeconds(0.5f);
                SpawnCounter += 0.5f;
            }
        }
    }
}
