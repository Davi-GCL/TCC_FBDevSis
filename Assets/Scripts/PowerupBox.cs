using KartGame.KartSystems;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;

public class PowerupBox : MonoBehaviour
{

    public PowerupItem boostStats = new PowerupItem()
    {
        MaxTime = 10
    };
    public bool isCoolingDown { get; private set; }
    public float lastActivatedTimestamp { get; private set; }

    public float cooldown = 5f;

    public bool disableGameObjectWhenActivated;
    public UnityEvent onPowerupActivated;
    public UnityEvent onPowerupFinishCooldown;

    private IList<PowerupItem> AllPowerupsList = new List<PowerupItem>()
    {
        new PowerupItem()
        {
            PowerUpID = "1",
            MaxTime = 3,
            onSelf = true,
            modifiers = new ArcadeRolima.Stats
            {
                TopSpeed = 5,
                ImpulseAnimMaxSpeed = 4f,
                ImpulseAnimCurve = 4f
            }
        }
    };

    private void Awake()
    {
        lastActivatedTimestamp = -9999f;

        this.boostStats = SortRandomItem<PowerupItem>(AllPowerupsList);
    }


    private void Update()
    {
        if (isCoolingDown)
        {

            if (Time.time - lastActivatedTimestamp > cooldown)
            {
                //finished cooldown!
                isCoolingDown = false;
                onPowerupFinishCooldown.Invoke();
                this.gameObject.SetActive(true);
            }

        }
    }


    private void OnTriggerEnter(Collider other)
    {
        if (isCoolingDown) return;

        var rb = other.attachedRigidbody;
        if (rb)
        {
            //var kart = rb.GetComponent<ArcadeRolima>();
            var kart = rb.GetComponent<PlayerPowerupInventory>();

            if (kart)
            {
                lastActivatedTimestamp = Time.time;
                //kart.AddPowerup(this.boostStats);
                kart.StoredItens.Add(this.boostStats);
                onPowerupActivated.Invoke();
                isCoolingDown = true;


                if (disableGameObjectWhenActivated) this.gameObject.SetActive(false);
            }
        }
    }

    T SortRandomItem<T>(IList<T> itemList)
    {
        var max = itemList.Count;
        var index = new System.Random().Next(0, max);

        return itemList[index];
    }
}
