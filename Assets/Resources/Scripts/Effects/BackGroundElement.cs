using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    [RequireComponent(typeof(MeshRenderer))]
    
    public class BackGroundElement : MonoBehaviour
    {
        [Range(0f, 4f)]
        [SerializeField] private float m_parallaxPower;

        [SerializeField] private float m_textureScale;

        private Material m_quadMaterial;
        private Vector2 m_initialOffSet;

        private void Start()
        {
            m_quadMaterial = GetComponent<MeshRenderer>().material;
            m_initialOffSet = UnityEngine.Random.insideUnitCircle;

            m_quadMaterial.mainTextureScale = Vector2.one * m_textureScale;
        }

        private void Update()
        {
            Vector2 offset = m_initialOffSet;

            offset.x += transform.position.x / transform.localScale.x / m_parallaxPower;
            offset.y += transform.position.y / transform.localScale.y / m_parallaxPower;

            m_quadMaterial.mainTextureOffset = offset;
        }
    }
}


