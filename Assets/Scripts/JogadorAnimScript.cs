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
    public GameObject kartObject;

    private Vector3 _initialPlayerPosition;

    [System.Serializable]
    public class PlayerImpulse
    {
        public float Interval = 0.5f;
        public float MaxAnimMultiplier = 6f;
        public float AnimMultiplier = 0f;
        internal float ElapsedTime;

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
        _initialPlayerPosition = new Vector3(
            playerTransform.localPosition.x,
            playerTransform.localPosition.y,
            playerTransform.localPosition.z
        );

        playerImpulse.ElapsedTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        //GatherInputs();

        if (Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) 
        {
            playerImpulse.ElapsedTime = Time.time;
            playerImpulse.SumAnimMultiplier(0.5f);
        }
        else
        {
            if ((Time.time - playerImpulse.ElapsedTime) > playerImpulse.Interval)
            {
                playerImpulse.ElapsedTime = Time.time;
                playerImpulse.ReduceAnimMultiplier(3f);
            }
        }
        animatorController.SetFloat("impulso", playerImpulse.AnimMultiplier);

    }

    public void TriggerHandOnGround()
    {
        if(kartObject is not null)
            StartCoroutine(NotifyPushActionCoroutine());
    }
    IEnumerator NotifyPushActionCoroutine()
    {
        kartObject.gameObject.SendMessage("PlayerPushing", true);

        yield return new WaitForSeconds(0.1f);

        kartObject.gameObject.SendMessage("PlayerPushing", false);
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
