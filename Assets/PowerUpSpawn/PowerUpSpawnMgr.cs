using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnMgr : MonoBehaviour
{
    public int numberOfPowerUpsPerCar = 2;

    private List<PowerUp> inactivePowerUps = new List<PowerUp>();
    private List<PowerUp> activePowerUps = new List<PowerUp>();

    public static PowerUpSpawnMgr instance;

    public void Awake()
    {
        instance = this;
    }

    public int ActivatePowerUp(int index)
    {
        activePowerUps.Add(this.inactivePowerUps[index]);
        inactivePowerUps.RemoveAt(index);
        return activePowerUps.Count - 1;
    }

    public void DeactivatePowerUp(PowerUp powerUp)
    {
        for (int index = 0; index < activePowerUps.Count; index++)
        {
            if (activePowerUps[index] == powerUp)
            {
                inactivePowerUps.Add(this.activePowerUps[index]);
                activePowerUps.RemoveAt(index);
                break;
            }
        }
    }

    public List<PowerUp> SpawnRandomPowerUp(Color carColor)
    {
        Debug.Log(inactivePowerUps.Count);
        List<int> randomIndices = new List<int>();
        List<PowerUp> carRelatedPowerUps = new List<PowerUp>();

        for (int count = 0; count < numberOfPowerUpsPerCar; count++)
        {
            int randomIndex = UnityEngine.Random.Range(0, inactivePowerUps.Count - 1);
            randomIndices.Add(randomIndex);
            PowerUp powerUp = inactivePowerUps[randomIndex].SpawnPowerUp(carColor);
            int newIndex = ActivatePowerUp(randomIndex);
            carRelatedPowerUps.Add(powerUp);
        }

        return carRelatedPowerUps;
    }

    public void RegisterPowerUp(PowerUp powerUp)
    {
        this.inactivePowerUps.Add(powerUp);
    }
}
