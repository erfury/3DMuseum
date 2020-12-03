using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoomController : MonoBehaviourPunCallbacks
{
    public int multiplayerSceneIndex;

    public override void OnEnable()
    {
        PhotonNetwork.AddCallbackTarget(this);
    }


    public override void OnDisable()
    {
        PhotonNetwork.RemoveCallbackTarget(this);
    }


    public override void OnJoinedRoom()
    {
        print("Joined to a room");

        if (PhotonNetwork.IsMasterClient)
        {
            print("Starting the game ...");
            PhotonNetwork.LoadLevel(multiplayerSceneIndex);
        }
    }
}
