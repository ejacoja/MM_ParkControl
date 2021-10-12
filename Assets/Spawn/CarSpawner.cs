using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarSpawner : MonoBehaviour
{
    public GameObject InArrows, OutArrows;

    public List<MeshRenderer> InArrowMeshRenderer, OutArrowMeshRenderer;
    public List<Light> InArrowLights, OutArrowLights;
    public void ShowIn(Color color)
    {
        OutArrows.SetActive(false);
        InArrows.SetActive(true);

        InArrows.GetComponent<Animator>().playbackTime = 0;

        foreach (MeshRenderer mr in InArrowMeshRenderer)
        {
            mr.material.color = color;
        }

        foreach (Light l in InArrowLights)
        {
            l.color = color;
        }
    }
    public void ShowOut(Color color)
    {
        InArrows.SetActive(false);
        OutArrows.SetActive(true);

        OutArrows.GetComponent<Animator>().playbackTime = 0;

        foreach (MeshRenderer mr in OutArrowMeshRenderer)
        {
            mr.material.color = color;
        }

        foreach (Light l in OutArrowLights)
        {
            l.color = color;
        }
    }

    public void ShowNone()
    {
        InArrows.SetActive(false);
        OutArrows.SetActive(false);
    }
}
