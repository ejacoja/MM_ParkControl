using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    [SerializeField] private LineRenderer _lr;
    [SerializeField] private Transform _carTransform;

    public Collider crashCollider;

    public List<PowerUp> listOfPowerUps = new List<PowerUp>();
    public Color carColor;

    public AudioSource PowerUpAudio;
    public AudioSource ExitAudio;

    public CarSpawner exitSpawner { get; private set; }

    public Transform CarTransform { get { return this._carTransform; } }

    [SerializeField] private float moveTime = 0.5f;
    private List<Vector3> points = new List<Vector3>(100);

    private bool currentlyMoving = false;
    private Coroutine _coroutine = null;

    public bool hasCrashed = false;

    public bool isSelected = false;
    private bool hasAllPowerUpsCollected;

    public void AppendPosition(Vector3 newPosition)
    {
        this.points.Add(newPosition);
        TryToMoveToNextTarget();
    }

    public void ClearPositions()
    {
        if (_coroutine != null) StopCoroutine(this._coroutine);
        this.points.Clear();
        this.currentlyMoving = false;
    }

    private void Start()
    {
        TryToMoveToNextTarget();
    }

    private void TryToMoveToNextTarget()
    {
        if (this.currentlyMoving) return;

        if (this.points.Count >= 1)
        {
            var targetPos = this.points[0];
            this.points.RemoveAt(0);
            this._coroutine = StartCoroutine(MovetToTarget(targetPos));
        }
    }

    private IEnumerator MovetToTarget(Vector3 targetPosition)
    {
        this.currentlyMoving = true;
        var startPos = this._carTransform.position;
        var targetRotation = Quaternion.LookRotation((targetPosition - this._carTransform.position).normalized, Vector3.up);
        var startRotation = this._carTransform.transform.rotation;

        float distance = Vector3.Distance(targetPosition, startPos);
        float currentMoveTime = distance / this.moveTime;

        float a = 0f;
        while (a <= currentMoveTime)
        {
            a += Time.deltaTime;
            this._carTransform.rotation = Quaternion.Lerp(startRotation, targetRotation, a / currentMoveTime);
            this._carTransform.position = Vector3.Lerp(startPos, targetPosition, a / currentMoveTime);
            yield return new WaitForEndOfFrame();
        }

        this._carTransform.position = targetPosition;
        this._carTransform.rotation = targetRotation;


        this.currentlyMoving = false;

        TryToMoveToNextTarget();
    }

    internal void Crash()
    {
        foreach(PowerUp powerUp in listOfPowerUps)
        {
            powerUp.Deactivate();
        }

        if (exitSpawner) SpawnMgr.Instance.ReturnCarSpawner(exitSpawner);

        hasCrashed = true;
    }

    internal void CollectPowerUp(PowerUp powerUp)
    {
        PowerUpAudio.Play();

        this.listOfPowerUps.Remove(powerUp);
        hasAllPowerUpsCollected = this.listOfPowerUps.Count <= 0;
        
        powerUp.Collect();

        if (hasAllPowerUpsCollected)
        {
            exitSpawner = SpawnMgr.Instance.GetRandomCarSpawner();
            exitSpawner.ShowIn(carColor);
        }
    }

    internal void ArrivedAtExit(CarSpawner exit)
    {
        Instantiate(ExitAudio);
        SpawnMgr.Instance.ReturnCarSpawner(exitSpawner);
        ScoreAndLivesUI.Instance.AddScore();
        Destroy(gameObject);
    }

    private void LateUpdate()
    {
        if (this.hasCrashed)
        {
            if (this._coroutine != null)
            {
                StopCoroutine(this._coroutine);
                this._coroutine = null;
            }
            return;
        }

        // update line renderer points


        // position 0 for line renderer is current car position
        if (this._lr.positionCount + 1 == this.points.Count)
        {
            this._lr.SetPosition(0, this._carTransform.position);
        }
        else
        {
            List<Vector3> positions = new List<Vector3>(101);
            positions.Add(this._carTransform.position);
            positions.AddRange(this.points);
            this._lr.positionCount = positions.Count;
            this._lr.SetPositions(positions.ToArray());
        }

        if (this.currentlyMoving == false && !isSelected)
        {
            this._carTransform.position += this._carTransform.forward * this.moveTime * Time.deltaTime;
        }
    }
}
