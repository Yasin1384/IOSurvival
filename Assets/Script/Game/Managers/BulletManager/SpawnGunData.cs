using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SpawnBulletData
{
    public string bulletTypeName;
    public BulletTypes_SO GunTypes; 
    public int poolSize = 10;
    public Queue<GameObject> pool = new Queue<GameObject>();
}
