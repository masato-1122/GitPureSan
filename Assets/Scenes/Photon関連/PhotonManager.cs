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

    //�v���C���[�̕��̐F�A�̂̐F
    private Color clothColor;
    private Color bodyColor;
    private PhotonView photonView;

    //���O�\���I�u�W�F�N�g
    private GameObject namePanel;
    public GameObject namePrefab;

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
        if( GameObject.Find("PhotonManager") != null)
        {
            DontDestroyOnLoad(this.gameObject);
        }
        PhotonNetwork.ConnectUsingSettings();
    }

    void OnGUI()
    {
        //(new Rect(����̂����W, ����̂����W, ����, �c��), "�e�L�X�g", �X�^�C���i���͏ȗ��j)
        GUILayout.Label("ClientState:" + PhotonNetwork.NetworkClientState);
        using (GUILayout.ScrollViewScope scrollView = new GUILayout.ScrollViewScope(scrollPosition, GUILayout.Width(400), GUILayout.Height(300)))
        {
            scrollPosition = scrollView.scrollPosition;
            GUILayout.Label(logMessage);
        }
    }

    void JoinRoomLoaded(Scene scene, LoadSceneMode mode = LoadSceneMode.Single)
    {
        logMessage += PhotonNetwork.NickName + "�𐶐����܂��B\n";
        PhotonNetwork.IsMessageQueueRunning = true;
        clone = PhotonNetwork.Instantiate("Player", new Vector3(50f, 5f, -90f), Quaternion.identity);

        //�v���C���[����X�N���v�g�̏���
        clone.GetComponent<RigidbodyFirstPersonController>().enabled = true;
        clone.GetComponent<PlayerBehaviour>().enabled = true;
        clone.GetComponent<PlayerBehaviour>().SetName(PhotonNetwork.NickName);
        clone.GetComponent<PlayerBehaviour>().SetColor(getClothColor());
        clone.GetComponent<PhotonView>().RPC("UpdateMemberList", RpcTarget.AllBuffered);
        SceneManager.sceneLoaded -= this.JoinRoomLoaded;
    }

    public void OnPhotonPlayerConnected(Player player)
    {
        Debug.Log(player.NickName + " is joined.");
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

        this.logMessage += "�}�X�^�[�T�[�o�[�ɐڑ����܂����B\n";
    }


    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        this.logMessage += "���[��[" + PhotonNetwork.CurrentRoom.Name + "]�ɓ������܂����B\n";
        PhotonNetwork.IsMessageQueueRunning = false;

        SceneManager.sceneLoaded += this.JoinRoomLoaded;
        SceneManager.LoadScene("Game");
    }


    public override void OnLeftRoom()
    {
        base.OnLeftRoom();
        this.logMessage += "���[������ޏo���܂����B\n";
        SceneManager.sceneLoaded += this.LeaveRoomLoaded;
        SceneManager.LoadScene("Login");
        
    }

    public void SetColor(Color c)
    {
        clothColor = c;
    }

    public Color getClothColor()
    {
        return clothColor;
    }
}
