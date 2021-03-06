﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TanksPUN
{
    public class Player : Photon.PunBehaviour
    {
        Camera playerCam;

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            playerCam = GetComponentInChildren<Camera>();

            if (!photonView.isMine) {
                playerCam.gameObject.SetActive(false);
            }
        }
    }
}