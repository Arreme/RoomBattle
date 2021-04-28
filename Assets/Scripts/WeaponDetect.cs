using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponDetect : MonoBehaviour
{
    [SerializeField] GameObject ballon;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name.Equals("Knife"))
        {
            destroyBallon();
        }
    }

    private void destroyBallon()
    {
        Destroy(ballon);
    }
}
