using UnityEngine;

namespace SharedGame.Extensions
{
    public static class TransformExtensions
    {
        /// <summary>
        /// Check if the target is whitin the line of sight
        /// </summary>
        /// <param name="origin">transform origin</param>
        /// <param name="target">target direction</param>
        /// <param name="fieldOfView">field of view</param>
        /// <param name="collisionMask">check against layers</param>
        /// <param name="offset">transform origin offset</param>
        /// <returns>yes or no</returns>
        public static bool IsInLineOfSight(this Transform origin,Vector3 target,float fieldOfView,LayerMask collisionMask,Vector3 offset)
        {
            Vector3 direction = target - origin.position;

            if (Vector3.Angle(origin.forward, direction.normalized) < fieldOfView / 2)
            {
                float distanceToTarget = Vector3.Distance(origin.position, target);
                //view blocked
                if (Physics.Raycast(origin.position + offset + origin.forward* .3f, direction.normalized, distanceToTarget, collisionMask))
                {
                    return false;
                }
                return true;
            }
            return false;
        }
    }
}
