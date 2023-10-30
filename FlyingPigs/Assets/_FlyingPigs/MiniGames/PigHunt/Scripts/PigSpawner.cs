using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PigSpawner : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    [SerializeField] protected float time;
    [SerializeField] protected float yClamp;
    protected float elapsedTime;

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > time){
            SpawnObject();
            elapsedTime = 0f;
        }
    }

    protected virtual void SpawnObject(){
        float offsetY = UnityEngine.Random.Range(yClamp, yClamp - 2);

        float posX = UnityEngine.Random.Range(-2, 2);

        Vector2 pos = new Vector2(posX, this.transform.position.y + offsetY);

        Instantiate(prefab, pos, Quaternion.identity, this.transform);
    }
}
