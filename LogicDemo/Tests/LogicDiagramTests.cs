using Logic.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Concurrency;
using System.Text;
using System.Threading.Tasks;

namespace WpfApplication1.Tests
{
    #region Tests: Digital Logic Diagrams

    public static class LogicDiagramTests
    {
        #region Test Diagram #1 (Timers)

        public static DigitalLogicDiagram GetTestDigitalLogicDiagram1(IScheduler scheduler)
        {
            // create diagram
            var diagram = new DigitalLogicDiagram() { Id = Guid.NewGuid() };

            // create signals
            var input1 = Factory.CreateDigitalSignal("input1", 0, 90, 0);
            var input2 = Factory.CreateDigitalSignal("input2", 0, 150, 0);

            var output1 = Factory.CreateDigitalSignal("output1", 0, 0, 0);
            var output2 = Factory.CreateDigitalSignal("output2", 0, 0, 0);
            var output3 = Factory.CreateDigitalSignal("output3", 450, 90, 0);

            // create elements
            var andGate1 = Factory.CreateAndGate("andGate1", 180, 90, 0);
            var timerOnDelay1 = Factory.CreateTimerOnDelay("timerOnDelay1", 270, 90, 0, 2.0);
            var timerPulse1 = Factory.CreateTimerPulse("timerPulse1", 360, 90, 0, 1.0);

            // create digital wires with signal and pin bindings
            var wire1 = Factory.CreateDigitalWire("wire1", input1.OutputPin, andGate1.Pins[3], input1);
            var wire2 = Factory.CreateDigitalWire("wire2", input2.OutputPin, andGate1.Pins[2], input2);

            var wire3 = Factory.CreateDigitalWire("wire3", andGate1.Pins[1], timerOnDelay1.Pins[3], output1);
            var wire4 = Factory.CreateDigitalWire("wire4", timerOnDelay1.Pins[1], timerPulse1.Pins[3], output2);
            var wire5 = Factory.CreateDigitalWire("wire5", timerPulse1.Pins[1], output3.InputPin, output3);

            // connect signals to elements
            andGate1.Outputs.Add(output1);
            andGate1.Inputs.Add(input1);
            andGate1.Inputs.Add(input2);

            timerOnDelay1.Outputs.Add(output2);
            timerOnDelay1.Inputs.Add(output1);

            timerPulse1.Outputs.Add(output3);
            timerPulse1.Inputs.Add(output2);

            // add all digital elements/signals/wires to list
            diagram.Elements.Add(wire1);
            diagram.Elements.Add(wire2);
            diagram.Elements.Add(wire3);
            diagram.Elements.Add(wire4);
            diagram.Elements.Add(wire5);

            diagram.Elements.Add(andGate1);
            diagram.Elements.Add(timerOnDelay1);
            diagram.Elements.Add(timerPulse1);

            diagram.Elements.Add(input1);
            diagram.Elements.Add(input2);

            diagram.Elements.Add(output3);

            // initialize input/output vector
            output1.State = false;
            output2.State = false;
            output3.State = false;

            input1.State = false;
            input2.State = false;

            // initialize diagram and elements
            diagram.ObserveInputs(scheduler);
            diagram.ObserveElements(scheduler);

            return diagram;
        }

        #endregion

        #region Test Diagram #2 (SR NOR latch)

        public static DigitalLogicDiagram GetTestDigitalLogicDiagram2(IScheduler scheduler)
        {
            //
            // SR NOR latch
            //
            // SR latch operation
            // S	R	Action
            // 0	0	No Change
            // 0	1	Q = 0
            // 1	0	Q = 1
            // 1	1	Restricted combination
            //
            // mofre info: http://en.wikipedia.org/wiki/Flip-flop_(electronics)
            //

            // create diagram
            //var diagram = new DigitalLogicDiagram(collectionScheduler, simulationScheduler) { Id = Guid.NewGuid() };
            var diagram = new DigitalLogicDiagram() { Id = Guid.NewGuid() };

            // create digital input signals
            var input1 = new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = "R",
                X = 0,
                Y = 90
            };

