using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Parachuting
{
    public enum Direction { Left, Right }

    public class Parachute : MonoBehaviour
    {

        [SerializeField]
        Transform parachuter;
        [SerializeField]
        Transform meshContainer;
        Rigidbody parachuteRigidbody;
        [SerializeField]
        float openingDuration;
        [SerializeField]
        float defaultResistance;
        [SerializeField]
        float turningSpeed;
        float currentResistance;
        bool isOpen;

        void Start()
        {
            parachuteRigidbody = GetComponent<Rigidbody>();
            currentResistance = 0;
            isOpen = false;
            gameObject.SetActive(false);
        }

        void Update()
        {
            parachuteRigidbody.AddForce(-1 * currentResistance * parachuteRigidbody.velocity.magnitude * parachuteRigidbody.velocity);
            //print("parachute speed:" + parachuteRigidbody.velocity.magnitude + " " + currentResistance);
        }

        public void Open()
        {
            if (!isOpen)
            {
                transform.parent = null;
                parachuter.parent = transform;
                gameObject.SetActive(true);
                StartCoroutine(OpenParachute());
                isOpen = true;
            }
        }

        public void Turn(Direction direction)
        {
            if (isOpen)
            {
                switch (direction)
                {
                    case Direction.Left:
                        transform.Rotate(Vector3.down, turningSpeed * Time.deltaTime);
                        break;
                    case Direction.Right:
                        transform.Rotate(Vector3.up, turningSpeed * Time.deltaTime);
                        break;
                }
            }
        }

        IEnumerator OpenParachute()
        {
            float t = 0;

            while (t < 1)
            {
                meshContainer.localScale = Vector3.Lerp(Vector3.zero, Vector3.one, t);
                currentResistance = Mathf.Lerp(0, defaultResistance, t);
                t += Time.deltaTime / openingDuration;
                yield return null;
            }

            currentResistance = defaultResistance;
        }
    }
}