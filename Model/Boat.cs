using System;

namespace workshop_2
{
    class Boat {
        private BoatTypes _boatType;
        private double _length;
        private int _boatId;

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
        public int BoatId
        {
            get
            {
                return _boatId;
            }
            set
            {
                if (value < 0 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be above 0");

                _boatId=value;
            }
        }
        public Boat(BoatTypes boatType, double length, int boatId)
        {

            Type = Enum.IsDefined(typeof(BoatTypes), boatType) ? boatType : throw new ArgumentException(nameof(boatType));
            _length = length;
            _boatId = boatId;
        }

         public override string ToString()
        {
            return $"Length: {Length} \nType: {Type} \nId: {_boatId} ";
        }
    }
}