using UnityEngine;

public class LineDrawing : MonoBehaviour
{
    [Header("Car Selection")]
    [SerializeField] private LayerMask _carLayer = 1;

    [Header("Line Drawing")]
    [SerializeField] private CarController _carController = null;
    [SerializeField] private float _minDistance = 0.15f;
    private Vector3 _lastPosition = Vector3.zero;

    void Update()
    {
        SelectCar();
        DrawLineOnMouseDrag();
    }

    private Ray CurrentMouseRay => Camera.main.ScreenPointToRay(Input.mousePosition);


    private void SelectCar()
    {
        if (Input.GetMouseButtonDown(0)) // mouse button pressed during this frame
        {
            RaycastHit hit;
            if (Physics.Raycast(CurrentMouseRay, out hit, _carLayer.value))
            {
                _carController = hit.collider.transform.parent.GetComponent<CarController>();
                _carController.ClearPositions();
                _lastPosition = _carController.transform.position;
            }
            else
            {
                _carController = null;
                _lastPosition = Vector3.zero;
            }
        }
        else if (Input.GetMouseButtonUp(0))
        {
            _carController = null;
            _lastPosition = Vector3.zero;
        }
    }

    private void DrawLineOnMouseDrag()
    {
        if (_carController != null && Input.GetMouseButton(0))
        {
            RaycastHit hit;
            if (Physics.Raycast(CurrentMouseRay, out hit))
            {
                var newPosition = hit.point;
                newPosition.y = 0.05f;
                if (Vector3.Distance(newPosition, _lastPosition) > _minDistance)
                {
                    _carController.AppendPosition(newPosition);
                    _lastPosition = newPosition;
                }
            }
        }
    }
}
