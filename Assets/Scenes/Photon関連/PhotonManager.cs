using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityStandardAssets.Characters.FirstPerson;
using Photon.Pun;
using Photon.Realtime;

public class PhotonManager : MonoBehaviourPunCallbacks
{
    private string logMessage;
    private Vector2 scrollPosition;
    private RigidbodyFirstPersonController RigidScript;
    private PlayerBehaviour PlayerScript;
    public GameObject clone;
    private HeadBob BobScript;
    public string playerName = null;
    private bool itemFlag = true;

    //プレイヤーの服の色、体の色
    private Color clothColor;
    private Color bodyColor;
    private PhotonView photonView;

    //名前表示オブジェクト
    private GameObject namePanel;
    public GameObject namePrefab;

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.ConnectUsingSettings();
    }
    
    void OnGUI()
    {
        //(new Rect(左上のｘ座標, 左上のｙ座標, 横幅, 縦幅), "テキスト", スタイル（今は省略）)
        GUILayout.Label("ClientState:" + PhotonNetwork.NetworkClientState);
        using (GUILayout.ScrollViewScope scrollView = new GUILayout.ScrollViewScope(scrollPosition, GUILayout.Width(400), GUILayout.Height(300)))
        {
            scrollPosition = scrollView.scrollPosition;
            GUILayout.Label(logMessage);
        }
    }

    void JoinRoomLoaded(Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        logMessage += PhotonNetwork.NickName + "を生成します。\n";
        PhotonNetwork.IsMessageQueueRunning = true;
        clone = PhotonNetwork.Instantiate("Player", new Vector3(50f, 5f, -90f), Quaternion.identity);
        photonView = clone.GetComponent<PhotonView>();
        photonView.RPC("setClothColor", RpcTarget.AllBufferedViaServer);


        //プレイヤー操作に関する２つのスクリプトをONにする
        clone.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        clone.GetComponent<PlayerBehaviour>().enabled = true;
        clone.GetComponent<PlayerBehaviour>().setName(PhotonNetwork.NickName);

        ///プレイヤーの名前表示テキスト出現
        ///どちらが良いか検証中
        ///1.名前テキストを単独で出現させ、プレイヤーの頭上に追従させる
        ///2.プレイヤーに事前に名前テキストを付け、プレイヤーと同時に出現させる

        SceneManager.sceneLoaded -= this.JoinRoomLoaded;
    }


    void LeaveRoomLoaded(Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        PhotonNetwork.IsMessageQueueRunning = true;
        SceneManager.sceneLoaded -= this.LeaveRoomLoaded;
    }

    public void JoinRoom(string roomName)
    {
        PhotonNetwork.JoinOrCreateRoom(roomName, new RoomOptions(), TypedLobby.Default);
    }

    public void LogoutRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public override void OnConnectedToMaster()
    {
        base.OnConnectedToMaster();

        this.logMessage += "マスターサーバーに接続しました。\n";
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        this.logMessage += "ルーム[" + PhotonNetwork.CurrentRoom.Name + "]に入室しました。\n";
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.sceneLoaded += this.JoinRoomLoaded;
        SceneManager.LoadScene("Game");
    }


    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        this.logMessage += "ルームから退出しました。\n";
        SceneManager.sceneLoaded += this.LeaveRoomLoaded;
        SceneManager.LoadScene("Login");
    }


    public Color getClothColor()
    {
        return clothColor;
    }

    public Color getBodyColor()
    {
        return bodyColor;
    }

    public void setColor( Color c)
    {
        clothColor = c;
    }

    public void setBodyColor( Color c)
    {
        bodyColor = c;
    }
}
