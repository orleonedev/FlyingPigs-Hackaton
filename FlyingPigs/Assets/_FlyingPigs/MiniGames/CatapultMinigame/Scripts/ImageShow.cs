using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class ImageShow : MonoBehaviour {

	public TMP_Text success;
    public TMP_Text failure;

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