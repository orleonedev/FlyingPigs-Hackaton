using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PigSpawner : MonoBehaviour
{
    [SerializeField] protected Timer timer;
    [SerializeField] private TMP_Text score;
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float time;
    [SerializeField] protected float yClamp;
    protected float elapsedTime;
    protected bool spawnDirection; // when true spawn from left, when false from right

    private void Start(){
        var dir = UnityEngine.Random.Range(0.0f, 1.0f);

        if(dir >= 0.0f && dir <= 0.5f){
            spawnDirection = false;
        } else {
            spawnDirection = true;
        }

        ClickableObject.OnPigDestroy += OnScore;
    }

    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > time && timer.GetCounting()){
            SpawnObject();
            elapsedTime = 0f;
        }
    }

    protected virtual void SpawnObject(){
        float offsetY = UnityEngine.Random.Range(yClamp, yClamp - 2);
        
        Vector2 pos = new Vector2(GetSpawnPosX(), this.transform.position.y + offsetY);

        Instantiate(prefab, pos, Quaternion.identity, this.transform);
    }

    protected float GetSpawnPosX(){
        float posX;

        if(spawnDirection){
            posX = UnityEngine.Random.Range(-2, 0);
            spawnDirection = false;
        } else {
            posX = UnityEngine.Random.Range(0, 2);
            spawnDirection = true;
        }

        return posX;
    }

    private void OnScore(){
        score.text = (int.Parse(score.text) + 1).ToString();
    }
}
