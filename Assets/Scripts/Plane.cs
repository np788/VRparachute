using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parachuting
{
    public class Plane : MonoBehaviour
    {
        [SerializeField]
        float speed;
        [SerializeField]
        float propellerSpeed;
        [SerializeField]
        Transform propeller;

        void FixedUpdate()
        {
            Vector3 translationVector = new Vector3(0, 0, speed * Time.fixedDeltaTime);
            transform.Translate(translationVector);
        }

        void Update()
        {
            propeller.Rotate(propellerSpeed * Vector3.up);
        }

        public Vector3 GetWorldSpaceVelocity()
        {
            Vector3 velocity = transform.TransformDirection(new Vector3(0, 0, speed));
            return velocity;
        }
    }
}
