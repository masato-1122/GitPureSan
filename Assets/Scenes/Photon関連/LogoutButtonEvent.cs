using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LogoutButtonEvent : MonoBehaviour
{
    private PhotonManager photonManager;    // ���C���N���X���擾


    void Start()
    {
        this.photonManager = GameObject.Find("PhotonManager").GetComponent<PhotonManager>();
    }


    // �{�^���̃C�x���g
    public void OnButtonClicked()
    {
        if (this.photonManager != null)
        {
            this.photonManager.LogoutRoom();
        }
    }
}