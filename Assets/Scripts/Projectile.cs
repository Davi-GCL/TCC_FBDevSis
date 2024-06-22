using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KartGame.KartSystems
{
    public class Projectile : MonoBehaviour
    {
        public string id;
        public float ActivationDelay = 2f;

        private float StartTime;

        void Awake()
        {
            Destroy(gameObject, 10f);
            GetComponent<SphereCollider>().enabled = false;
            this.StartTime = Time.time;
        }

        private void Update()
        {
            if((Time.time - StartTime) > this.ActivationDelay)
            {
                GetComponent<SphereCollider>().enabled = true;
            }
        }

        void DestroyImmediately(GameObject go)
        {
            Destroy(go);
        }

        // Update is called once per frame
        //void Update()
        //{

        //}

        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.tag.Contains("Player"))
        //    {
        //        Debug.Log("ESCOREGOU NA BANANINHA");
        //        //collision.gameObject.GetComponent<ArcadeRolima>().Reset();
        //        collision.gameObject.transform.position += new Vector3(0, 3f);
        //        //collision.gameObject.transform.localRotation = collision.gameObject.transform.localRotation.Add(new Quaternion(2f * Time.deltaTime, 0, 0, 0));
        //    }
        //}


    }
}
