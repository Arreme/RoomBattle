using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    private List<GameObject> _players;
    private float distance = 20f;

    public void Awake()
    {
        _players = new List<GameObject>();
    }

    public void AddPlayer(GameObject obj)
    {
        _players.Add(obj);
    }

    public void Explosion(GameObject obj)
    {
        foreach (GameObject _target in _players)
        {
            if (ReferenceEquals(obj,_target)) continue;
            if (Vector3.Distance(_target.transform.position,obj.transform.position) < distance)
            {
                Vector3 direction = Vector3.Normalize(_target.transform.position - obj.transform.position);
                Debug.Log(direction);
                _target.GetComponent<CustomPhysics>().addForce(new Vector2(direction.x,direction.z), 5000);
            }
        }
    }
}
