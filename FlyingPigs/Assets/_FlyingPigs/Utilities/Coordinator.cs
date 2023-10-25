using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Coordinator : MonoBehaviour
{
    public static Coordinator Instance { get; private set;}

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake() {
		Instance = this;
	}
    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(string sceneName){
        SceneManager.LoadScene(sceneName);
    }

    public void loadScene(int index) {
        SceneManager.LoadScene(index);
    }

    public void GoToMainMenu(){
        SceneManager.LoadScene(0);
    }
}
