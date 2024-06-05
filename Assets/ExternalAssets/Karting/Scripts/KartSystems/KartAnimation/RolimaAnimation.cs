using System;
using UnityEngine;

namespace KartGame.KartSystems
{
    [DefaultExecutionOrder(100)]
    public class RolimaAnimation : MonoBehaviour
    {
        [Serializable]
        public class Wheel
        {
            [Tooltip("A reference to the transform of the wheel.")]
            public Transform wheelTransform;
            [Tooltip("A reference to the WheelCollider of the wheel.")]
            public WheelCollider wheelCollider;

            Quaternion m_SteerlessLocalRotation;

            public void Setup() => m_SteerlessLocalRotation = wheelTransform.localRotation;

            public void StoreDefaultRotation() => m_SteerlessLocalRotation = wheelTransform.localRotation;
            public void SetToDefaultRotation() => wheelTransform.localRotation = m_SteerlessLocalRotation;
        }

        [Serializable]
        public class KartAxis
        {
            [Tooltip("A reference to the transform of the axis.")]
            public Transform axisTransform;
            //[Tooltip("A reference to the WheelCollider of the wheel.")]
            //public WheelCollider axisCollider;

            Quaternion m_SteerlessLocalRotation;

            public void Setup() => m_SteerlessLocalRotation = axisTransform.localRotation;

            public void StoreDefaultRotation() => m_SteerlessLocalRotation = axisTransform.localRotation;
            public void SetToDefaultRotation() => axisTransform.localRotation = m_SteerlessLocalRotation;
        }

        [Tooltip("What kart do we want to listen to?")]
        public ArcadeRolima kartController;

        [Space]
        [Tooltip("The damping for the appearance of steering compared to the input.  The higher the number the less damping.")]
        public float steeringAnimationDamping = 10f;

        [Space]
        [Tooltip("The maximum angle in degrees that the front wheels can be turned away from their default positions, when the Steering input is either 1 or -1.")]
        public float maxSteeringAngle;

        [Space]
        [Tooltip("The maximum angle in degrees that the front wheels can be turned away from their default positions, when the Steering input is either 1 or -1.")]
        public float maxSteeringAngleAxis = 30f;

        [Tooltip("Information referring to the front left wheel of the kart.")]
        public Wheel? frontLeftWheel;
        [Tooltip("Information referring to the front right wheel of the kart.")]
        public Wheel? frontRightWheel;
        [Tooltip("Information referring to the rear left wheel of the kart.")]
        public Wheel rearLeftWheel;
        [Tooltip("Information referring to the rear right wheel of the kart.")]
        public Wheel rearRightWheel;

        public KartAxis frontAxis;


        float m_SmoothedSteeringInput;
        float auxRotacao;

        void Start()
        {
            frontLeftWheel?.Setup();
            frontRightWheel?.Setup();
            rearLeftWheel?.Setup();
            rearRightWheel?.Setup();
            frontAxis.Setup();
        }

        void FixedUpdate()
        {
            m_SmoothedSteeringInput = Mathf.MoveTowards(m_SmoothedSteeringInput, kartController.Input.TurnInput,
                steeringAnimationDamping * Time.deltaTime);

            // Steer front wheels
            float rotationAngle = m_SmoothedSteeringInput * maxSteeringAngle;

            frontLeftWheel.wheelCollider.steerAngle = rotationAngle;
            frontRightWheel.wheelCollider.steerAngle = rotationAngle;

            float rotationAngleAxis = m_SmoothedSteeringInput * maxSteeringAngleAxis;
            rotationAngleAxis = Math.Clamp(rotationAngleAxis, -30f, 30f);

            frontAxis.axisTransform.localRotation = Quaternion.Euler(new Vector3(0f, rotationAngleAxis, 0f));
            //frontAxis.axisTransform.localRotation.SetEulerAngles();

            // Update position and rotation from WheelCollider
            //UpdateWheelFromCollider(frontLeftWheel);
            //UpdateWheelFromCollider(frontRightWheel);
            UpdateWheelFromCollider(rearLeftWheel);
            UpdateWheelFromCollider(rearRightWheel);

            //UpdateAxisFromWheel(frontLeftWheel, frontAxis);
        }

        void LateUpdate()
        {
            // Update position and rotation from WheelCollider
            //UpdateWheelFromCollider(frontLeftWheel);
            //UpdateWheelFromCollider(frontRightWheel);
            UpdateWheelFromCollider(rearLeftWheel);
            UpdateWheelFromCollider(rearRightWheel);

            //UpdateAxisFromWheel(frontLeftWheel, frontAxis);
        }

        void UpdateWheelFromCollider(Wheel wheel)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            wheel.wheelTransform.position = position;
            wheel.wheelTransform.rotation = rotation;
        }

        void UpdateAxisFromWheel(Wheel wheel, KartAxis axis)
        {
            wheel.wheelCollider.GetWorldPose(out Vector3 position, out Quaternion rotation);
            axis.axisTransform.rotation = rotation;
        }
    }
}
