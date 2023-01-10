using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManejador : MonoBehaviourPunCallbacks, ILobbyCallbacks
{

    [SerializeField] TMPro.TMP_InputField inpNombreJugador;
    [SerializeField] Button btnHost;
    [SerializeField] Button btnJoin;
    [SerializeField] Button btnShared;
    [SerializeField] TMPro.TextMeshProUGUI txtLog;

    [SerializeField] Button btnLanzar;
    [SerializeField] Button btnSalir;

    private void Awake()
    {
        inpNombreJugador.onValueChanged.AddListener(OnNombreJugadorCambia);
        btnHost.interactable = false;
        btnJoin.interactable = false;
        btnShared.interactable = false;
        btnLanzar.interactable = false;
        btnSalir.interactable = false;

        inpNombreJugador.interactable = false;

        btnHost.onClick.AddListener(CrearRoom);
        btnJoin.onClick.AddListener(UnirseRoom);
        btnLanzar.onClick.AddListener(LanzarJuego);
        btnSalir.onClick.AddListener(SalirRoom);
        btnShared.onClick.AddListener(CrearRoomCompartido);
    }

    

    void Start()
    {
        //cuando el componente inicia
        
    }


    void Update()
    {

    }

    private void OnNombreJugadorCambia(string valor)
    {
        Debug.Log($"OnNombreJugadorCambia {valor}");
        PhotonNetwork.NickName = valor;
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log($"MenuManejador - OnConnectedToMaster");
        btnHost.interactable = true;
        btnJoin.interactable = true;
        btnShared.interactable = true;
        inpNombreJugador.interactable = true;
    }

    public override void OnJoinedRoom()
    {
        //ESTO ES PARA MODO HOST
        /*
        txtLog.text += $"Alguien se unió al ROOM...";
        photonView.RPC("ActualizarLogJoinRoom", RpcTarget.All);

        //solo se le debe permitir al HOST tener habilitado el boton
        //o que sea automatico , como cuenta regresiva (NO aplica para el Host Mode)

        btnLanzar.interactable = PhotonNetwork.IsMasterClient;
        btnSalir.interactable = true;
        */

        //MODO SHARED / COMPARTIDO... a penas entro al room, lanzo la escena
        string nombreEscena = "MundoC";
        NetworkManejador.Instancia.CargarJuegoEscena(nombreEscena);
    }

    [PunRPC]
    void ActualizarLogJoinRoom()
    {
        txtLog.text = $"Jugadores que se encuentran en el room --> ";
        foreach (Player p in PhotonNetwork.PlayerList)
        {
            txtLog.text += $"{p.NickName} \n ";
        }
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        txtLog.text += $"{newPlayer.NickName} se unio al room...";
    }

    public override void OnLeftRoom()
    { 
        txtLog.text += $"Alguien salio del ROOM...";
        btnLanzar.interactable = false;
        btnSalir.interactable = false;
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    { 
        txtLog.text += $"{otherPlayer.NickName} salio del room...";
    }


    private void SalirRoom()
    {
        Debug.Log($"SalirRoom");
        PhotonNetwork.LeaveRoom();
    }

    private void LanzarJuego()
    {
        //Solo debe lanzar el HOST - Modo Host
        PhotonNetwork.CurrentRoom.IsVisible = false;
        PhotonNetwork.CurrentRoom.IsOpen = false;

        string nombreEscena = "Mundo1";
        NetworkManejador.Instancia.photonView.RPC("CargarEscena", RpcTarget.All, nombreEscena);
        //NetworkManejador.Instancia.CargarJuegoEscena(nombreEscena);

        //string, boolean, int, double, float, Vector3, Vector3...
        //no puedo enviar , debe ser serializado y deserializado
    }

    private void UnirseRoom()
    {
        NetworkManejador.Instancia.UnirseRoom();
    }

    private void CrearRoom()
    {
        Debug.Log($"CrearRoom");
        NetworkManejador.Instancia.CrearRoom();
    }

    private void CrearRoomCompartido()
    {
        NetworkManejador.Instancia.CrearUnirseRoom();
    }

}
