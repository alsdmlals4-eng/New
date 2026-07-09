using UnityEngine;
using UnityEngine.EventSystems;

namespace MyLittleBoat
{
    public class FirstPersonLook : MonoBehaviour
    {
        [SerializeField] private float lookSensitivity = 0.16f;
        [SerializeField] private float minPitch = -12f;
        [SerializeField] private float maxPitch = 18f;

        private float yaw;
        private float pitch = 6f;
        private Vector2 lastPointerPosition;
        private bool isDragging;

        private void Update()
        {
            HandleMouseLook();
            HandleTouchLook();
            transform.localRotation = Quaternion.Euler(pitch, yaw, 0f);
        }

        /// <summary>
        /// Resets the camera to the stable forward view used by the boat.
        /// </summary>
        public void ResetLook()
        {
            yaw = 0f;
            pitch = 6f;
        }

        private void HandleMouseLook()
        {
            if (Input.touchCount > 0)
            {
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject())
                {
                    return;
                }

                isDragging = true;
                lastPointerPosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (!isDragging)
            {
                return;
            }

            Vector2 currentPosition = Input.mousePosition;
            ApplyPointerDelta(currentPosition - lastPointerPosition);
            lastPointerPosition = currentPosition;
        }

        private void HandleTouchLook()
        {
            if (Input.touchCount == 0)
            {
                return;
            }

            Touch touch = Input.GetTouch(0);
            if (EventSystem.current != null && EventSystem.current.IsPointerOverGameObject(touch.fingerId))
            {
                return;
            }

            if (touch.phase == TouchPhase.Moved)
            {
                ApplyPointerDelta(touch.deltaPosition);
            }
        }

        private void ApplyPointerDelta(Vector2 delta)
        {
            yaw += delta.x * lookSensitivity;
            pitch -= delta.y * lookSensitivity;
            yaw = Mathf.Clamp(yaw, -55f, 55f);
            pitch = Mathf.Clamp(pitch, minPitch, maxPitch);
        }
    }
}
