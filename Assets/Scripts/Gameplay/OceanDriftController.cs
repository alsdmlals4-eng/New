using System.Collections.Generic;
using UnityEngine;

namespace MyLittleBoat
{
    public class OceanDriftController : MonoBehaviour
    {
        [SerializeField] private float baseDriftSpeed = 1.8f;
        [SerializeField] private float recycleZ = -9f;
        [SerializeField] private float spawnZ = 38f;

        private readonly List<Transform> driftObjects = new List<Transform>();
        private DriftSpeed currentSpeed = DriftSpeed.Normal;
        private float speedMultiplier = 1f;

        public DriftSpeed CurrentSpeed
        {
            get { return currentSpeed; }
        }

        public void Initialize(IEnumerable<Transform> objectsToMove)
        {
            driftObjects.Clear();
            if (objectsToMove == null)
            {
                return;
            }

            foreach (Transform objectToMove in objectsToMove)
            {
                if (objectToMove != null)
                {
                    driftObjects.Add(objectToMove);
                }
            }
        }

        /// <summary>
        /// Changes the feeling of drifting without adding fail states or competition.
        /// </summary>
        public void SetSpeed(DriftSpeed speed)
        {
            currentSpeed = speed;
            switch (speed)
            {
                case DriftSpeed.Slow:
                    speedMultiplier = 0.55f;
                    break;
                case DriftSpeed.Fast:
                    speedMultiplier = 1.65f;
                    break;
                default:
                    speedMultiplier = 1f;
                    break;
            }
        }

        private void Update()
        {
            for (int i = 0; i < driftObjects.Count; i++)
            {
                Transform item = driftObjects[i];
                if (item == null)
                {
                    continue;
                }

                Vector3 position = item.position;
                position.z -= baseDriftSpeed * speedMultiplier * Time.deltaTime;
                position.y += Mathf.Sin(Time.time * 0.8f + i) * 0.004f;

                if (position.z < recycleZ)
                {
                    position.z = spawnZ + Random.Range(0f, 14f);
                    position.x = Random.Range(-9f, 9f);
                }

                item.position = position;
            }
        }
    }
}
