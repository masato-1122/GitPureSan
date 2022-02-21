using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;

public class GachaBehaviour : ItemBehaviour, ItemReceiveMessage
{
    public GameObject[] spawns;
    private GameObject spawn;
    public GameObject spawnPoint;

    private int ranMin = 0;
    private int ranMax = 20;
    private int[] weights;

    private bool possible = true;

    void Start()
    {
        base.Start();
        weights = new int[spawns.Length -1];
        int relay = ranMax / spawns.Length;

        for (int i = 0; i < weights.Length; i++)
        {
            weights[i] = (i+1) * relay;
        }
        spawn = spawns[0];
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
    }

    public void Action(GameObject target)
    {

    }

    public void ActionForTargetedObject(GameObject taregt)
    {
        if (GetLottery())
        {
            gameObject.GetComponent<PhotonView>().RPC("DrawLottery", RpcTarget.AllBuffered);
            //DrawLottery();
            GameObject item = PhotonNetwork.Instantiate(spawn.name, spawnPoint.transform.position, Quaternion.identity);
            item.transform.parent = gameObject.transform;
        }
        LostLottery();
    }

    public void Damaged(GameObject attacker)
    {

    }

    public void Reset()
    {

    }

    [PunRPC]
    public void DrawLottery()
    {
        int kuji = Random.Range(0, 20);
        if (kuji <= weights[0])
        {
            spawn = spawns[0];
        }
        else if (kuji >= weights[spawns.Length-1])
        {
            spawn = spawns[spawns.Length -1];
        }

        for (int i = 1; i < weights.Length; i++)
        {
            if (weights[i] <= kuji && kuji < weights[i+1])
            {
                spawn = spawns[i];
            }
        }
    }

    private GameObject SpawnObject()
    {
        return spawn;
    }

    public bool GetLottery()
    {
        return possible;
    }

    public void SetLottery()
    {
        possible = true;
    }
    public void LostLottery()
    {
        possible = false;
    }
}
