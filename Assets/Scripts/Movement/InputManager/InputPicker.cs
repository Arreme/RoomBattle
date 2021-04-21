using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class InputPicker
{
    private List<Player> _playerList;

    public InputPicker(int players)
    {
        _playerList = Enumerable.Repeat(new Player(), players).ToList();
    }

    public void RemovePlayer (int id)
    {
        _playerList.RemoveAt(id);
    }

    public Player GetPlayer(int id)
    {
        return _playerList.ElementAt(id);
    }

    public class Player
    {
        private bool _boost;
        private Vector2 _movevent;
        private bool _powerUp;

        public Player()
        {
            _boost = false;
            _powerUp = false;
            _movevent = new Vector3(0,0,0);
        }


    }
}
