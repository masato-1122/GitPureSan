using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using Photon.Pun;
using Photon.Realtime;

public class NameTag : MonoBehaviour
{
    public Player TargetPlayer;
    public Vector3 Offset;

    private Text _nameText;
    [SerializeField] private PlayerBehaviour _target;

    
    // Start is called before the first frame update
    void Start()
    {
        _nameText = GetComponent<Text>();
    }

    
    void Update()
    {
        if (_target == null)
        {
            Debug.Log("ネームタグのターゲットが見つかりません。\n");

            //GetComponent<Text>().enabled = false;
            Destroy(gameObject);

            return;
        }

        Vector3 targetScreenPosition = Camera.main.WorldToScreenPoint(_target.transform.position);

        _nameText.text = _target.UserName;
        transform.position = targetScreenPosition + Offset;
    }


    public void Initialize(PlayerBehaviour target)
    {
        _target = target;
    }
    
}
