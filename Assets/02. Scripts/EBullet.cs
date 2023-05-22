using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class EBullet : MonoBehaviour
{
    public int power = 500;
    public AudioClip sound;
    public GameObject exp;
    public GameObject Target;
    
    void Start()
    {
        this.GetComponent<Rigidbody>().AddForce(transform.forward * power);
    }

    void Update()
    {

    }

    void OnTriggerEnter(Collider col)
    {
        Debug.Log(col.gameObject);
        if (col.gameObject.tag != "EBULLET")
        {
            AudioSource.PlayClipAtPoint(sound, transform.position);
            GameObject copy_exp = Instantiate(exp) as GameObject;
            copy_exp.transform.position = col.gameObject.transform.position;
            Destroy(copy_exp, 5.0f);
            copy_exp.transform.position = this.transform.position;
            Destroy(this.gameObject);
        }
    }
}
