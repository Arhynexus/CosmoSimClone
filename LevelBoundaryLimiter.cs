using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CosmoSimClone
{
    /// <summary>
    /// Ограничитель позиции. Работает в связке со скриптом LevelBoundary, если таковой имеется на сцене
    /// Кидается на объект, который надо ограничить
    /// </summary>
    
    public class LevelBoundaryLimiter : MonoBehaviour
    {


        void Update()
        {
            if (LevelBoundary.Instance == null) return;

            var levelBoundary = LevelBoundary.Instance;
            var r = levelBoundary.Radius;

            if(transform.position.magnitude > r)
            {
                if(levelBoundary.LimitMode == LevelBoundary.Mode.Limit)
                {
                    transform.position = transform.position.normalized * r;
                }

                if (levelBoundary.LimitMode == LevelBoundary.Mode.Teleport)
                {
                    transform.position = -transform.position.normalized * r;
                }
            }
        }
    }
}


