using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public PowerUpSpawnMgr powerUpManager;
    public Light powerUpLight;
    private Material lightMaterial;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.SetActive(false);
        this.powerUpManager = FindObjectOfType<PowerUpSpawnMgr>();
        Debug.Log("Mgr" + powerUpManager);
        powerUpManager.RegisterPowerUp(this);
        lightMaterial = this.GetComponentInChildren<MeshRenderer>().material;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void Collect()
    {
        powerUpManager.DeactivatePowerUp(this);
        this.gameObject.SetActive(false);
        ScoreAndLivesUI.Instance.AddScore();
    }

    public void Deactivate()
    {
        this.gameObject.SetActive(false);
        powerUpManager.DeactivatePowerUp(this);
    }

    private void OnDestroy()
    {
        
    }

    public PowerUp SpawnPowerUp(Color color)
    {
        powerUpLight.color = color;
        lightMaterial.color = color;
        lightMaterial.SetColor("_EmissionColor", color);
        this.gameObject.SetActive(true);
        return this;
    }
}
