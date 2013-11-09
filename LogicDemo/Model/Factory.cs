#region References

using Logic.Model.Core;
using Logic.Model.Gates;
using Logic.Model.Timers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

#endregion

namespace Logic.Model
{
    #region Factory

    public static class Factory
    {
        public static DigitalPin NewDigitalPin(string name, double x, double y, double z)
        {
            return new DigitalPin()
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z
            };
        }

        public static DigitalSignal CreateDigitalSignal(string name, double x, double y, double z)
        {
            return new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                InputPin = NewDigitalPin("pin1", x, y + 15, 0), // left
                OutputPin = NewDigitalPin("pin2", x + 120, y + 15, 0) // right
            };
        }

        public static DigitalWire CreateDigitalWire(string name, DigitalPin startPin, DigitalPin endPin, DigitalSignal signal)
        {
            startPin.Signal = signal;
            endPin.Signal = signal;

            return new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = name,
                Signal = signal,
                StartPin = startPin,
                EndPin = endPin
            };
        }

        public static AndGate CreateAndGate(string name, double x, double y, double z)
        {
            return new AndGate()
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                Pins =
                {
                    NewDigitalPin("pin1", x + 15, y, 0), // top
                    NewDigitalPin("pin2", x + 30, y + 15, 0), // right
                    NewDigitalPin("pin3", x + 15, y + 30, 0), // bottom
                    NewDigitalPin("pin4", x, y + 15, 0) // left
                }
            };
        }

        public static TimerOnDelay CreateTimerOnDelay(string name, double x, double y, double z, double delay)
        {
            return new TimerOnDelay(delay)
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                Pins =
                {
                    NewDigitalPin("pin1", x + 15, y, 0), // top
                    NewDigitalPin("pin2", x + 30, y + 15, 0), // right
                    NewDigitalPin("pin3", x + 15, y + 30, 0), // bottom
                    NewDigitalPin("pin4", x, y + 15, 0) // left
                }
            };
        }

        public static TimerPulse CreateTimerPulse(string name, double x, double y, double z, double delay)
        {
            return new TimerPulse(delay)
            {
                Id = Guid.NewGuid(),
                Name = name,
                X = x,
                Y = y,
                Z = z,
                Pins =
                {
                    NewDigitalPin("pin1", x + 15, y, 0), // top
                    NewDigitalPin("pin2", x + 30, y + 15, 0), // right
                    NewDigitalPin("pin3", x + 15, y + 30, 0), // bottom
                    NewDigitalPin("pin4", x, y + 15, 0) // left
                }
            };
        }
    }

    #endregion
}
