using NUnit.Framework;
using UnityEngine;

namespace Tests.SOLID
{
    //Summary : Guarantee having feature
    //Able to swap with subclass without knowing actual type
    public class LiskovSubstitution
    {
        [Test]
        public void LiskovSubstitutionTest()
        {
            //Able to use without knowing exact type
            IMoveable vehicle = new RoadVehicle();
            IMoveable vehicle2 = new RailVehicle();

            vehicle.GoForward();
            vehicle2.GoForward();
            //check if have ITurnable
            //※Below may not be Liskov Substitution
            if (vehicle is ITurnable turnable) turnable.TurnLeft();
            if (vehicle2 is ITurnable turnable2) turnable2.TurnLeft();
        }
    }

    public interface IMoveable //All inheritance guaranteed to have those methods
    {
        // Usually logic of those function could be same.
        void GoForward();
        void Reverse();
    }

    public interface ITurnable
    {
        void TurnLeft();
        void TurnRight();
    }

    public class RoadVehicle : IMoveable, ITurnable
    {
        public void GoForward()
        {
            Debug.Log("Moving forward on the road");
        }

        public void Reverse()
        {
            Debug.Log("Reversing on the road");
        }

        public void TurnLeft()
        {
            Debug.Log("Turn left on the road");
        }

        public void TurnRight()
        {
            Debug.Log("Turn right on the road");
        }
    }

    public class RailVehicle : IMoveable
    {
        public void GoForward()
        {
            Debug.Log("Moving forward on the rail");
        }

        public void Reverse()
        {
            Debug.Log("Reversing on the rail");
        }
    }
}