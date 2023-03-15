using UnityEngine;
public class ChestModel
{
    private ChestController Controller;
    private ChestObject config;
    private int unlockTimer;
    public void SetController(ChestController _controller)
    {
        this.Controller = _controller;
    }
    public ChestModel (ChestObject _chestObject)
    {
        config = _chestObject;
        ResetTimer();
    }
    public ChestType chestType    {get{return config.type;}}
    public Sprite Thumb    {get{return config.thumb;}}
    public Sprite openThumb    {get{return config.thumbOpen;}}
    public void ResetTimer()    {unlockTimer = config.UnlockDuration;}
    public int UnlockDuration    {get{return unlockTimer;}set{unlockTimer = value;}}
    public int minGold    {get{return config.minGold;}}
    public int maxGold    {get{return config.maxGold;}}
    public int minGems    {get{return config.minGems;}}
    public int maxGems    {get{return config.maxGems;}}
}
