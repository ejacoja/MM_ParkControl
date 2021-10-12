using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeleteAfterSeconds : MonoBehaviour
{
    public float DeleteAfterSeconds = 1.0f;

    void Start()
    {
        StartCoroutine(DeleteSelf());
    }

    IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(DeleteAfterSeconds);

        Destroy(gameObject);
    }
}
