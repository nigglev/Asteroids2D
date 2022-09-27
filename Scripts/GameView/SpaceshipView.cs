using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameView
{
    public class SpaceshipView : MonoBehaviour, IViewObject
    {   

        void Start()
        {   
            Debug.Log("Ship start");
        }


        private void Update()
        {
            
        }

        #region IViewObject
        public void SetPosition(Vector2 inPosition)
        {
            transform.position = inPosition;
        }

        //public void SetRotation(Quaternion inRotation)
        //{
        //    transform.rotation = inRotation;

        //}

        public void SetRotation(Matrix2x2 inRotation)
        {
            float angle = inRotation.ToAngle();
            Debug.Log($"Current rotation is {angle} Degrees");
            transform.rotation = Quaternion.AngleAxis(angle, -Vector3.forward);

        }

        #endregion IViewObject
    }

}