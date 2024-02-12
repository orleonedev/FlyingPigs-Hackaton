using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : Spawner
{
    private void Start(){
        SpawnObject();
        switch(Camera.main.aspect){
            case (float) 10/16:
            case (float) 9/16:
            case (float) 3/4:
                tabletOffset = 3.5f;
                break;
            default:
                tabletOffset = 0f;
                break;
        }
    }

    protected override void SpawnObject(){
        int i = 1;
        Vector2 size = prefab.GetComponent<BoxCollider2D>().size;
        float midTileXSize = Mathf.Round((size.x)/2 * 100.0f) * 0.01f; 

        while(i < 5){
            Vector2 pos = new Vector2((this.transform.position.x + midTileXSize + tabletOffset) * i, this.transform.position.y);

            Instantiate(prefab, pos, Quaternion.identity, this.transform);
            i++;
        }
    }
}
