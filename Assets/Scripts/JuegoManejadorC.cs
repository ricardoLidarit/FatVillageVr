using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoManejadorC : MonoBehaviourPun
{
    JugadorManejador[] jugadores;
    int jugadoresConectados = 0;

    public static JuegoManejadorC instancia;

    private void Awake()
    {
        instancia = this;
    }
    void Start()
    {
        ConfigurarJugadores();
    }

    private void ConfigurarJugadores()
    {
        //jugadores = new JugadorManejador[jugadores.Length];
        //MODO COMPARTIDO:
        //Alguien lo creo
        //No se cuantos hay ahora mismo
        //Se supone que entraran con el tiempo
        //Asi que al final no se cuantos son
        
        photonView.RPC("JugadorEnJuego", RpcTarget.AllBuffered);

    }

    //se dispara cada vez que un cliente entra al ROOM
    //igual pueden ser 20, 100 veces, pero no al mismo
    //tiempo
    [PunRPC]
    void JugadorEnJuego()
    {
        jugadoresConectados++;
        //esto lo deberia hacer cuando esten todos SOLAMENTE
        if (PhotonNetwork.IsMasterClient)
        {
            photonView.RPC("ColocarJugador", RpcTarget.All);
            //CrearBolasConPuntaje();
        }
        
    }

    private void CrearBolasConPuntaje()
    {
        for (int i = 0; i < 5; i++)
        {
            Vector3 posicion = Vector3.zero;
            posicion.x = Random.Range(-10, 10);
            posicion.y = 1;
            posicion.z = Random.Range(-10, 10);

            GameObject bolaAzul = PhotonNetwork.Instantiate("Bola Azul", posicion, Quaternion.identity);
        }

        for (int i = 0; i < 5; i++)
        {
            Vector3 posicion = Vector3.zero;
            posicion.x = Random.Range(-10, 10);
            posicion.y = 1;
            posicion.z = Random.Range(-10, 10);

            GameObject bolaRoja = PhotonNetwork.Instantiate("Bola Roja", posicion, Quaternion.identity);
        }
    }

    [PunRPC]
    void ColocarJugador()
    {

        if (NetworkManejador.Instancia.VerificarSiYaExisteJugador(PhotonNetwork.LocalPlayer)) return;

        //EL prefab debe estar en RESOURCES
        GameObject jugadorGO = PhotonNetwork.Instantiate("Jugador", Vector3.zero, Quaternion.identity);
        jugadorGO.GetComponent<JugadorManejador>().photonView.RPC("Inicializar", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }

    //public void AgregarJugador(JugadorManejador jugador, int posicion)
    //{
    //    jugadores[posicion] = jugador;
    //}
}
