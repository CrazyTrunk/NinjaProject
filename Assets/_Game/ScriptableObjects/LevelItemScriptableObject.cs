using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelItemData", menuName = "_Game/ScriptableObjects/LevelItemScriptableObject")]
public class LevelItemScriptableObject : ScriptableObject
{
    public LevelItem[] LevelItems;
}