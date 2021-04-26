using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelInitializer : MonoBehaviour
{
    [SerializeField]
    private Transform[] playerSpawn;
    [SerializeField]
    private GameObject playerPrefab;

    private void Start()
    {
        var playerConfig = PlayerConfigManager.Instance.GetPlayerConfigs().ToArray();
        for (int i = 0; i < playerConfig.Length; i++)
        {
            var player = Instantiate(playerPrefab, playerSpawn[i]);
            player.GetComponent<InputManager>().InitializePlayer(playerConfig[i]);
        }
    }
}
