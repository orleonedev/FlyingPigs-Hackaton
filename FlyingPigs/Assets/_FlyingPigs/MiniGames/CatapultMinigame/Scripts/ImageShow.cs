using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ImageShow : MonoBehaviour {

	public Image success;
    public Image failure;

	void Start () {
		success.enabled = false;
        failure.enabled = false;
	}

	public void SwitchShow (bool succ) {
        if(succ) {
            success.enabled = !success.enabled;
        }
        else {
            failure.enabled = !failure.enabled;
        }
        
	}
}