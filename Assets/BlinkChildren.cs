using System.Collections;
using UnityEngine;

public class BlinkChildren : MonoBehaviour
{

    public float blinkTime = 1f;
    private Coroutine coroutine = null;

    private void Start()
    {
        this.coroutine = StartCoroutine(ToggleChildren());
    }

    // Update is called once per frame
    private IEnumerator ToggleChildren()
    {
        while(true)
        {
            yield return new WaitForSeconds(this.blinkTime);
            Transform[] children = GetComponentsInChildren<Transform>(true);
            foreach (var child in children)
            {
                if (child.gameObject != this.gameObject)
                {
                    child.gameObject.SetActive(!child.gameObject.activeInHierarchy);
                }
            }
        }
    }

    private void OnDestroy()
    {
        if (this.coroutine != null) StopCoroutine(this.coroutine);
    }
}
