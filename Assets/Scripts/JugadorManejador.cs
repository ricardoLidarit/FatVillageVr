using Photon.Pun;
using Photon.Realtime;
using StarterAssets;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class JugadorManejador : MonoBehaviourPun
{
    [SerializeField] int Id;
    [SerializeField] string nombre;
    [SerializeField] string prefabBola;
    [SerializeField] GameObject prefabBolaGO;
    [SerializeField] private TextMeshProUGUI nombreAppear;
    [SerializeField] GameObject joystic;   


    Player jugador;

    [SerializeField] GameObject camaras;


    public delegate void CuandoJugadorEstaInicializado(GameObject jugador);
    public static event CuandoJugadorEstaInicializado JugadorInicializado;

    private StarterAssetsInputs _input;

    private bool probarCreacionGameobjects = false;

    void Start()
    {
        //solo cuando quiero probar la creacion de gameobjects
        //estamos con Host Mode, pero luego usaremos esto mismo para Shared Mode
        probarCreacionGameobjects = false;
    }

    
    void Update()
    {
        //#region Prueba de Creacion de GameObjects - Host Mode
        //if (_input.crear && probarCreacionGameobjects)
        //{
        //    _input.crear = false;
        //    Vector3 posicion = Vector3.zero;
        //    posicion.x = Random.Range(-10, 10);
        //    posicion.y = Random.Range(1, 2);
        //    posicion.z = Random.Range(-10, 10);

        //    GameObject bolaGO_PUN = PhotonNetwork.Instantiate(prefabBola, posicion, Quaternion.identity);
        //    bolaGO_PUN.GetComponent<Renderer>().material.color = Color.blue;

        //    posicion.x = Random.Range(-10, 10);
        //    posicion.y = Random.Range(1, 2);
        //    posicion.z = Random.Range(-10, 10);


        //    GameObject bolaGO = Instantiate(prefabBolaGO, posicion, Quaternion.identity);
        //    bolaGO.GetComponent<Renderer>().material.color = Color.red;

        //    photonView.RPC("CrearObjetoRPC", RpcTarget.All);
        //} 
        //#endregion

        //nombreAppear.text = nombre;

    }

    [PunRPC]
    void CrearObjetoRPC()
    {
        Vector3 posicion = Vector3.zero;
        //posicion.x = Random.Range(-10, 10);
        //posicion.y = Random.Range(1, 2);
        //posicion.z = Random.Range(-10, 10);

        GameObject bolaGO_PUN_RPC = PhotonNetwork.Instantiate(prefabBola, posicion, Quaternion.identity);
        bolaGO_PUN_RPC.GetComponent<Renderer>().material.color = Color.green;
    }


    [PunRPC]
    void Inicializar(Player jugador)
    {        
        Id = jugador.ActorNumber;
        nombre = jugador.NickName;
        nombreAppear.text = nombre;
        this.jugador = jugador;

        //JuegoManejadorC.instancia.AgregarJugador(this, Id - 1);

        if (!photonView.IsMine)
        {
            //si no es mio que pasa?
            //GetComponentInChildren<Camera>().gameObject.SetActive(false);
        }
        else
        {
            //_input = GetComponentInChildren<StarterAssetsInputs>();
            //si es mio, que quiero hacer?
            //GetComponentInChildren<Camera>(true).gameObject.SetActive(true);
            camaras.SetActive(true);
            GetComponentInChildren<ThirdPersonController>(true).gameObject.SetActive(true);
            joystic.SetActive(true);
            //callback o delegate para saber cuando se inicializar un jugador
            if (JugadorInicializado != null)
                JugadorInicializado(gameObject);
        }
    }

        
}
