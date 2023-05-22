using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public GameObject bullet;
    public GameObject spPoint;
    //public Audioclip snd;

    private NavMeshAgent nvAgent;
    private Transform target;
    private float distance;
    private float fTime;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Target").GetComponent<Transform>();
        fTime = 0.0f;

        nvAgent = this.gameObject.GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
            distance = Vector3.Distance(target.transform.position, this.transform.position);
        
        if(distance < 1000.0f)
        {
            nvAgent.destination = target.position;
        }
        if(distance < 20.0f){
            fTime += Time.deltaTime;
            if(fTime > 1f)
            {
                GameObject obj = Instantiate(bullet) as GameObject;
                obj.transform.position = spPoint.transform.position;
                obj.transform.rotation = spPoint.transform.rotation;
                fTime = 0.0f;
        }
        }
        
    }
}
