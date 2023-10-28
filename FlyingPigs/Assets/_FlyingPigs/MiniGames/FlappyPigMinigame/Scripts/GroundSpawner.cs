using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : Spawner
{
    private void Start(){
        
    }

    protected override void SpawnObject(){
        Vector2 pos = new Vector2(this.transform.position.x, this.transform.position.y);

        Instantiate(prefab, pos, Quaternion.identity, this.transform);
    }
}
