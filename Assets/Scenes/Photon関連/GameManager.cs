using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public GameObject playerPrefab;
    private RigidbodyFirstPersonController controllerScript;
    private PlayerBehaviour playerScript;
    public Color clothColor;
    public PhotonView photonView;

    // Start is called before the first frame update
    void Start()
    {
        //Photonに接続していれば自プレイヤーを生成
        Player = PhotonNetwork.Instantiate(this.playerPrefab.name, new Vector3(44f, 1.6f, -86f), Quaternion.identity, 0);
        Player.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        Player.GetComponent<PlayerBehaviour>().enabled = true;
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyUp(KeyCode.P))
        {
            photonView.RPC("setClothColor", RpcTarget.AllBuffered);
        }

    }

    void OnPhotonCreateRoomFailed()
    {
        SceneManager.LoadScene("Login"); //ログイン画面に戻る
        return;
    }
}
