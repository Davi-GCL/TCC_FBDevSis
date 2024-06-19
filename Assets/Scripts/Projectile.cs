using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class Projectile : MonoBehaviour
    {

        // Start is called before the first frame update
        void Start()
        {
            Destroy(gameObject, 10f);
        }

        // Update is called once per frame
        void Update()
        {

        }

        public void SetDirection(Vector3 direction)
        {

        }
    }
}
