using System;
using NUnit.Framework;
using UnityEngine;

namespace Tests.SOLID
{
    //Summary : Able to add feature with variant
    //Don't have to break existing code when adding new feature
    public class OpenClosed
    {
        [Test]
        public static void OpenClosedTest()
        {
            Shape s1 = new Circle(1.0);
            Shape s2 = new Rectangle(1.0, 2.0);
            Debug.Log(s1.GetArea());
            Debug.Log(s2.GetArea());
        }
    }

    public abstract class Shape
    {
        public abstract double GetArea(); //Able to add feature with variant
    } //Usually functionality vary based on variant.

    public class Circle : Shape
    {
        public Circle(double radius)
        {
            Radius = radius;
        }

        public double Radius { get; }

        public override double GetArea()
        {
            return Math.PI * Radius * Radius;
        }
    }

    public class Rectangle : Shape
    {
        public Rectangle(double width, double height)
        {
            Width = width;
            Height = height;
        }

        public double Width { get; }
        public double Height { get; }

        public override double GetArea()
        {
            return Width * Height;
        }
    }
}