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
    protected float tabletOffset = 0f;

    private void Start(){
        time = time - (0.25f * (PigScript.level - 1));

        switch(Camera.main.aspect){
            case (float) 10/16:
            case (float) 9/16:
            case (float) 3/4:
                tabletOffset = 1.5f;
                break;
            default:
                tabletOffset = 0f;
                break;
        }
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
        float offsetY = UnityEngine.Random.Range(-LevelOffsetYVariation(), yClamp + LevelOffsetYVariation());

        Vector2 pos = new Vector2(this.transform.position.x + tabletOffset, this.transform.position.y + offsetY);

        Instantiate(prefab, pos, Quaternion.identity, this.transform);
    }

    private float LevelOffsetYVariation(){
        return UnityEngine.Random.Range(0.5f, (PigScript.level >= 3) ? 1.0f : 1.5f);
    }
}
