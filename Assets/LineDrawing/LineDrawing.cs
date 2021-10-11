using UnityEngine;

public class LineDrawing : MonoBehaviour
{
    [Header("Car Selection")]
    [SerializeField] private LayerMask _carLayer = 1;

    [Header("Line Drawing")]
    [SerializeField] private CarController _carController = null;
    [SerializeField] private float _minDistance = 0.15f;
    private Vector3 _lastPosition = Vector3.zero;
    public Camera camera = null;

    private void Update()
    {
        SelectCar();
        DrawLineOnMouseDrag();
    }

    private Ray CurrentMouseRay => camera.ScreenPointToRay(Input.mousePosition);


    private void SelectCar()
    {
        if (Input.GetMouseButtonDown(0)) // mouse button pressed during this frame
        {
            RaycastHit hit;
            if (Physics.Raycast(CurrentMouseRay, out hit, this._carLayer.value))
            {
                this._carController = hit.collider.transform.parent.GetComponent<CarController>();
                this._carController.ClearPositions();
                this._lastPosition = this._carController.transform.position;
            }
            else
            {
                this._carController = null;
                this._lastPosition = Vector3.zero;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            this._carController = null;
            this._lastPosition = Vector3.zero;
        }
    }

    private void DrawLineOnMouseDrag()
    {
        if (this._carController != null && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(CurrentMouseRay, out hit))
            {
                var newPosition = hit.point;
                newPosition.y = 0.05f;
                if (Vector3.Distance(newPosition, this._lastPosition) > this._minDistance)
                {
                    this._carController.AppendPosition(newPosition);
                    this._lastPosition = newPosition;
                }
            }
        }
    }
}
