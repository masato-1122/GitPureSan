using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


/// <summary>
/// RaiseEvent‚Ìƒ‰ƒbƒp[.
///
/// ƒCƒxƒ“ƒg‚Ì‘«‚µ•û
/// ‡@ enum‚ğ’Ç‰Á
/// ‡A Action‚ğ’Ç‰Á
/// ‡B RaiseEventóM‚ÌƒCƒxƒ“ƒg‚ğenum‚É]‚Á‚ÄU‚è•ª‚¯
/// ‡C RES‚É‘—Mƒƒ\ƒbƒh‚ğ’Ç‰Á
/// </summary>

// RaiseEventReceiver
public class RER : SingletonMonoBehaviour<RER>
{
    
    public void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }
    

    // ‡@
    // eventCode. 0~199B0‚Í“Áê‚Èˆµ‚¢‚Ì‚½‚ß1‚©‚çn‚ß‚é
    public enum RaiseEventType : byte
    {
        SampleEvent = 1,
        ChangeColor = 2,
    }

    // ‡A
    public Action<string> OnSampleEvent;
    public Action<Color> cloneChangeColor;

    // ‡B
    
    public void OnEvent(EventData photonEvent)
    {
        var type = (RaiseEventType)Enum.ToObject(typeof(RaiseEventType), photonEvent.Code);
        Debug.Log("RaiseEvent Received. Type = " + type);
        
        switch (type)
        {
            
            case RaiseEventType.ChangeColor:
                var color = (Color)Enum.ToObject(typeof(Color), photonEvent.CustomData);
                cloneChangeColor?.Invoke(color);
                break;
            

            case RaiseEventType.SampleEvent:
            default:
                return;
        }
        
    }
    
}


// RaiseEventSender
public static class RES
{
    // ‡C
    public static void SendSampleEvent(string message)
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent((byte)RER.RaiseEventType.SampleEvent, message, raiseEventOptions, SendOptions.SendReliable);
    }

    public static void ChangeColor(string message)
    {
        var raiseEventOptions = new RaiseEventOptions
        {
            Receivers = ReceiverGroup.All,
            CachingOption = EventCaching.AddToRoomCache,
        };
        PhotonNetwork.RaiseEvent((byte)RER.RaiseEventType.ChangeColor, message, raiseEventOptions, SendOptions.SendReliable);
    }
}
