using System.IO;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;

public class GameSetupController : MonoBehaviourPunCallbacks
{
    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
    }


    // Start is called before the first frame update
    void Start()
    {
        CreatePlayer();
    }


    void CreatePlayer()
    {
        print("Creating player ...");
        var masterPlayer = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PhotonPlayer"),new  Vector3(0, 1.5f, 0), Quaternion.identity);
        //masterPlayer.GetComponent<FPSMovement>().isMaster = true;
    }
    
}
