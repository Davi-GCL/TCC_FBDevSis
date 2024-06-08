using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JogadorAnimScript : MonoBehaviour
{
    public Animator animatorController;
    public Transform playerTransform;

    [Space]
    public float sittingPositionX = 0;
    public float sittingPositionY = 0;
    public float sittingPositionZ = 0;

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
            this.AnimMultiplier += this.AnimMultiplier < this.MaxAnimMultiplier? value : 0f;
        }
        public void ReduceAnimMultiplier(float value)
        {
            this.AnimMultiplier -= this.AnimMultiplier > 0 ? value : 0f;
            this.AnimMultiplier = this.AnimMultiplier < 0f ? 0f : this.AnimMultiplier;
        }
    }

    public PlayerImpulse playerImpulse;

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
        if(Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.UpArrow)) 
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

    public bool ChangePlayerState(bool statePushing) 
    {

        if (statePushing == false) 
        {
            //playerTransform.SetLocalPositionAndRotation(_initialPlayerPosition, this.playerTransform.localRotation);
            Debug.Log(_initialPlayerPosition);
            return true;
        }
        else
        {
            //playerTransform.localPosition = new Vector3(sittingPositionX, sittingPositionY, sittingPositionZ);
            return false;
        }
    }
}
