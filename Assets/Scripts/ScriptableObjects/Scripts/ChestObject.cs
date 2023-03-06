using UnityEngine;
[CreateAssetMenu(fileName = "Chest", menuName = "Objects/New Chest")]
public class ChestObject : ScriptableObject
{
    [Header("Chest Prefab")]
    public ChestView chestPrefab;
    public ChestType type;
    [Header("Chest Visuals")]
    public Sprite thumb;
    public Sprite thumbOpen;
    [Header("Rewards")]
    public int minGold;
    public int maxGold;
    public int minGems;
    public int maxGems;
    [Header("Timer (in Seconds)")]
    public int timer;
}