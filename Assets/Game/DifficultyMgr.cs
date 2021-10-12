using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DifficultyMgr : MonoBehaviour
{
    public int CurDifficultyStage = 0;

    [System.Serializable]
    public class DifficultyStage
    {
        public int Score;
        public float SpawnTime;
        public int Powerups;
    }

    public List<DifficultyStage> DifficultyStages = new List<DifficultyStage>();

    void Update()
    {
        CurDifficultyStage = 0;

        foreach (DifficultyStage stage in DifficultyStages)
        {
            if(stage.Score <= ScoreAndLivesUI.Instance.currentScore)
            {
                SpawnMgr.Instance.SpawnTime = stage.SpawnTime;
                PowerUpSpawnMgr.instance.numberOfPowerUpsPerCar = stage.Powerups;
                CurDifficultyStage++;
            }
        }
    }
}
