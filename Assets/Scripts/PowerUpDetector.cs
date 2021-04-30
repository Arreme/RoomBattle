using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpDetector : MonoBehaviour
{
    public RoombaController roomba;
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("collide");
        if (other.gameObject.tag.Equals("Player"))
        {
            if(other.gameObject.GetComponentInParent<RoombaController>()._currentPowerUp is NoPowerUp)
            {
                other.gameObject.GetComponentInParent<RoombaController>().getPowerUp(new RunPowerUp());
                Debug.Log("collide with player");
                destroyPowerUp();
            }    
        }
    }

    public void destroyPowerUp()
    {
        Destroy(this.gameObject);
    }
}
