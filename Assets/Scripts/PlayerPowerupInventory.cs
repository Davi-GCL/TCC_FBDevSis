using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KartGame.KartSystems
{
    [Serializable]
    public class PowerupItem
    {
        public string PowerUpID;
        public bool onSelf;
        public float ElapsedTime;
        public float MaxTime;
        public ArcadeRolima.Stats modifiers;
        public Action PowerupAction;
    }
    public class PlayerPowerupInventory : MonoBehaviour
    {
        public IList<PowerupItem> StoredItens = new List<PowerupItem>();
        [Space]
        public GameObject ProjectileOrigin;
        public Transform ProjectilePrefab;

        [Space]
        public GameObject KartObject;
        public InputData Input { get; private set; }
        // the input sources that can control the kart
        IInput[] m_Inputs;

        void Start()
        {
            m_Inputs = GetComponents<IInput>();
        }

        // Update is called once per frame
        void Update()
        {
            GatherInputs();

            if (UnityEngine.Input.GetKeyUp(KeyCode.E)) UseStoredPowerup();
        }

        void UseStoredPowerup()
        {
            ThrowProjectile();
            var mainScript = gameObject.GetComponent<ArcadeRolima>();

            if (mainScript && StoredItens.Count >= 1)
            {
                if (StoredItens[0].onSelf)
                {
                    var statPowerUp = ItemToPowerupMap(StoredItens[0]);
                    mainScript.AddPowerup(statPowerUp);
                    StoredItens.RemoveAt(0);
                }
                //ThrowProjectile();
            }
        }
        void ThrowProjectile()
        {
            var originTransform = ProjectileOrigin.transform;
            var projectileObj = Instantiate(ProjectilePrefab, originTransform.position, originTransform.rotation);

            var originVelocity = KartObject.GetComponent<Rigidbody>().velocity;
            Debug.Log(originVelocity);

            projectileObj.GetComponent<Rigidbody>().velocity = originVelocity + (originTransform.forward * 15f);
        }
        ArcadeRolima.StatPowerup ItemToPowerupMap(PowerupItem item)
        {
            return new ArcadeRolima.StatPowerup()
            {
                ElapsedTime = item.ElapsedTime,
                modifiers = item.modifiers,
                PowerUpID = item.PowerUpID,
                MaxTime = item.MaxTime
            };
        }
        void GatherInputs()
        {
            // reset input
            Input = new InputData();

            // gather nonzero input from our sources
            for (int i = 0; i < m_Inputs.Length; i++)
            {
                Input = m_Inputs[i].GenerateInput();
            }
        }
    }
}