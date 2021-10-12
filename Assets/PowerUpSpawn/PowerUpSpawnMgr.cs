using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpSpawnMgr : MonoBehaviour
{
    private List<PowerUp> inactivePowerUps = new List<PowerUp>();
    private List<PowerUp> activePowerUps = new List<PowerUp>();

    public void ActivatePowerUp(int index)
    {
        activePowerUps.Add(this.inactivePowerUps[index]);
        inactivePowerUps.RemoveAt(index);
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

    public void SpawnRandomPowerUp(Color carColor)
    {
        Debug.Log(inactivePowerUps.Count);
        int randomIndex = -1;

        randomIndex = UnityEngine.Random.Range(0, inactivePowerUps.Count - 1);

        inactivePowerUps[randomIndex].SpawnPowerUp(carColor);

        ActivatePowerUp(randomIndex);
    }

    public void RegisterPowerUp(PowerUp powerUp)
    {
        this.inactivePowerUps.Add(powerUp);
    }
}
