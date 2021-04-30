using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetect : MonoBehaviour
{
    [SerializeField] public List<GameObject> ballon;
    [SerializeField] RoombaController _controller;
    private int count = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Knife"))
        {
            destroyBallon();
        }
    }

    public void destroyBallon()
    {
        Destroy(ballon[count]);
        count++;
        _controller.activateInvincibility();
        
    }
}
