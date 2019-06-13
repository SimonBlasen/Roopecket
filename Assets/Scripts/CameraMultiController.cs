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
    [SerializeField]
    private float lerpSpeedProjMatr = 0.04f;
    [SerializeField]
    private float linearSpeedProjMatr = 0.04f;

    public bool debugLog = false;

    private Camera childCamera = null;
    private Transform childTransform = null;
    private Vector3 childLocalOffset = Vector3.zero;

    private RocketSpawner rocketSpawner = null;

    [HideInInspector]
    public Vector2 proj1 = Vector2.zero;
    [HideInInspector]
    public Vector2 proj2 = Vector2.zero;
    public Vector2 proj1Goal = new Vector2(0f, 0f);
    public Vector2 proj2Goal = new Vector2(1f, 1f);
    [HideInInspector]
    public Vector2 vector1GoalOutside = new Vector2(0f, 0f);
    [HideInInspector]
    public Vector2 vector2GoalOutside = new Vector2(1f, 1f);
    
    public bool finishedLerpProj = false;

    private bool lerpBackToTransform = false;
    public float timeLerpBackToTransform = 0f;
    private Transform transformLerpBack = null;

    private float sign1 = 0f;
    private float sign2 = 0f;
    private float sign3 = 0f;
    private float sign4 = 0f;
    private bool done1 = false;
    private bool done2 = false;
    private bool done3 = false;
    private bool done4 = false;

    // Use this for initialization
    void Start()
    {
        rocketSpawner = GameObject.FindObjectOfType<RocketSpawner>();
        postProc = GetComponentInChildren<PostProcessingBehaviour>();
        childTransform = GetComponentInChildren<Camera>().transform;
        childCamera = childTransform.GetComponent<Camera>();
        childLocalOffset = childTransform.localPosition;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (LerpChild)
        {
            timeLerpBackToTransform += Time.deltaTime;
            if (finishedLerpProj == false && sign1 == 0f)
            {
                sign1 = Mathf.Sign((proj1Goal.x - proj1.x));
                sign2 = Mathf.Sign((proj1Goal.y - proj1.y));
                sign3 = Mathf.Sign((proj2Goal.x - proj2.x));
                sign4 = Mathf.Sign((proj2Goal.x - proj2.x));
                done1 = false;
                done2 = false;
                done3 = false;
                done4 = false;
            }

            childTransform.localPosition = Vector3.Lerp(childTransform.localPosition, childLocalOffset, lerpSpeedChild);
            if (finishedLerpProj == false && Vector2.Distance(proj1, proj1Goal) > 0.003f && Vector2.Distance(proj2, proj2Goal) > 0.003f)
            //if (finishedLerpProj == false && (done1 == false || done2 == false || done3 == false || done4 == false))
            {
                proj1 = Vector2.Lerp(proj1, proj1Goal, Mathf.Min(0.5f, (lerpSpeedProjMatr + (timeLerpBackToTransform > 4.3f ? (timeLerpBackToTransform - 4.3f) * 0.4f : 0f))));
                proj2 = Vector2.Lerp(proj2, proj2Goal, Mathf.Min(0.5f, (lerpSpeedProjMatr + (timeLerpBackToTransform > 4.3f ? (timeLerpBackToTransform - 4.3f) * 0.4f : 0f))));
                /*proj1 = new Vector2(proj1.x + (done1 ? 0f : Mathf.Sign((proj1Goal.x - proj1.x)) * linearSpeedProjMatr), proj1.y + (done2 ? 0f : Mathf.Sign((proj1Goal.y - proj1.y)) * linearSpeedProjMatr));
                proj2 = new Vector2(proj2.x + (done3 ? 0f : Mathf.Sign((proj2Goal.x - proj2.x)) * linearSpeedProjMatr), proj2.y + (done4 ? 0f : Mathf.Sign((proj2Goal.y - proj2.y)) * linearSpeedProjMatr));

                if (Mathf.Sign((proj1Goal.x - proj1.x)) != sign1)
                {
                    done1 = true;
                }
                if (Mathf.Sign((proj1Goal.y - proj1.y)) != sign2)
                {
                    done2 = true;
                }
                if (Mathf.Sign((proj2Goal.x - proj2.x)) != sign3)
                {
                    done3 = true;
                }
                if (Mathf.Sign((proj2Goal.y - proj2.y)) != sign4)
                {
                    done4 = true;
                }*/

                CameraMultiController.SetScissorRect(childCamera, new Rect(proj1.x, proj1.y, proj2.x, proj2.y));
            }
            else
            {
                //Debug.Log("Finished moving");
                finishedLerpProj = true;
                CameraMultiController.SetScissorRect(childCamera, new Rect(proj1Goal.x, proj1Goal.y, proj2Goal.x, proj2Goal.y));
                sign1 = 0f;

            }
        }
        else if (lerpBackToTransform)
        {
            timeLerpBackToTransform += Time.deltaTime;
            if (finishedLerpProj == false && sign1 == 0f)
            {
                sign1 = Mathf.Sign((proj1Goal.x - proj1.x));
                sign2 = Mathf.Sign((proj1Goal.y - proj1.y));
                sign3 = Mathf.Sign((proj2Goal.x - proj2.x));
                sign4 = Mathf.Sign((proj2Goal.x - proj2.x));
                done1 = false;
                done2 = false;
                done3 = false;
                done4 = false;
            }

            childTransform.position = Vector3.Lerp(childTransform.position, transformLerpBack.position, Mathf.Min(0.5f, (lerpSpeedChild + (timeLerpBackToTransform > 3f ? (timeLerpBackToTransform - 3f) * 0.1f : 0f))));
            if (Vector3.Distance(childTransform.position, transformLerpBack.position) < 0.1f && finishedLerpProj)
            {
                FinishedLerpingBack = true;
                lerpBackToTransform = false;
            }

            if (finishedLerpProj == false && Vector2.Distance(proj1, vector1GoalOutside) > 0.003f && Vector2.Distance(proj2, vector2GoalOutside) > 0.003f)
            //if (finishedLerpProj == false && (done1 == false || done2 == false || done3 == false || done4 == false))
            {
                proj1 = Vector2.Lerp(proj1, vector1GoalOutside, Mathf.Min(0.5f, (lerpSpeedProjMatr + (timeLerpBackToTransform > 3f ? (timeLerpBackToTransform - 3f) * 0.1f : 0f))));
                proj2 = Vector2.Lerp(proj2, vector2GoalOutside, Mathf.Min(0.5f, (lerpSpeedProjMatr + (timeLerpBackToTransform > 3f ? (timeLerpBackToTransform - 3f) * 0.1f : 0f))));
                /*proj1 = new Vector2(proj1.x + (done1 ? 0f : Mathf.Sign((vector1GoalOutside.x - proj1.x)) * linearSpeedProjMatr), proj1.y + (done2 ? 0f : Mathf.Sign((vector1GoalOutside.y - proj1.y)) * linearSpeedProjMatr));
                proj2 = new Vector2(proj2.x + (done3 ? 0f : Mathf.Sign((vector2GoalOutside.x - proj2.x)) * linearSpeedProjMatr), proj2.y + (done4 ? 0f : Mathf.Sign((vector2GoalOutside.y - proj2.y)) * linearSpeedProjMatr));

                if (Mathf.Sign((vector1GoalOutside.x - proj1.x)) != sign1)
                {
                    done1 = true;
                }
                if (Mathf.Sign((vector1GoalOutside.y - proj1.y)) != sign2)
                {
                    done2 = true;
                }
                if (Mathf.Sign((vector2GoalOutside.x - proj2.x)) != sign3)
                {
                    done3 = true;
                }
                if (Mathf.Sign((vector2GoalOutside.y - proj2.y)) != sign4)
                {
                    done4 = true;
                }*/

                CameraMultiController.SetScissorRect(childCamera, new Rect(proj1.x, proj1.y, proj2.x, proj2.y));
            }
            else
            {
                finishedLerpProj = true;
                CameraMultiController.SetScissorRect(childCamera, new Rect(vector1GoalOutside.x, vector1GoalOutside.y, vector2GoalOutside.x, vector2GoalOutside.y));
                sign1 = 0f;

            }

        }

        if (debugLog)
        {
            Debug.Log(childTransform.localPosition.ToString());
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

        if (rocketSpawner.Spawn2Rockets)
        {
            set.focusDistance = distance;
        }
        else
        {
            set.focusDistance = distance;
        }

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

    public void LerpToTransform(Transform trans)
    {
        timeLerpBackToTransform = 0f;
        proj1 = proj1Goal;
        proj2 = proj2Goal;
        finishedLerpProj = false;
        FinishedLerpingBack = false;
        lerpBackToTransform = true;
        transformLerpBack = trans;
    }

    public bool FinishedLerpingBack
    {
        get; protected set;
    }



    public static void SetScissorRect(Camera cam, Rect r)
    {
        if (r.x < 0)
        {
            r.width += r.x;
            r.x = 0;
        }

        if (r.y < 0)
        {
            r.height += r.y;
            r.y = 0;
        }

        r.width = Mathf.Min(1 - r.x, r.width);
        r.height = Mathf.Min(1 - r.y, r.height);

        //cam.rect = new Rect(0, 0, 1, 1);
        cam.ResetProjectionMatrix();
        Matrix4x4 m = cam.projectionMatrix;
        //cam.rect = r;
        Matrix4x4 m1 = Matrix4x4.TRS(new Vector3(r.x, r.y, 0), Quaternion.identity, new Vector3(r.width, r.height, 1));
        Matrix4x4 m2 = Matrix4x4.TRS(new Vector3((1 / r.width - 1), (1 / r.height - 1), 0), Quaternion.identity, new Vector3(1 / r.width, 1 / r.height, 1));
        Matrix4x4 m3 = Matrix4x4.TRS(new Vector3(-r.x * 2 / r.width, -r.y * 2 / r.height, 0), Quaternion.identity, Vector3.one);
        cam.projectionMatrix = m3 * m2 * m;
    }
}
