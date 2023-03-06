using UnityEngine;
using System.Collections.Generic;
[CreateAssetMenu(fileName = "Chest List", menuName = "Objects/New Chest List")]
public class ChestList : ScriptableObject
{
    public List<ChestObject> Chest;
}