using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace TanksPUN
{
    public class GameManager : Photon.PunBehaviour
    {
        public static GameManager instance;
        public static GameObject localPlayer;

        void Awake()
        {
            if (instance != null) {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;

            PhotonNetwork.automaticallySyncScene = true;
        }

        void Start()
        {
            PhotonNetwork.ConnectUsingSettings("TanksPUN_v1.0");
        }

        void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        public override void OnConnectedToPhoton()
        {
            DebugLog("Connected to Photon");
        }

        public override void OnConnectedToMaster()
        {
            DebugLog("Connected to Master");
        }

        public override void OnFailedToConnectToPhoton(DisconnectCause cause)
        {
            DebugLog("Failed Connect");
        }

        public override void OnDisconnectedFromPhoton()
        {
            DebugLog("Disconnected");
        }

        public void JoinGameRoom()
        {
            RoomOptions options = new RoomOptions();
            options.MaxPlayers = 6;
            PhotonNetwork.JoinOrCreateRoom("Kingdom", options, null);
        }

        public override void OnJoinedRoom()
        {
            if(PhotonNetwork.isMasterClient)
            {
                DebugLog("Created room!!");
                PhotonNetwork.LoadLevel("GameScene");
            } else {
                DebugLog("Joined room!!");
            }
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if(!PhotonNetwork.inRoom) return;

            localPlayer = PhotonNetwork.Instantiate(
                "Player",
                new Vector3(0,0.5f,0),
                Quaternion.identity, 0);

            Debug.Log(localPlayer.GetInstanceID());
        }

        void DebugLog(string msg)
        {
            Debug.Log("Photon: " + msg);
        }
    }
}