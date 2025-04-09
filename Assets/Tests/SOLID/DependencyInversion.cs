using NUnit.Framework;
using UnityEngine;

namespace Tests.SOLID
{
    //Interface to proxy coupling
    //Only high-level module will depend. Loosely coupled
    public class DependencyInversion
    {
        [Test]
        public void DependencyInversionTest()
        {
            var light = new Light();
            var controller = new Switch(light);
            controller.Toggle();
            controller.Toggle();
        }
    }

    public class Switch
    {
        private readonly ISwitchable _switchable;
        private bool _isOn;

        public Switch(ISwitchable switchable)
        {
            _switchable = switchable;
        }

        public void Toggle()
        {
            if (_isOn) _switchable.SwitchOff();
            else _switchable.SwitchOn();
            _isOn = !_isOn;
        }
    }

    public interface ISwitchable
    {
        void SwitchOn();
        void SwitchOff();
    }

    public class Light : ISwitchable
    {
        public void SwitchOn()
        {
            Debug.Log("Light is on");
        }

        public void SwitchOff()
        {
            Debug.Log("Light is off");
        }
    }
}