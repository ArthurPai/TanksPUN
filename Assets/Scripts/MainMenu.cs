using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TanksPUN
{
    public class MainMenu : Photon.PunBehaviour
    {
        static MainMenu instance;
        GameObject ui;
        Button joinGameButton;

        void Awake()
        {
            if (instance != null) {
                DestroyImmediate(gameObject);
                return;
            }
            instance = this;

            ui = transform.FindAnyChild<Transform>("UI").gameObject;
            joinGameButton = transform.FindAnyChild<Button>("JoinGameButton");

            ui.SetActive(true);
            joinGameButton.interactable = false;
        }

        void OnEnable() {
            SceneManager.sceneLoaded += OnSceneLoaded;
        }

        void OnDisable() {
            SceneManager.sceneLoaded -= OnSceneLoaded;
        }

        private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            ui.SetActive(!PhotonNetwork.inRoom);
        }

        public override void OnConnectedToMaster()
        {
            joinGameButton.interactable = true;
        }
    }
}