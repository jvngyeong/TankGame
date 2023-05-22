using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemRemove : MonoBehaviour
{
	void OnTriggerEnter (Collider col)
	{
		if(col.gameObject.tag == "Target"){
				Destroy (this.gameObject);
		}
	}

}
