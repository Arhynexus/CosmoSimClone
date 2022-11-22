using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    
namespace CosmoSimClone
{
    public class Pickable : MonoBehaviour
    {
        protected virtual void OnTriggerEnter(Collider other)
        {
            Destructible fps = other.GetComponent<Destructible>();

            if (fps != null)
            {
                Destroy(gameObject);
            }

        }
    }
}


