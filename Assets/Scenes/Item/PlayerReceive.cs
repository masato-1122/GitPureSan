using UnityEngine;
using UnityEngine.EventSystems;

public interface PlayerReceive : IEventSystemHandler
{
    void Damaged(GameObject attacker);  // ダメージを受ける
}
