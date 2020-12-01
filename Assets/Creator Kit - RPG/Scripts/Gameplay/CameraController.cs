using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPGM.Gameplay
{

    /// <summary>
    /// A simple camera follower class. It saves the offset from the
    ///  focus position when started, and preserves that offset when following the focus.
    /// </summary>
    public class CameraController : MonoBehaviour
    {
        public Transform focus;
        public float smoothTime = 2;
        Vector3 offset;
     //   public Vector2 maxPosition;
     //   public Vector2 minPosition;

        void Awake()
        {
            offset = focus.position - transform.position;
       //     offset.x = Mathf.Clamp(offset.x, minPosition.x, maxPosition.x);
       //     offset.y = Mathf.Clamp(offset.y, minPosition.y, maxPosition.y);
        }

        void Update()
        {
            //idea: clamp the transform. no. clamp offset?
            transform.position = Vector3.Lerp(transform.position, focus.position - offset, Time.deltaTime * smoothTime);
        }
    }
}
