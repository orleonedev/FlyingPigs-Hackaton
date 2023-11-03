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

    public void LoadScene(string sceneName){
        SceneManager.LoadSceneAsync(sceneName);
        
    }

    public void LoadScene(int index) {
        SceneManager.LoadSceneAsync(index);
    }

    public void GoToMainMenu(){
        SceneManager.LoadSceneAsync(0);
    }

    public void LoadAdditively(string sceneName) {
        SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
        Debug.Log("Adding "+sceneName);
    }
    public void UnloadScene(string sceneName) {
        SceneManager.UnloadSceneAsync(sceneName);
        Debug.Log("Removing "+sceneName);
    }
}
