using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class NetworkUIManager : MonoBehaviour
{
    public CesiumForUnity.Cesium3DTileset tileset;
    void Start()
    {
        tileset.url = "https://tile.googleapis.com/v1/3dtiles/root.json?key=" + Secrets.API_KEY;

        /* Secrets sample
         public static class Secrets
        {
            public const string API_KEY = "your_api_key_here";
        }
         */
    }
}
