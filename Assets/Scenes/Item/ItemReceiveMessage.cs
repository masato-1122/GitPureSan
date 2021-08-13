using UnityEngine;
using UnityEngine.EventSystems;

public interface ItemReceiveMessage : IEventSystemHandler
{
    void Action(GameObject targetPoint);   // アイテムの機能を使う
    void ActionForTargetedObject(GameObject target);  // アイテムの機能を対象物に使う
    void Damaged(GameObject attacker);  // ダメージを受ける
}
