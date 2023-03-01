using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CosmoSimClone
{



    public class Bag : MonoBehaviour
    {
        public int coins;

        [HideInInspector] public UnityEvent OnChangeCoins;

        // Start is called before the first frame update
        void Start()
        {
            OnChangeCoins.Invoke();
        }
        public int Coins()
        {
            return coins;
        }

        public void GetCoins()
        {
            Coins();
        }

        public void AddCoins(int amount)
        {
            coins += amount;
            OnChangeCoins.Invoke();
        }

        public void RemoveCoins(int amount)
        {
            coins -= amount;
            OnChangeCoins.Invoke();
        }
        // Update is called once per frame
        void Update()
        {

        }
    }
}
