﻿// Rigidbody Follow|Utilities|90061
namespace VRTK
{
    using UnityEngine;

    /// <summary>
    /// Changes one game object's rigidbody to follow another game object's rigidbody.
    /// </summary>
    [AddComponentMenu("VRTK/Scripts/Utilities/Object Follow/VRTK_RigidbodyFollow")]
    public class VRTK_RigidbodyFollow : VRTK_ObjectFollow
    {
        /// <summary>
        /// Specifies how to position and rotate the rigidbody.
        /// </summary>
        public enum MovementOption
        {
            /// <summary>
            /// Use Rigidbody.position and Rigidbody.rotation.
            /// </summary>
            Set,
            /// <summary>
            /// Use Rigidbody.MovePosition and Rigidbody.MoveRotation.
            /// </summary>
            Move,
            /// <summary>
            /// Use Rigidbody.AddForce(Vector3) and Rigidbody.AddTorque(Vector3).
            /// </summary>
            Add
        }

        [Tooltip("Specifies how to position and rotate the rigidbody.")]
        public MovementOption movementOption = MovementOption.Set;

        protected Rigidbody rigidbodyToFollow;
        protected Rigidbody rigidbodyToChange;

        public override void Follow()
        {
            CacheRigidbodies();
            base.Follow();
        }

        protected virtual void OnDisable()
        {
            rigidbodyToFollow = null;
            rigidbodyToChange = null;
        }

        protected virtual void FixedUpdate()
        {
            Follow();
        }

        protected override Vector3 GetPositionToFollow()
        {
            return rigidbodyToFollow.position;
        }

        protected override void SetPositionOnGameObject(Vector3 newPosition)
        {
            switch (movementOption)
            {
                case MovementOption.Set:
                    rigidbodyToChange.position = newPosition;
                    break;
                case MovementOption.Move:
                    rigidbodyToChange.MovePosition(newPosition);
                    break;
                case MovementOption.Add:
                    // TODO: Test if this is correct
                    rigidbodyToChange.AddForce(newPosition - rigidbodyToChange.position);
                    break;
            }
        }

        protected override Quaternion GetRotationToFollow()
        {
            return rigidbodyToFollow.rotation;
        }

        protected override void SetRotationOnGameObject(Quaternion newRotation)
        {
            switch (movementOption)
            {
                case MovementOption.Set:
                    rigidbodyToChange.rotation = newRotation;
                    break;
                case MovementOption.Move:
                    rigidbodyToChange.MoveRotation(newRotation);
                    break;
                case MovementOption.Add:
                    // TODO: Test if this is correct
                    rigidbodyToChange.AddTorque(newRotation * Quaternion.Inverse(rigidbodyToChange.rotation).eulerAngles);
                    break;
            }
        }

        protected override Vector3 GetScaleToFollow()
        {
            return rigidbodyToFollow.transform.localScale;
        }

        protected virtual void CacheRigidbodies()
        {
            if (gameObjectToFollow == null || gameObjectToChange == null
                || (rigidbodyToFollow != null && rigidbodyToChange != null))
            {
                return;
            }

            rigidbodyToFollow = gameObjectToFollow.GetComponent<Rigidbody>();
            rigidbodyToChange = gameObjectToChange.GetComponent<Rigidbody>();
        }
    }
}