using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksPUN
{
    public class GameManager : Photon.PunBehaviour
    {
        public static GameManager instance;

        void Awake()
        {
            if (instance != null) {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;
        }

        void Start()
        {
            PhotonNetwork.ConnectUsingSettings("TanksPUN_v1.0");
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

        void DebugLog(string msg)
        {
            Debug.Log("Photon: " + msg);
        }
    }
}