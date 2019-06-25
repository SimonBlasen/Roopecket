using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketController : MonoBehaviour {

    [SerializeField]
    protected KeyCode[] keyCodes;
    [SerializeField]
    protected KeyCode keyLander;
    [SerializeField]
    protected Transform[] thrustPositions;
    [SerializeField]
    protected ParticleSystem[] thrustParticles;
    [SerializeField]
    protected AudioSource[] thrusterAudioSrcs;
    [SerializeField]
    protected Light[] thrustLights;
    [SerializeField]
    protected float thrustStrength = 10.0f;
    [SerializeField]
    protected float[] thrustStrengthes;
    [SerializeField]
    protected Transform midPoint;
    [SerializeField]
    protected RocketProps rocketProps;
    [SerializeField]
    protected LanderMover[] landerMovers;
    [SerializeField]
    protected float thrusterAudioIncr = 0.1f;
    [SerializeField]
    public float rocketVolume = 1f;

    protected bool[] thrusts = null;

    protected bool[] audioOn = null;

    protected Rigidbody ownRig;

    // Use this for initialization
    protected void Start ()
    {
        Init();
	}

    protected void Update()
    {
        Vector3 localVelocity = transform.InverseTransformDirection(ownRig.angularVelocity);
        localVelocity.x = 0f;
        localVelocity.y = 0f;

        ownRig.angularVelocity = transform.TransformDirection(localVelocity);


        for (int i = 0; i < thrusterAudioSrcs.Length; i++)
        {
            if (audioOn[i] && thrusterAudioSrcs[i].volume < rocketVolume)
            {
                thrusterAudioSrcs[i].volume += thrusterAudioIncr * Time.deltaTime;
                if (thrusterAudioSrcs[i].volume > rocketVolume)
                {
                    thrusterAudioSrcs[i].volume = rocketVolume;
                }
            }
            else if (audioOn[i] == false && thrusterAudioSrcs[i].volume > 0f)
            {
                thrusterAudioSrcs[i].volume -= thrusterAudioIncr * Time.deltaTime;
                if (thrusterAudioSrcs[i].volume < 0f)
                {
                    thrusterAudioSrcs[i].volume = 0f;
                    thrusterAudioSrcs[i].Stop();
                }
            }
        }

        if (Input.GetKeyDown(keyLander))
        {
            //Debug.Log("KeyDown");
            for (int i = 0; i < landerMovers.Length; i++)
            {
                landerMovers[i].TurnOut = !landerMovers[i].TurnOut;
            }

            // TODO for multiplayer, must be set by message
            rocketProps.Indestroyable = false;
        }
    }


    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (rocketProps.OutOfFuel == false)
        {
            for (int i = 0; i < thrustPositions.Length; i++)
            {
                if (thrusts[i])
                {
                    ownRig.AddForceAtPosition(thrustPositions[i].up * thrustStrengthes[i], thrustPositions[i].position);
                }
            }

            for (int i = 0; i < keyCodes.Length; i++)
            {
                if (Input.GetKey(keyCodes[i]))
                {
                    SetThrust(i, true);
                }
                else
                {
                    SetThrust(i, false);
                }
            }
        }
        else
        {
            for (int i = 0; i < keyCodes.Length; i++)
            {
                SetThrust(i, false);
            }
        }

        


        if (!Turning)
        {
            float angleAbs = Vector3.Angle((new Vector3(0f, 0f, 1f)), transform.forward);
            if (Vector3.Angle((new Vector3(1f, 0f, 0f)), transform.forward) > 90f)
            {
                angleAbs = 360f - angleAbs;
            }

            angleAbs = angleAbs / 90f;
            while (angleAbs < 0f)
            {
                angleAbs += 4f;
            }
            angleAbs += 0.5f;
            angleAbs = ((int)angleAbs);
            angleAbs *= 90f;

            bool zNull = false;
            if ((angleAbs >= 45f && angleAbs < 135f) || (angleAbs >= 225f && angleAbs < 315f))
            {
                zNull = false;
            }

            transform.rotation = Quaternion.Euler(zNull ? transform.rotation.eulerAngles.x : 0f, angleAbs, zNull ? 0f : transform.rotation.eulerAngles.z);
        }
    }

    protected void Init()
    {
        ownRig = GetComponent<Rigidbody>();
        if (midPoint != null)
        {
            ownRig.centerOfMass = midPoint.localPosition;
        }
        thrusts = new bool[thrustPositions.Length];
        audioOn = new bool[thrusts.Length];
        for (int i = 0; i < thrusts.Length; i++)
        {
            thrusts[i] = false;
        }
        for (int i = 0; i < audioOn.Length; i++)
        {
            audioOn[i] = false;
        }

        if (thrustStrengthes.Length == 0)
        {
            thrustStrengthes = new float[thrusts.Length];
            for (int i = 0; i < thrustStrengthes.Length; i++)
            {
                thrustStrengthes[i] = thrustStrength;
            }
        }

        Normal = new Vector3(0f, 0f, -1f);
        Turning = false;

        Manager.Instance.ActivateManager();
    }

    public KeyCode[] Controls
    {
        get
        {
            return keyCodes;
        }
        set
        {
            keyCodes = value;
        }
    }

    public KeyCode LandingKey
    {
        get
        {
            return keyLander;
        }
        set
        {
            keyLander = value;
        }
    }

    public bool Turning
    {
        get;set;
    }

    public bool LandingMoversOut
    {
        get
        {
            return landerMovers[0].TurnOut;
        }
    }

    private Vector3 gravZone = new Vector3(200f, 200f, 200f);

    public Vector3 GravityZone
    {
        get
        {
            return gravZone;
        }
        set
        {
            gravZone = value;
        }
    }

    public Vector3 Normal
    {
        get; set;
    }

    public bool[] Thrusts
    {
        get
        {
            return thrusts;
        }
    }
    

    public void SetThrust(int index, bool on)
    {
        if (thrusts == null)
        {
            Init();
        }
        if (index >= 0 && index < thrustPositions.Length)
        {
            if (on)
            {
                thrustLights[index].intensity = 5f;
                thrustParticles[index].Play();
                if (thrusts[index] == false)
                {
                    thrusterAudioSrcs[index].Play();
                    audioOn[index] = true;
                }
            }
            else
            {
                thrustLights[index].intensity = 0f;
                thrustParticles[index].Stop();
                if (thrusts[index])
                {
                    audioOn[index] = false;
                }
            }
            thrusts[index] = on;
        }
    }
}
