using UnityEngine;

public class CheckCollision : MonoBehaviour
{
    public float collisionRadius = 1.0f;
    public LayerMask collisionLayer;
    private CarController carController = null;

    public string excludeCollisionTag = "Street";

    private LineRenderer lineRenderer = null;
    public GameObject crashIndicator = null;

    private void Start()
    {
        this.carController = GetComponent<CarController>();
        this.lineRenderer = GetComponentInChildren<LineRenderer>();
    }

    private void Update()
    {
        if (this.carController.hasCrashed) return;

        Collider[] colliders = Physics.OverlapSphere(this.carController.CarTransform.position, this.collisionRadius);
        foreach (var collider in colliders)
        {
            // only if not myself
            if (this.carController.CarTransform.gameObject != collider.gameObject && collider.tag != excludeCollisionTag)
            {
                ActivateCarCrash();
            }
        }

        void ActivateCarCrash()
        {
            this.lineRenderer.SetPositions(new Vector3[0]);
            this.lineRenderer.enabled = false;

            // inform car controller
            this.carController.hasCrashed = true;

            // show new status
            this.crashIndicator.SetActive(true);
        }
    }
}
