using System;
using UnityEngine;

public class SamuraiSwiper : MonoBehaviour
{
    public bool isSwiping;


    public Color trailColor = new Color(0.91f, 0.24f, 0.38f);
    public float distanceFromCamera = 5;
    public float startWidth = 0.1f;
    public float endWidth = 0f;
    public float trailTime = 0.24f;

    [Header("Swipe overuse prevention")]
    private bool hasTimeout;
    public float swipeTimer;
    public float swipeTimeoutTime;

    Transform trailTransform;
    Camera thisCamera;

    private void Start()
    {
        isSwiping = false;
        hasTimeout = false;
        SetupTrail();

        GameEvents.FruitEvent += OnFruitEvent;
    }

    private void OnDisable()
    {
        GameEvents.FruitEvent -= OnFruitEvent;
    }

    private void OnFruitEvent(object sender, FruitEventArgs e)
    {
        if (e.EventType == FruitEventType.Cut)
        {
            swipeTimer = 0f;
        }
    }

    private void SetupTrail()
    {
        thisCamera = Camera.main;

        GameObject trailObj = new GameObject("Trail");
        trailTransform = trailObj.transform;
        TrailRenderer trail = trailObj.AddComponent<TrailRenderer>();
        trail.time = -0.1f;
        MoveTrailToCursor(Input.mousePosition);
        trail.time = trailTime;
        trail.startWidth = startWidth;
        trail.endWidth = endWidth;
        trail.numCapVertices = 2;
        trail.sharedMaterial = new Material(Shader.Find("Unlit/Color"));
        trail.sharedMaterial.color = trailColor;
    }

    void MoveTrailToCursor(Vector3 screenPosition)
    {
        trailTransform.position = thisCamera.ScreenToWorldPoint(new Vector3(screenPosition.x, screenPosition.y, distanceFromCamera));
    }

    void Update()
    {
        HandleSwipeToggle();
        HandleSwipe();
        HandleSwipeOveruse();
    }

    private void HandleSwipe()
    {
        if (isSwiping && !hasTimeout)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit))
            {
                Transform objectHit = hit.transform;
                // Do something with the object that was hit by the raycast.
                Debug.Log(objectHit.name);
                ICutable cutable = hit.transform.GetComponent<ICutable>();
                cutable?.Cut();
            }
            MoveTrailToCursor(Input.mousePosition);
        }
    }
    private void HandleSwipeOveruse()
    {
        if (isSwiping)
        {
            swipeTimer += Time.deltaTime;

            if (swipeTimer > swipeTimeoutTime)
            {
                hasTimeout = true;
                swipeTimer = 0;
            }
        }
    }

    private void HandleSwipeToggle()
    {
        if (Input.GetMouseButtonDown(0))
        {
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            isSwiping = false;
            hasTimeout = false;
            swipeTimer = 0;
        }
    }
}