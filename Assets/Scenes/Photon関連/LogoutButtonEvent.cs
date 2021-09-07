using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogoutButtonEvent : MonoBehaviour
{
    private PhotonManager photonManager;    // メインクラスを取得


    void Start()
    {
        this.photonManager = GameObject.Find("PhotonManager").GetComponent<PhotonManager>();
    }


    // ボタンのイベント
    public void OnButtonClicked()
    {
        if (this.photonManager != null)
        {
            this.photonManager.LogoutRoom();
        }
    }
}