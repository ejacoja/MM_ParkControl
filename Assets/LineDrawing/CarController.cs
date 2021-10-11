using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform _carTransform;

    [SerializeField] private float moveTime = 0.5f;

    List<Vector3> points = new List<Vector3>(100);

    private bool currentlyMoving = false;
    private Coroutine _coroutine = null;
    public void AppendPosition(Vector3 newPosition)
    {
        points.Add(newPosition);
        TryToMoveToNextTarget();
    }

    public void ClearPositions()
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        points.Clear();
        currentlyMoving = false;
    }

    private void Start()
    {
        TryToMoveToNextTarget();
    }

    private void TryToMoveToNextTarget()
    {
        if (currentlyMoving) return;

        if (points.Count >= 1)
        {
            var targetPos = points[0];
            points.RemoveAt(0);
            _coroutine = StartCoroutine(MovetToTarget(targetPos));
        }
    }

    private IEnumerator MovetToTarget(Vector3 targetPosition)
    {
        currentlyMoving = true;

        var startPos = _carTransform.position;
        var targetRotation = Quaternion.LookRotation((targetPosition - _carTransform.position).normalized, Vector3.up);
        var startRotation = _carTransform.transform.rotation;
        float a = 0f;
        while(a <= moveTime)
        {
            a += Time.deltaTime;
            _carTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, a / moveTime);
            _carTransform.position = Vector3.Lerp(startPos, targetPosition, a / moveTime);
            yield return new WaitForEndOfFrame();
        }

        _carTransform.position = targetPosition;
        _carTransform.rotation = targetRotation;


        currentlyMoving = false;

        TryToMoveToNextTarget();
    }

    private void LateUpdate()
    {
        // update line renderer points


        // position 0 for line renderer is current car position
        if (_lr.positionCount + 1 == points.Count)
        {
            _lr.SetPosition(0, _carTransform.position);
        }
        else
        {
            List<Vector3> positions = new List<Vector3>(101);
            positions.Add(_carTransform.position);
            positions.AddRange(points);
            _lr.positionCount = positions.Count;
            _lr.SetPositions(positions.ToArray());
        }
    }
}
