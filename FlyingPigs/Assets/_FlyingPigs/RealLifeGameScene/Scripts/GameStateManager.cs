using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateManager : MonoBehaviour
{
    [SerializeField]
    public GameObject InGameSceneObject;

    [SerializeField]
    public GameObject ChatSceneObject;
    
    // Start is called before the first frame update
    void Start()
    {
        SwitchToInGame();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchToInGame() {
        if (ChatSceneObject.activeInHierarchy) {
            ChatSceneObject.SetActive(false);
        }
        InGameSceneObject.SetActive(true);
    }

    public void SwitchToChat() {
        if (InGameSceneObject.activeInHierarchy) {
            InGameSceneObject.SetActive(false);
        }
        ChatSceneObject.SetActive(true);
    }
}
