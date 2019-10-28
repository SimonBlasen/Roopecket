using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class rocketPush : MonoBehaviour
{

    [SerializeField]
    private KeyCode Rocketpush;
    [SerializeField]
    private Rigidbody RB;
    [SerializeField]
    private float ForceMulti;
    [SerializeField]
    private float pushLimit;
    [SerializeField]
    private GameObject ImpulseParticles;

    [SerializeField]
    TextMeshPro Ecounter;

    // Start is called before the first frame update
    void Start()
    {
        RB = GetComponent<Rigidbody>();
        pushLimit = 3f;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(Rocketpush))
        {
            if (pushLimit >= 1)
            {
            RB.AddForce(new Vector3(0, ForceMulti, 0), ForceMode.Impulse);
            GameObject ParticlePosition = Instantiate(ImpulseParticles);
            ParticlePosition.transform.position = transform.position;
            
            pushLimit -= 1f;
                Debug.Log(pushLimit);
            }

            Ecounter.text = pushLimit.ToString();
       
        }

        

    }

}
