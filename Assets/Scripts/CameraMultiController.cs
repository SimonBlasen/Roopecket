using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;

public class CameraMultiController : MonoBehaviour
{
    [SerializeField]
    public Transform[] rockets;
    [SerializeField]
    public Rigidbody rocketRigidbody;
    [SerializeField]
    private Vector3 offsetVector = new Vector3(0f, 0f, -1f);
    [SerializeField]
    private float minDistance = 10f;
    [SerializeField]
    private float distanceFactor = 1f;
    [SerializeField]
    private float lerpSpeed = 0.1f;
    [SerializeField]
    private float camScaleFactor = 2f;
    [SerializeField]
    private PostProcessingBehaviour postProc;
    [SerializeField]
    private float lerpSpeedChild = 0.04f;

    private Transform childTransform = null;
    private Vector3 childLocalOffset = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        postProc = GetComponentInChildren<PostProcessingBehaviour>();
        childTransform = GetComponentInChildren<Camera>().transform;
        childLocalOffset = childTransform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LerpChild)
        {
            childTransform.localPosition = Vector3.Lerp(childTransform.localPosition, childLocalOffset, lerpSpeedChild);
        }


        Vector3 mid = Vector3.zero;

        Vector3 minBB = rockets[0].position;
        Vector3 maxBB = rockets[0].position;

        for (int i = 0; i < rockets.Length; i++)
        {
            if (rockets[i].position.x < minBB.x)
            {
                minBB.x = rockets[i].position.x;
            }
            if (rockets[i].position.y < minBB.y)
            {
                minBB.y = rockets[i].position.y;
            }
            if (rockets[i].position.z < minBB.z)
            {
                minBB.z = rockets[i].position.z;
            }

            if (rockets[i].position.x > maxBB.x)
            {
                maxBB.x = rockets[i].position.x;
            }
            if (rockets[i].position.y > maxBB.y)
            {
                maxBB.y = rockets[i].position.y;
            }
            if (rockets[i].position.z > maxBB.z)
            {
                maxBB.z = rockets[i].position.z;
            }
            mid += rockets[i].position;
        }
        mid /= rockets.Length;
        float maxDist = Vector3.Distance(minBB, maxBB) * camScaleFactor;
        if (maxDist < minDistance)
        {
            maxDist = minDistance;
        }

        Vector3 vecUp = Vector3.up;
        Vector3 vecSide = Vector3.Cross(vecUp, offsetVector);

        float distance = (maxDist + (IsRocketDead ? 20f : (rocketRigidbody != null ? rocketRigidbody.velocity.magnitude * distanceFactor : 0f)));
        Vector3 goalPos = mid + offsetVector.normalized * distance;

        RaycastHit hit;
        if (false && Physics.Raycast(new Ray(goalPos, childTransform.forward), out hit, distance - 2f))
        {
            goalPos = hit.point + childTransform.forward * 0.1f;
            distance = Vector3.Distance(mid, goalPos);
        }
        transform.position = Vector3.Lerp(transform.position, goalPos, lerpSpeed);
        transform.LookAt(transform.position + offsetVector * -1f);

        DepthOfFieldModel.Settings set = postProc.profile.depthOfField.settings;

        set.focusDistance = distance;

        postProc.profile.depthOfField.settings = set;
    }

    public bool IsRocketDead
    {
        get;set;
    }

    public Vector3 OffsetVector
    {
        get
        {
            return offsetVector;
        }
        set
        {
            offsetVector = value;
        }
    }

    public bool LerpChild
    {
        get;set;
    }
}
