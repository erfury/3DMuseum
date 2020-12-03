using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class LobbyController : MonoBehaviourPunCallbacks
{
    public GameObject joinButton;

    public override void OnConnectedToMaster()
    {
        PhotonNetwork.AutomaticallySyncScene = true;
        joinButton.SetActive(true);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        print("Joining failed...");
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        print("Creating a new room failed ...");
        CreateRoom();
    }
    
    void CreateRoom()
    {
        print("Creating a new room now ...");
        int roomCode = 10;
        RoomOptions roomOps = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 0 };
        PhotonNetwork.CreateRoom("Room" + roomCode, roomOps);
    }

    public void JoinRoom()
    {
        joinButton.SetActive(false) ;
        PhotonNetwork.JoinRandomRoom();
        print("Trying to join a room ...");
    }
   
}
