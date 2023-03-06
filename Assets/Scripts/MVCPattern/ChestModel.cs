using UnityEngine;
public class ChestModel
{
    private ChestController Controller;
    private ChestObject config;
    private int Timer;
    public void SetController(ChestController _controller)
    {
        this.Controller = _controller;
    }
    public ChestModel (ChestObject _chestObject)
    {
        config = _chestObject;
        ResetTimer();
    }
    public ChestType type
    {
        get
        {return config.type;}
    }
    public Sprite Thumb
    {
        get
        {
            return config.thumb;
        }
    }
    public Sprite openThumb
    {
        get
        {
            return config.thumbOpen;
        }
    }
    public void ResetTimer()
    {
        Timer = config.timer;
    }
    public int CountDown
    {
        get
        {
            return Timer;
        }
        set
        {
            Timer = value;
        }
    }
    public int minGold
    {
        get
        {
            return config.minGold;
        }
    }
    public int maxGold
    {
        get
        {
            return config.maxGold;
        }
    }
    public int minGems
    {
        get
        {
            return config.minGems;
        }
    }
    public int maxGems
    {
        get
        {
            return config.maxGems;
        }
    }
}
