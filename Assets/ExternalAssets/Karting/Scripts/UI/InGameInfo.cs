using KartGame.KartSystems;
using TMPro;
using UnityEngine;

namespace KartGame.UI
{
    public class InGameInfo : MonoBehaviour
    {
        public TextMeshProUGUI Speed;
        public bool AutoFindKart = true;
        public ArcadeRolima RolimaController;


        void Start()
        {
            if (AutoFindKart)
            {
                ArcadeRolima foundKart = FindObjectOfType<ArcadeRolima>();
                RolimaController = foundKart;
            }

            if (!RolimaController)
            {
                gameObject.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
            float speed = RolimaController.Rigidbody.velocity.magnitude;
            Speed.text = string.Format($"{Mathf.FloorToInt(speed * 3.6f)} km/h");
            Speed.text += string.Format($"\n{speed:0.0} m/s");
        }
    }
}