using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Unity.VisualScripting;

public class SBullet : MonoBehaviour
{
    public int spower = 2000;
    public AudioClip sound;
    public GameObject exp;
    public GameObject DamagePoint;

    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * spower);
    }

    void Update()
    {

    }
 
    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject);
        
        if (col.gameObject.tag != "SBULLET")
        {
            GameObject copy_DP = Instantiate(DamagePoint) as GameObject;
            copy_DP.transform.position = transform.position;
            Destroy(copy_DP, 0.1f);

            AudioSource.PlayClipAtPoint(sound, transform.position);
            GameObject copy_exp = Instantiate(exp) as GameObject;
            copy_exp.transform.position = col.gameObject.transform.position;
            Destroy(copy_exp, 5.0f);
            copy_exp.transform.position = this.transform.position;
            Destroy(this.gameObject);



            
        }
    }
}
