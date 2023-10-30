using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public Timer timer;
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float time;
    [SerializeField] protected float yClamp;
    protected float elapsedTime;

    private void Start(){
        time = 2.5f - (0.2f * (PigScript.level - 1));
        Debug.Log("Time: " + time);
    }

    // Update is called once per frame
    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > time && timer.GetCounting()){
            SpawnObject();

            elapsedTime = 0f;
        }
    }

    protected virtual void SpawnObject(){
        float offsetY = UnityEngine.Random.Range(-yClamp - LevelOffsetYVariation(), yClamp + LevelOffsetYVariation());

        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y + offsetY);

        Instantiate(prefab, pos, Quaternion.identity, this.transform);
    }

    private float LevelOffsetYVariation(){
        return UnityEngine.Random.Range(0, PigScript.level/2);
    }
}
