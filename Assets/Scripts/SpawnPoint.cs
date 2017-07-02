using UnityEngine;
using System.Collections;

namespace TanksPUN
{
    public class SpawnPoint : MonoBehaviour
    {
        void Awake()
        {
            gameObject.SetActive(false);
        }
    }
}