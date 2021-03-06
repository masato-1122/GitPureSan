using System;
using ExitGames.Client.Photon;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine;


/// <summary>
/// RaiseEventのラッパー.
///
/// イベントの足し方
/// ?@ enumを追加
/// ?A Actionを追加
/// ?B RaiseEvent受信時のイベントをenumに従って振り分け
/// ?C RESに送信メソッドを追加
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
    

    // ?@
    // eventCode. 0~199。0は特殊な扱いのため1から始める
    public enum RaiseEventType : byte
    {
        SampleEvent = 1,
        ChangeColor = 2,
    }

    // ?A
    public Action<string> OnSampleEvent;
    public Action<Color> cloneChangeColor;

    // ?B
    
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
    // ?C
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
