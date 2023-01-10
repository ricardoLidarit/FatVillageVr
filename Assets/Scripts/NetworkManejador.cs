using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkManejador : MonoBehaviourPunCallbacks
{

    public static NetworkManejador Instancia;

    //no necesariamente el nombre es fijo, pero lo hare asi
    [SerializeField] string nombreRoom = "Room001";
    [SerializeField] string nombreRoomC = "Room001C";
    [SerializeField] byte cantidadMaximaJugadores = 10;

    TypedLobby tipoLobby = new TypedLobby("TutorialYT", LobbyType.SqlLobby);
    List<Player> jugadoresConectados = new List<Player>();

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PhotonNetwork.ConnectUsingSettings();
    }


    public void CrearRoom()
    {
        RoomOptions roomConfiguracion = new RoomOptions()
        {
            MaxPlayers = cantidadMaximaJugadores,
        };

        PhotonNetwork.CreateRoom(nombreRoom, roomConfiguracion);

        if (PhotonNetwork.InRoom)
        {
            PhotonNetwork.CurrentRoom.IsVisible = true;
            PhotonNetwork.CurrentRoom.IsOpen = true;
        }
    }

    public void UnirseRoom()
    {
        PhotonNetwork.JoinRoom(nombreRoom);
    }

    internal void CrearUnirseRoom()
    {
        RoomOptions roomConfiguracion = new RoomOptions()
        {
            MaxPlayers = cantidadMaximaJugadores,
        };

        PhotonNetwork.JoinOrCreateRoom(nombreRoomC, roomConfiguracion, tipoLobby);

    }


    [PunRPC]
    public void CargarJuegoEscena(string nombreEscena)
    {
        PhotonNetwork.LoadLevel(nombreEscena);
    }

    // --------------  PHOTON  --------------  
    #region Metodos de Photon PUN
    public override void OnConnectedToMaster()
    {
        Debug.Log("Conectado al servidor");
        PhotonNetwork.JoinLobby();
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarning($"Se desconectó {cause}");
    }

    

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log($"OnPlayerEnteredRoom --> ");
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log($"OnPlayerLeftRoom --> ");
    }

    public override void OnPlayerPropertiesUpdate(Player targetPlayer, ExitGames.Client.Photon.Hashtable changedProps)
    {

    }

    public override void OnJoinedRoom()
    {
        Debug.Log($"OnJoinedRoom --> ");

        //MODO SHARED / COMPARTIDO... a penas entro al room, lanzo la escena
        //string nombreEscena = "MundoC";
        //NetworkManejador.Instancia.photonView.RPC("CargarEscena", RpcTarget.All, nombreEscena);
    }

    public override void OnLeftRoom()
    {
        Debug.Log($"OnLeftRoom --> ");
    } 
    #endregion


    private void OnApplicationQuit()
    {
        PhotonNetwork.Disconnect();
    }


    [PunRPC]
    private void CargarEscena(string nombreEscena)
    {
        PhotonNetwork.LoadLevel(nombreEscena);
    }

    internal bool VerificarSiYaExisteJugador(Player localPlayer)
    {
        bool existe = jugadoresConectados.Contains(localPlayer);

        Debug.Log($"localPlayer {localPlayer.NickName} - {existe}");
        if (!existe) jugadoresConectados.Add(localPlayer);

        return existe;
    }
}
