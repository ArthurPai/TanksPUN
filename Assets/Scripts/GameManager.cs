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
        GameObject defaultSpawnPoint;

        void Awake()
        {
            if (instance != null) {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;

            PhotonNetwork.automaticallySyncScene = true;

            defaultSpawnPoint = new GameObject("Default SpawnPoint");
            defaultSpawnPoint.transform.position = new Vector3(0, 0.5f, 0);
            defaultSpawnPoint.transform.SetParent(transform, false);
        }

        void Start()
        {
            PhotonNetwork.ConnectUsingSettings("TanksPUN_v1.0");
        }

        void OnEnable()
        {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable()
        {
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
            if (PhotonNetwork.isMasterClient) {
                DebugLog("Created room!!");
                PhotonNetwork.LoadLevel("GameScene");
            } else {
                DebugLog("Joined room!!");
            }
        }


        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (!PhotonNetwork.inRoom)
                return;

            var spawnPoint = GetRandomSpawnPoint();

            localPlayer = PhotonNetwork.Instantiate(
                "Player",
                spawnPoint.position,
                spawnPoint.rotation, 0);
        }

        void DebugLog(string msg)
        {
            Debug.Log("Photon: " + msg);
        }

        Transform GetRandomSpawnPoint()
        {
            var spawnPoints = GetAllObjectsOfTypeInScene<SpawnPoint>();
            if (spawnPoints.Count == 0) {
                return defaultSpawnPoint.transform;
            } else {
                return spawnPoints[Random.Range(0, spawnPoints.Count)].transform;
            }
        }

        public static List<GameObject> GetAllObjectsOfTypeInScene<T>()
        {
            List<GameObject> objectsInScene = new List<GameObject>();

            foreach (GameObject go in Resources.FindObjectsOfTypeAll(typeof(GameObject)) as GameObject[]) {
                if (go.hideFlags == HideFlags.NotEditable ||
                    go.hideFlags == HideFlags.HideAndDontSave)
                    continue;

                if (go.GetComponent<T>() != null)
                    objectsInScene.Add(go);      
            }

            return objectsInScene;
        }
    }
}