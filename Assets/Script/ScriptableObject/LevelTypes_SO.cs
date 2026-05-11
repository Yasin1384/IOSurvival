using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsData", menuName = "Level System/Levels Data")]

public class LevelTypes_SO : ScriptableObject
{
    public string Name;
    
    public int Level;

    public List<GameObject> ObstaclesGameObjects;
}
