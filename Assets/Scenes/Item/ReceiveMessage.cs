using UnityEngine.EventSystems;

public interface ReceiveMessage : IEventSystemHandler
{
    void setDead();
}