using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksPUN {
    public class GameManager : Photon.PunBehaviour {
        public static GameManager instance;

        void Awake()
        {
            if(instance != null)
            {
                DestroyImmediate(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }
}