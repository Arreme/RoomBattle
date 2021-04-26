using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfig> _configs;

    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
            _configs = new List<PlayerConfig>();
        }
    }

    public List<PlayerConfig> GetPlayerConfigs()
    {
        return _configs;
    }

    public void ReadyPlayer(int index)
    {
        _configs[index].IsReady = true;
        if (_configs.All(p => p.IsReady == true))
        {
            //Start Game
            SceneManager.LoadScene("TestScene");
        }
    }

    public void SetPlayerColor(int index, Material color)
    {
        _configs[index].PlayerMaterial = color;
    }

    public void HandlePlayerJoin(PlayerInput pi)
    {
        Debug.Log("Player joined: " + pi.playerIndex);
        pi.transform.SetParent(transform);
        if (!_configs.Any(p => p.PlayerIndex == pi.playerIndex))
        {
            
            _configs.Add(new PlayerConfig(pi));
        }
    }
}

public class PlayerConfig
{

    public PlayerConfig(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }

    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }
    public bool IsReady { get; set; }

    public Material PlayerMaterial { get; set; }
}
