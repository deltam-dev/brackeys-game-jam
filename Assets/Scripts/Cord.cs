using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Tentacles
{
    public class Cord : MonoBehaviour
    {
        [SerializeField] private LineRenderer lineRenderer;
        [SerializeField] private int length;
        [SerializeField] private Vector3[] segments;
        [SerializeField] private Vector3[] segmentsVelocity;

        public Transform targetDir;
        public float targetDistance;
        public float smoothSpeed;
        public float trailSpeed;

        // Start is called before the first frame update
        void Start()
        {
            lineRenderer.positionCount = length;
            segments = new Vector3[length];
            segmentsVelocity = new Vector3[length];
        }

        // Update is called once per frame
        void Update()
        {
            segments[0] = targetDir.position;
            for (int i = 1; i < segments.Length; i++)
            {
                segments[i] = Vector3.SmoothDamp(segments[i], segments[i - 1] + targetDir.up * targetDistance, ref segmentsVelocity[i], smoothSpeed + i / trailSpeed);
            }
            lineRenderer.SetPositions(segments);
        }
    }

}