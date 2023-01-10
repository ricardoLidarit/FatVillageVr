using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JuegoManejador : MonoBehaviourPun
{

    JugadorManejador[] jugadores;
    int jugadoresConectados = 0;

    void Start()
    {
        ConfigurarJugadores();
    }

    private void ConfigurarJugadores()
    {
        //jugadores = new JugadorManejador[jugadores.Length];
        
        //MODO HOST:
        //Alguien lo creo
        //Se supone que hay X jugadores en el room
        //Quiero generar los componentes para cada uno
        //de los que estan ahora mismo en el room
        photonView.RPC("JugadorEnJuego", RpcTarget.All);

    }

    //Si hay 20, se dispara 20 veces
    //si hay 200, se dispara 200 veces
    [PunRPC]
    void JugadorEnJuego()
    {
        jugadoresConectados++;
        //esto lo deberia hacer cuando esten todos SOLAMENTE
        if (PhotonNetwork.IsMasterClient && jugadoresConectados == PhotonNetwork.PlayerList.Length)
        {
            photonView.RPC("ColocarJugador", RpcTarget.All);
            CrearBolasConPuntaje();
        }
    }

    private void CrearBolasConPuntaje()
    {
        for (int i= 0; i < 5; i++)
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

        //EL prefab debe estar en RESOURCES
        GameObject jugadorGO = PhotonNetwork.Instantiate("Jugador", Vector3.zero, Quaternion.identity);

        jugadorGO.GetComponent<JugadorManejador>().photonView.RPC("Inicializar", RpcTarget.All, PhotonNetwork.LocalPlayer);
    }
     
}
