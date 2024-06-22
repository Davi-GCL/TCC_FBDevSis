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
        public Transform ProjectilePrefab;
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

        public Animator AnimatorController;
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
            var mainScript = gameObject.GetComponent<ArcadeRolima>();

            if (mainScript && StoredItens.Count >= 1)
            {
                if (StoredItens[0].onSelf)
                {
                    Debug.Log($"Usei o item:{StoredItens[0].PowerUpID}");
                    var statPowerUp = ItemToPowerupMap(StoredItens[0]);
                    mainScript.AddPowerup(statPowerUp);
                }
                else
                {
                    this.CurrentItem = StoredItens[0];
                    UseThrowable();
                }
                StoredItens.RemoveAt(0);
            }
        }
        void UseThrowable()
        {
            AnimatorController.SetBool("arremessando", true);
        }
        private PowerupItem CurrentItem;
        //Chamado pelo JogadorAnimScript.TriggerHandReleaseItem() quando a animação de arremesso ativa o evento 
        void ReleaseItem()
        {
            var originTransform = ProjectileOrigin.transform;
            var projectileObj = Instantiate(CurrentItem.ProjectilePrefab, originTransform.position, originTransform.rotation);
            
            projectileObj.GetComponent<Projectile>().id = CurrentItem.PowerUpID;

            var originVelocity = KartObject.GetComponent<Rigidbody>().velocity;
            Debug.Log(originVelocity);

            projectileObj.GetComponent<Rigidbody>().velocity = originVelocity + (originTransform.forward * 15f);

     
            AnimatorController.SetBool("arremessando", false);
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