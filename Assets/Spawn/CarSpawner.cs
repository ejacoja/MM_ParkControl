using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject InArrows, OutArrows;

    public void ShowIn(Color color)
    {
        OutArrows.SetActive(false);
        InArrows.SetActive(true);

        foreach (MeshRenderer mr in InArrows.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = color;
        }
    }
    public void ShowOut(Color color)
    {
        InArrows.SetActive(false);
        OutArrows.SetActive(true);

        foreach (MeshRenderer mr in OutArrows.GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.color = color;
        }
    }

    public void ShowNone()
    {
        InArrows.SetActive(false);
        OutArrows.SetActive(false);
    }
}
