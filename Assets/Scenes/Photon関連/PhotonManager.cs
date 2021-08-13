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
    private GameObject clone;
    private HeadBob BobScript;
    public string playerName;
    private bool itemFlag = true;

    void Awake()
    {
        GameObject instance = GameObject.Find(this.gameObject.name);
        if (instance.gameObject != this.gameObject)
        {
            Destroy(this.gameObject);

        }
    }

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        PhotonNetwork.ConnectUsingSettings();
        
    }

    
    void OnGU()//サーバー接続状態の表示
    {
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
 
        //プレイヤー操作に関する２つのスクリプトをONにする
        clone.GetComponent<RigidbodyFirstPersonController>().enabled = true;

        clone.GetComponent<PlayerBehaviour>().enabled = true;
        /*カメラスクリプト
        GameObject Bob = clone.transform.Find("Player").gameObject;
        Bob.GetComponent<HeadBob>().enabled = true;
        BobScript = Bob.GetComponent<HeadBob>();
        */
        

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
}
