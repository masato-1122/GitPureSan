using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;

public class RoomJoin : MonoBehaviour
{
    public InputField RoomNameField;
    public InputField PlayerNameField;
    private PhotonManager photonManager;

    void Start()
    {
        this.photonManager = GameObject.Find("PhotonManager").GetComponent<PhotonManager>();
    }


    // ボタンのイベント
    public void OnButtonClicked()
    {
        if (this.photonManager != null)
        {
            string _roomName = this.RoomNameField.text;
            string _playerName = this.PlayerNameField.text;

            if (!string.IsNullOrEmpty(_roomName) && _playerName != "")
            {
                // ルームに接続
                this.photonManager.JoinRoom(this.RoomNameField.text);
                this.photonManager.setColor(GameObject.Find("ColorChange").GetComponent<ColorChange>().getColor());
                //プレイヤーの名前を渡す
                PhotonNetwork.NickName = _playerName;
                Debug.Log(PhotonNetwork.NickName);
            }

            else
            {
                Debug.LogError("部屋名またはプレイヤーの名前を入力してください");
                return;
            }
        }
    }
}