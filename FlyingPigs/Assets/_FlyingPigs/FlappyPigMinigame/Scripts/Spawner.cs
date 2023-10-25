using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float time;
    [SerializeField] private float yClamp;
    private float elapsedTime;

    // Update is called once per frame
    private void Update()
    {
        elapsedTime += Time.deltaTime;

        if(elapsedTime > time){
            SpawnObject();

            elapsedTime = 0f;
        }
    }

    private void SpawnObject(){
        float offsetY = UnityEngine.Random.Range(-yClamp, yClamp);

        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y + offsetY);

        Instantiate(prefab, pos, Quaternion.identity, this.transform);
    }
}
