using System;
using System.Collections.Generic;

namespace Rallyman
{
    abstract class Dice
    {
        public abstract int Roll();
        public abstract uint Faces { get; }
    }

    abstract class RallymanDice : Dice
    {
        private Random _rnd = new();

        public enum Results
        {
            ALERT,
            FIRST_GEAR,
            SECOND_GEAR,
            THIRD_GEAR,
            FOURTH_GEAR,
            FIFTH_GEAR,
            SIXTH_GEAR,
            MAINTAIN_SPEED,
            SUCCESSFUL_BRAKING
        }

        public override int Roll()
        {
            double roll = _rnd.NextDouble();

            foreach (var item in Probabilities)
            {
                if (roll <= item.Value)
                    return (int)item.Key;
                roll -= item.Value;
            }
            // Should never be reached
            return 0;
        }

        public bool RollAndCheckAlert()
        {
            Results roll = (Results)Roll();
            return roll == Results.ALERT;
        }

        protected Dictionary<Results, double> _probabilities = new();
        public Dictionary<Results, double> Probabilities
        {
            get => _probabilities;
        }

        public override uint Faces => 6;
    }

    class BrakeDice : RallymanDice
    {
        public BrakeDice()
        {
            _probabilities.Add(Results.ALERT, 2.0 / Faces);
            _probabilities.Add(Results.SUCCESSFUL_BRAKING, 4.0 / Faces);
        }
    }

    class MaintainDice : RallymanDice
    {
        public MaintainDice()
        {
            _probabilities.Add(Results.ALERT, 1.0 / Faces);
            _probabilities.Add(Results.MAINTAIN_SPEED, 5.0 / Faces);
        }
    }

    class GearDice : RallymanDice
    {
        public GearDice(Results gearType)
        {
            double alertFaces = 0.0;

            switch (gearType)
            {
                case Results.ALERT:
                case Results.MAINTAIN_SPEED:
                case Results.SUCCESSFUL_BRAKING:
                    throw new ArgumentException("Passed type must be a valid gear");

                case Results.FIRST_GEAR:
                case Results.SECOND_GEAR:
                case Results.THIRD_GEAR:
                    alertFaces = 1.0;
                    break;

                case Results.FOURTH_GEAR:
                case Results.FIFTH_GEAR:
                case Results.SIXTH_GEAR:
                    alertFaces = 2.0;
                    break;
            }

            _probabilities.Add(Results.ALERT, alertFaces / Faces);
            _probabilities.Add(gearType, (Faces - alertFaces) / Faces);
        }
    }
}
