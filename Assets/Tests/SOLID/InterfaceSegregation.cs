using NUnit.Framework;

namespace Tests.SOLID
{
    //Avoid large interface
    //Only have to implement what you need
    public class InterfaceSegregation
    {
        [Test]
        public void InterfaceSegregationTest()
        {
            //Reusing Liskov Substitution Demo
            IMoveable vehicle = new RoadVehicle();
            IMoveable vehicle2 = new RailVehicle(); //Don't have turning method which can not be used

            vehicle.GoForward();
            vehicle2.GoForward();
            if (vehicle is ITurnable turnable) turnable.TurnLeft();
            if (vehicle2 is ITurnable turnable2) turnable2.TurnLeft();
        }
    }
}