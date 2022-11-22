using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace CosmoSimClone
{


    public class Inventory : MonoBehaviour
    {
        public DataBase m_Data;

        public List<ItemINventory> m_Items = new List<ItemINventory>();

        /// <summary>
        /// Показывает предмет в инвентаре
        /// </summary>
        public GameObject m_GameObjectShow;

        /// <summary>
        /// Игровой объект в инвентаре
        /// </summary>
        public GameObject m_GameObjectMain;

        /// <summary>
        /// Максимальное количество предметов в ячейке
        /// </summary>
        public int m_MaxCount;

        public Camera m_Camera;

        public EventSystem m_EventSystem;

        public ItemINventory m_CurrentItem;

        public RectTransform m_MovingObject;

        public Vector3 m_Offset;

        public int m_CurrentID;

        public GameObject m_InventoryBackGround;

        private void Start()
        {
            if (m_Items.Count == 0)
            {
                AddGraphics();
            }

            for (int i = 0; i < m_MaxCount; i++) //заполнение инвентаря для теста
            {
                AddItem(i, m_Data.m_Items[Random.Range(0, m_Data.m_Items.Count)], Random.Range(1, 99));
            }
            UpdateInventory();
        }

        public void Update()
        {
            if (m_CurrentID != -1)
            {
                MoveObject();
            }

            if (Input.GetKeyDown(KeyCode.I) == true)
            {
                m_InventoryBackGround.SetActive(!m_InventoryBackGround.activeSelf);
                if (m_InventoryBackGround.activeSelf == true)
                {
                    UpdateInventory();
                }
            }
        }

        public void SearchForSameItem(Item item, int count)
        {
            for (int i = 0; i < m_MaxCount; i++)
            {
                if (m_Items[i].m_ID == item.m_ID)
                {
                    if (m_Items[i].m_Count < 128) // задать количество предметов переменной
                    {
                        m_Items[i].m_Count += count;

                        if (m_Items[i].m_Count > 128)
                        {
                            count = m_Items[i].m_Count - 128;
                            m_Items[i].m_Count = 64;
                        }
                        else
                        {
                            count = 0;
                            i = m_MaxCount;
                        }
                    }
                }
            }

            if (count > 0)
            {
                for (int i = 0; i < m_MaxCount; i++)
                {
                    if (m_Items[i].m_ID == 0)
                    {
                        AddItem(i, item, count);
                        i = m_MaxCount;
                    }
                }
            }
        }
        public void AddItem(int id, Item item, int count)
        {
            m_Items[id].m_ID = item.m_ID;
            m_Items[id].m_Count = count;
            m_Items[id].m_ItemGameobject.GetComponent<Image>().sprite = item.m_Image;

            if (count > 1 && item.m_ID != 0)
            {
                m_Items[id].m_ItemGameobject.GetComponentInChildren<Text>().text = count.ToString();
            }
            else
            {
                m_Items[id].m_ItemGameobject.GetComponentInChildren<Text>().text = "";
            }
        }

        public void AddInventoryItem(int id, ItemINventory invItem)
        {
            m_Items[id].m_ID = invItem.m_ID;
            m_Items[id].m_Count = invItem.m_Count;
            m_Items[id].m_ItemGameobject.GetComponent<Image>().sprite = m_Data.m_Items[invItem.m_ID].m_Image;

            if (invItem.m_Count > 1 && invItem.m_ID != 0)
            {
                m_Items[id].m_ItemGameobject.GetComponentInChildren<Text>().text = invItem.m_Count.ToString();
            }
            else
            {
                m_Items[id].m_ItemGameobject.GetComponentInChildren<Text>().text = "";
            }
        }
        public void AddGraphics()
        {
            for (int i = 0; i < m_MaxCount; i++)
            {
                GameObject newItem = Instantiate(m_GameObjectShow, m_GameObjectMain.transform) as GameObject;
                newItem.name = i.ToString();
                ItemINventory ii = new ItemINventory();
                ii.m_ItemGameobject = newItem;

                RectTransform rt = newItem.GetComponent<RectTransform>();
                rt.localPosition = new Vector3(0, 0, 0);
                rt.localScale = new Vector3(1, 1, 1);
                newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1, 1, 1);

                Button m_TempButton = newItem.GetComponent<Button>();

                m_TempButton.onClick.AddListener(delegate { SelectObject(); });

                m_Items.Add(ii);
            }
        }
        public void UpdateInventory()
        {
            for (int i = 0; i < m_MaxCount; i++)
            {
                if (m_Items[i].m_ID != 0 && m_Items[i].m_Count > 1)
                {
                    m_Items[i].m_ItemGameobject.GetComponentInChildren<Text>().text = m_Items[i].m_Count.ToString();
                }
                else
                {
                    m_Items[i].m_ItemGameobject.GetComponentInChildren<Text>().text = "";
                }

                m_Items[i].m_ItemGameobject.GetComponent<Image>().sprite = m_Data.m_Items[m_Items[i].m_ID].m_Image;
            }
        }

        public void SelectObject()
        {
            if (m_CurrentID == -1)
            {
                m_CurrentID = int.Parse(m_EventSystem.currentSelectedGameObject.name);
                m_CurrentItem = CopyInventoryItem(m_Items[m_CurrentID]);
                m_MovingObject.gameObject.SetActive(true);
                m_MovingObject.GetComponent<Image>().sprite = m_Data.m_Items[m_CurrentItem.m_ID].m_Image;

                AddItem(m_CurrentID, m_Data.m_Items[0], 0);
            }
            else
            {
                ItemINventory II = m_Items[int.Parse(m_EventSystem.currentSelectedGameObject.name)];
                if (m_CurrentItem.m_ID != II.m_ID)
                {
                    AddInventoryItem(m_CurrentID, II);
                    AddInventoryItem(int.Parse(m_EventSystem.currentSelectedGameObject.name), m_CurrentItem);
                }
                else
                {
                    if (II.m_Count + m_CurrentItem.m_Count <= 128)
                    {
                        II.m_Count += m_CurrentItem.m_Count;
                    }
                    else
                    {
                        AddItem(m_CurrentID, m_Data.m_Items[II.m_ID], II.m_Count + m_CurrentItem.m_Count - 128);

                        II.m_Count = 128;
                    }
                }

                m_CurrentID = -1;

                m_MovingObject.gameObject.SetActive(false);

                UpdateInventory();
            }
        }

        public void MoveObject()
        {
            Vector3 pos = Input.mousePosition + m_Offset;
            pos.z = m_GameObjectMain.GetComponent<RectTransform>().position.z;
            m_MovingObject.position = m_Camera.ScreenToWorldPoint(pos);
        }

        public ItemINventory CopyInventoryItem(ItemINventory old)
        {
            ItemINventory newItem = new ItemINventory();
            newItem.m_ID = old.m_ID;
            newItem.m_ItemGameobject = old.m_ItemGameobject;
            newItem.m_Count = old.m_Count;

            return newItem;
        }
    }

    [System.Serializable]
    public class ItemINventory 
    {
        public int m_ID;

        public GameObject m_ItemGameobject;

        public int m_Count;
    }

}