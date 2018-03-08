using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parachuting
{
    public class Parachuter : MonoBehaviour
    {
        [SerializeField]
        Plane plane;
        [SerializeField]
        Parachute parachute;
        [SerializeField]
        float airResistance;
        Rigidbody parachuterRigidbody;
        enum State { OnPlane, Jumping, Parachuting }
        State state;

        void Start()
        {
            state = State.OnPlane;
            parachuterRigidbody = GetComponent<Rigidbody>();
            parachuterRigidbody.useGravity = false;
        }

        void Update()
        {
            switch (state)
            {
                case State.OnPlane:
                    if (Input.GetKeyDown("space"))
                    //if (Input.GetAxis("Axis 3") == 0 && Input.GetAxis("Axis 6") = 0)
                    {
                        JumpFromPlane();
                        state = State.Jumping;
                    }
                    break;

                case State.Jumping:
                    if (Input.GetKeyUp("space"))
                    //if (Input.GetAxis("Axis 3") > 0 || Input.GetAxis("Axis 6") > 0)
                    {
                        state = State.Parachuting;
                    }
                    break;

                case State.Parachuting:
                    ParachuteController();
                    AirResistance();
                    break;
            }
        }

        void JumpFromPlane()
        {
            transform.parent = null;
            parachuterRigidbody.velocity = plane.GetWorldSpaceVelocity() + transform.TransformDirection(new Vector3(-2, 0, 0));
            parachuterRigidbody.useGravity = true;
        }

        void ParachuteController()
        {
            float horizontalAxis = Input.GetAxis("Horizontal");
            if (horizontalAxis < 0)
            //if (Input.GetAxis("Axis 3") == 0 && Input.GetAxis("Axis 6") != 0)
            {
                parachute.Turn(Direction.Left);
            }
            if (horizontalAxis > 0)
            //if (Input.GetAxis("Axis 6") == 0 && Input.GetAxis("Axis 3") != 0)
            {
                parachute.Turn(Direction.Right);
            }

            if (Input.GetKeyDown("space"))
            //if (Input.GetAxis("Axis 3") == 0 && Input.GetAxis("Axis 6") = 0)
            {
                OpenParachute();
            }
        }

        void OpenParachute()
        {
            parachute.Open();
        }

        void AirResistance()
        {
            parachuterRigidbody.AddForce(-1 * airResistance * parachuterRigidbody.velocity.magnitude * parachuterRigidbody.velocity);
        }
    }
}