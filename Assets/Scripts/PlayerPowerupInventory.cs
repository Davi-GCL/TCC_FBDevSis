using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KartGame.KartSystems
{
    public class PowerupItem
    {
        public bool onSelf;
        public ArcadeRolima.Stats modifiers;
        public Action PowerupAction;
        public string PowerUpID;
        public float ElapsedTime;
        public float MaxTime;
    }
    public class PlayerPowerupInventory : MonoBehaviour
    {
        public List<PowerupItem> StoredItens;

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

            if (Input.UsePowerup) UseStoredPowerup();
        }

        void UseStoredPowerup()
        {
            var mainScript = gameObject.GetComponent<ArcadeRolima>();

            if (mainScript && StoredItens.Count >= 1)
            {
                if (StoredItens[0].onSelf)
                {
                    var statPowerUp = ItemToPowerupMap(StoredItens[0]);
                    mainScript.AddPowerup(statPowerUp);
                    StoredItens.RemoveAt(0);
                    Debug.Log($"USEI O PODER: {statPowerUp.PowerUpID}");
                }
            }
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