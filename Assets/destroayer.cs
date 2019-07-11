using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroayer : MonoBehaviour
{
   public float lifeTime = 10f;
    public float dieTime = 2f;
    public GameObject DestroayDusts;
    public GameObject TheSphere;

    public void Start()
    {
        TheSphere.SetActive(true);
    }
    // Update is called once per frame
    void Update()
    {
        if (lifeTime > 0)
        {
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
