using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpSpawnMgr powerUpManager;
    public Light powerUpLight;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        this.powerUpManager = FindObjectOfType<PowerUpSpawnMgr>();
        Debug.Log("Mgr" + powerUpManager);
        powerUpManager.RegisterPowerUp(this);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Collect()
    {
        powerUpManager.DeactivatePowerUp(this);
        this.gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        
    }

    public void SpawnPowerUp(Color color)
    {
        powerUpLight.color = color;
        this.gameObject.SetActive(true);
    }
}
