using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetect : MonoBehaviour
{
    [SerializeField] GameObject ballon;
    [SerializeField] RoombaController _controller;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Knife"))
        {
            destroyBallon();
        }
    }

    public void destroyBallon()
    {
        Destroy(ballon);
        _controller.activateInvincibility();
    }
}