            var input2 = new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = "S",
                X = 0,
                Y = 150
            };

            // create digital output signals
            var output1 = new DigitalSignal()
            {
                Id = Guid.NewGuid(),
                Name = "Q",
                X = 270,
                Y = 90
            };

            var output2 = new DigitalSignal() // TODO: Q' must be 'true' for the init value for the SR ("set-reset") latch
            {
                Id = Guid.NewGuid(),
                Name = "Q'",
                X = 270,
                Y = 150
            };

            // create logic element and signal bindings
            //var norGate1 = new NorGate(collectionScheduler, simulationScheduler)
            var norGate1 = new NorGate()
            {
                Id = Guid.NewGuid(),
                Name = "NorGate1",
                Inputs = { input1, output2 },
                Outputs = { output1 }, // TODO: add Inputs before Outputs to properly init diagram
                X = 180,
                Y = 90
            };

            //var norGate2 = new NorGate(collectionScheduler, simulationScheduler)
            var norGate2 = new NorGate()
            {
                Id = Guid.NewGuid(),
                Name = "NorGate2",
                Inputs = { input2, output1 },
                Outputs = { output2 }, // TODO: add Inputs before Outputs to properly init diagram
                X = 180,
                Y = 150
            };

            // create digital wires with signal and pin bindings
            var wire1 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire1",
                Signal = input1,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 120,
                    Y = 105
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 180,
                    Y = 105
                }
            };

            var wire2 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire2",
                Signal = input2,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 120,
                    Y = 165
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 180,
                    Y = 165
                }
            };

            var wire3 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire3",
                Signal = output1,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 218,
                    Y = 105
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 270,
                    Y = 105
                }
            };

            var wire4 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire4",
                Signal = output2,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 218,
                    Y = 165
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 270,
                    Y = 165
                }
            };

            var wire5 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire5",
                Signal = output1,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 240,
                    Y = 105
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 195,
                    Y = 150
                }
            };

            var wire6 = new DigitalWire()
            {
                Id = Guid.NewGuid(),
                Name = "wire6",
                Signal = output2,
                StartPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "startPin",
                    X = 240,
                    Y = 165
                },
                EndPin = new DigitalPin()
                {
                    Id = Guid.NewGuid(),
                    Name = "endPin",
                    X = 195,
                    Y = 120
                }
            };

            /*
            // add all digital input signals to list
            diagram.Inputs.Add(input1);
            diagram.Inputs.Add(input2);

            // add all digital output signals to list
            diagram.Outputs.Add(output1);
            diagram.Outputs.Add(output2);

            // add all digital wires to list
            diagram.Wires.Add(wire1);
            diagram.Wires.Add(wire2);
            diagram.Wires.Add(wire3);
            diagram.Wires.Add(wire4);
            diagram.Wires.Add(wire5);
            diagram.Wires.Add(wire6);
            */

            // add all digital elements/signals/wires to list

            diagram.Elements.Add(wire1);
            diagram.Elements.Add(wire2);
            diagram.Elements.Add(wire3);
            diagram.Elements.Add(wire4);
            diagram.Elements.Add(wire5);
            diagram.Elements.Add(wire6);

            diagram.Elements.Add(norGate1);
            diagram.Elements.Add(norGate2);

            diagram.Elements.Add(input1);
            diagram.Elements.Add(input2);

            diagram.Elements.Add(output1);
            diagram.Elements.Add(output2);

            // initialize input/output vector
            output1.State = false; // Q
            output2.State = false; // Q'

            input2.State = false; // S
            input1.State = false; // R

            // initialize diagram and elements
            diagram.ObserveInputs(scheduler);
            diagram.ObserveElements(scheduler);

            return diagram;
        }

        #endregion
    }

    #endregion
}
