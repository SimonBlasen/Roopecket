using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroayer : MonoBehaviour
{
   public float lifeTime = 10f;
    public float dieTime = 2f;
    public GameObject DestroayDusts;
    public GameObject TheSphere;
    public Rigidbody Rigidbody;
    public float ForceX = 0f;
    public float ForceY = 0f;
    public float ForceZ = 0f;

   

    public void Start()
    {
        TheSphere.SetActive(true);
        
    }

   
    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {

            

            if (lifeTime >= 7)
           Rigidbody.AddForce(ForceX, ForceY, ForceZ, ForceMode.Impulse);

           /*  var Forces = GetComponent<SphereSpitter>();
            ForceX = Forces.ForceX;
            ForceY = Forces.ForceY;
            ForceZ = Forces.ForceZ;  */


            lifeTime -= Time.deltaTime;
            
            if (lifeTime <= 0)
                Destruction();
        }
    }

    private void Destruction()
    {

       
        DestroayDusts.SetActive(true);
    
                Destroy(this.gameObject);
       
        


    }
}
