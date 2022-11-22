using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CosmoSimClone
{



    public class DataBase : MonoBehaviour
    {
        public List<Item> m_Items = new List<Item>();
    }

    [System.Serializable]

    public class Item
    {
        /// <summary>
        /// ������������� ��������
        /// </summary>
        public int m_ID;

        /// <summary>
        /// ����������� ��������
        /// </summary>
        public Sprite m_Image;

        /// <summary>
        /// �������� ��������
        /// </summary>    
        public string m_Name;
    }
}
