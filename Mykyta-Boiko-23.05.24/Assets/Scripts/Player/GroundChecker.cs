using UnityEngine;

namespace PlayerLogic
{
    public class GroundChecker : MonoBehaviour
    {
        [SerializeField] private LayerMask groundMask; 
        public bool IsGrounded { get; private set; } = true;

        private void OnTriggerEnter(Collider other)
        {
            if (IsInLayerMask(other.gameObject.layer, groundMask))
            {
                IsGrounded = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (IsInLayerMask(other.gameObject.layer, groundMask))
            {
                IsGrounded = false;
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (IsInLayerMask(other.gameObject.layer, groundMask))
            {
                IsGrounded = true;
            }
        }

        private bool IsInLayerMask(int layer, LayerMask mask)
        {
            return (mask.value & (1 << layer)) > 0;
        }
    }
}