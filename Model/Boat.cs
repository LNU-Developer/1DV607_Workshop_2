using System;

namespace workshop_2
{
    class Boat {
        private BoatTypes _boatType;
        private double _length;

        public BoatTypes Type
        {
            get => _boatType;
            set => _boatType = value;
        }
        public double Length
        {
            get => _length;
            set
            {
                if (value < 0 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be above 0");
                _length = value;
            }
        }

        public Boat(BoatTypes boatType, double length)
        {

            Type = Enum.IsDefined(typeof(BoatTypes), boatType) ? boatType : throw new ArgumentException(nameof(boatType));
            _length = length;
        }
    }
}