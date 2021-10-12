using UnityEngine;
using UnityEngine.Events;

public class CheckCollision : MonoBehaviour
{
    public float collisionRadius = 1.0f;
    public LayerMask collisionLayer;
    private CarController carController = null;

    public string excludeCollisionTag = "Street";
    public string powerUpTag = "PowerUP";

    private LineRenderer lineRenderer = null;
    public GameObject crashIndicator = null;

    public UnityEvent OnCrash;

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
                Debug.Log(collider.gameObject.name);
                Debug.Log(collider.tag);
                if (collider.tag == powerUpTag)
                {
                    Debug.Log("Collide with PowerUp");
                    PowerUp powerUp = collider.transform.parent.GetComponent<PowerUp>();

                    if(this.carController.listOfPowerUps.Contains(powerUp))
                    {
                        this.carController.CollectPowerUp(powerUp);
                    }
                    
                }
                else
                {
                    ActivateCarCrash();
                }
                
            }
        }

        void ActivateCarCrash()
        {
            OnCrash?.Invoke();
            carController.hasCrashed = true;

            this.carController.DeleteRelatedPowerUps();

            //carController.ClearPositions();
            //this.lineRenderer.SetPositions(new Vector3[0]);
            //this.lineRenderer.enabled = false;

            //// inform car controller
            //this.carController.hasCrashed = true;

            //// show new status
            //this.crashIndicator.SetActive(true);
        }
    }
}
