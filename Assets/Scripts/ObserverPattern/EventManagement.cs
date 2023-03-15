using System;
public class EventManagement : Singleton<EventManagement>
{
    public event Action enablePopUp;
    public event Action<int,int> updateResource;
    public event Action<ChestController> showPopUp;
    public event Action<bool> timerState;
    public void UpdateResource(int Gold,int Gems)
    {
        updateResource?.Invoke(Gold,Gems);
    }
    public void ShowPopUp(ChestController _controller)
    {
        showPopUp?.Invoke(_controller);
    }
    public void EnablePopUp()
    {
        enablePopUp?.Invoke();
    }
    public void TimerState(bool state)
    {
        timerState?.Invoke(state);
    }
}
