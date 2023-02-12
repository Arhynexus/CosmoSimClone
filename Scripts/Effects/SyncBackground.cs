using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncBackground : MonoBehaviour
{

    [SerializeField] private Transform m_target;
    void Update()
    {
        transform.position = new Vector3(m_target.position.x, m_target.position.y, transform.position.z);
    }
}
