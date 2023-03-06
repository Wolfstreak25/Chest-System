using System;
public class EventManagement : Singleton<EventManagement>
{
    public event Action<ChestController> startTimer;
    public event Action<ChestController> stopTimer;
    public event Action<int,int> updateResource;
    public event Action<ChestController> showPopUp;
    public event Action<ChestController> returnChest;
    public event Action death;
    public void StartTimer(ChestController _controller)
    {
        startTimer?.Invoke(_controller);
    }
    public void StopTimer(ChestController _controller)
    {
        stopTimer?.Invoke(_controller);
    }
    public void UpdateResource(int Gold,int Gems)
    {
        updateResource?.Invoke(Gold,Gems);
    }
    public void ShowPopUp(ChestController _controller)
    {
        showPopUp?.Invoke(_controller);
    }
}
