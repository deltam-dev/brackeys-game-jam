using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tentacles
{
    public class Cord : MonoBehaviour
    {
        [SerializeField] private int length;
        [SerializeField] private Vector3[] segments;
        [SerializeField] private Vector3[] segmentsVelocity;

        [SerializeField] private Transform targetDir;
        [SerializeField] private Transform pivot;
        [SerializeField] private float targetDistance;
        [SerializeField] private float smoothSpeed;
        [SerializeField] private float trailSpeed;
        [SerializeField] private float currentSmoothSpeed = 0;
        [SerializeField] private float currentTrailSpeed = 0;
        [SerializeField] private float currentTargetDistance = 0;

        private LineRenderer lineRenderer;

        private void Awake()
        {
            TryGetComponent(out lineRenderer);
        }

        void Start()
        {
            int linePoints = Mathf.RoundToInt(length / targetDistance);
            lineRenderer.positionCount = linePoints;
            segments = new Vector3[linePoints];
            segmentsVelocity = new Vector3[linePoints];
        }

        void Update()
        {
            currentSmoothSpeed = smoothSpeed * (Math.Clamp(Math.Abs(transform.position.y), 0, length) / length);
            currentTrailSpeed = trailSpeed * (Math.Clamp(Math.Abs(transform.position.y), 0, length) / length);
            currentTargetDistance = targetDistance * (Math.Clamp(Math.Abs(transform.position.y), 0, length) / length);

            currentSmoothSpeed = GameState.Instance.IsOnSurface ? 0 : currentSmoothSpeed;
            currentTrailSpeed = GameState.Instance.IsOnSurface ? 0 : currentTrailSpeed;
            currentTargetDistance = GameState.Instance.IsOnSurface ? 0 : currentTargetDistance;
            
            Debug.Log(lineRenderer.colorGradient.colorKeys[0].color);
            float size = length * Math.Abs(transform.position.y / length);

            segments[0] = targetDir.position;
            segments[segments.Length - 1] = new Vector3(0, targetDir.position.y + size);
            for (int i = 1; i < segments.Length - 1; i++)
            {
                float lineSpeed = currentSmoothSpeed + (currentTrailSpeed != 0 ? i / currentTrailSpeed : 0);
                segments[i] = Vector3.SmoothDamp(segments[i], segments[i - 1] + targetDir.up * currentTargetDistance, ref segmentsVelocity[i], lineSpeed);
            }
            lineRenderer.SetPositions(segments);
        }
    }

}