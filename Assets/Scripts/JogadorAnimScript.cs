using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KartGame.KartSystems;

public class JogadorAnimScript : MonoBehaviour
{
    public Animator animatorController;
    public Transform playerTransform;
    [Space]
    public ArcadeRolima kartScript;
    public PlayerPowerupInventory playerPowerupInventory;

    [System.Serializable]
    public class PlayerImpulse
    {
        public float Interval = 0.5f;
        public float MaxAnimMultiplier = 6f;
        public float AnimSpeedCurve = 0.5f;
        public float AnimMultiplier = 0f;
        internal float ElapsedTime;
        public void SetAnimSpeed(float curve, float max)
        {
            this.AnimSpeedCurve = curve;
            this.MaxAnimMultiplier = max;
        }
        public void SumAnimMultiplier(float value)
        {
            this.AnimMultiplier += this.AnimMultiplier < this.MaxAnimMultiplier ? value : 0f;
        }
        public void ReduceAnimMultiplier(float value)
        {
            this.AnimMultiplier -= this.AnimMultiplier > 0 ? value : 0f;
            this.AnimMultiplier = this.AnimMultiplier < 0f ? 0f : this.AnimMultiplier;
        }
    }

    public PlayerImpulse playerImpulse;

    //public InputData Input { get; private set; }
    IInput[] m_Inputs;

    void Start()
    {
        playerImpulse.ElapsedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //GatherInputs();

        if (kartScript != null)
        {
            var curve = kartScript.m_FinalStats.ImpulseAnimCurve;
            var maxSpeed = kartScript.m_FinalStats.ImpulseAnimMaxSpeed;
            playerImpulse.SetAnimSpeed(curve, maxSpeed);
        }

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow))
        {
            playerImpulse.ElapsedTime = Time.time;
            playerImpulse.SumAnimMultiplier(playerImpulse.AnimSpeedCurve);
        }
        else
        {
            if ((Time.time - playerImpulse.ElapsedTime) > playerImpulse.Interval)
            {
                playerImpulse.ElapsedTime = Time.time;
                playerImpulse.ReduceAnimMultiplier((playerImpulse.MaxAnimMultiplier / 2));
            }
        }
        animatorController.SetFloat("impulso", playerImpulse.AnimMultiplier);

    }

    public void TriggerHandOnGround()
    {
        if (kartScript is not null)
            StartCoroutine(NotifyPushActionCoroutine());
    }
    IEnumerator NotifyPushActionCoroutine()
    {
        kartScript.SendMessage("PlayerPushing", true);

        yield return new WaitForSeconds(0.1f);

        kartScript.SendMessage("PlayerPushing", false);
    }
    public void TriggerHandReleaseItem()
    {
        playerPowerupInventory.SendMessage("ReleaseItem");
    }
    public void ExitHandOnGround()
    {
        //kartObject.gameObject.SendMessage("PlayerPushing", false);
    }

    //void GatherInputs()
    //{
    //    // reset input
    //    Input = new InputData();


    //    // gather nonzero input from our sources
    //    for (int i = 0; i < m_Inputs.Length; i++)
    //    {
    //        Input = m_Inputs[i].GenerateInput();

    //    }
    //}
}
