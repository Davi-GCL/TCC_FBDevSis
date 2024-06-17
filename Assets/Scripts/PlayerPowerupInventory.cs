using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace KartGame.KartSystems
{
    public class PlayerPowerupInventory : MonoBehaviour
    {
        public List<ArcadeRolima.StatPowerup> StoredPowerups;

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