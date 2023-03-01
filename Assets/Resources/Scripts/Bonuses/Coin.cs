using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoSimClone
{



    public class Coin : MonoBehaviour
    {
        [SerializeField] private int amountOfMoney;
        void Start()
        {

        }



        void Update()
        {

        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            BonusesGrabber grabber = collision.GetComponentInParent<BonusesGrabber>();
            if (grabber != null)  return;

            Bag bag = collision.GetComponentInParent<Bag>();
            if (bag != null)
            {
                bag.AddCoins(amountOfMoney);
                bag.OnChangeCoins.Invoke();
                Destroy(gameObject);
            }
        }
    }
}
