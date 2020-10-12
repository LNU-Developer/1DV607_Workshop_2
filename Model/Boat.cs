using System;
using Google.Cloud.Firestore;
using Enum.boat.type;

namespace Model
{
    [FirestoreData]
    class Boat {
        private BoatType _boatType;
        private double _length;
        private int _boatId;

        [FirestoreProperty (ConverterType = typeof(FirestoreEnumNameConverter<BoatType>))]
        public BoatType Type
        {
            get { return _boatType; }
            set
            {
                _boatType = BoatType.IsDefined(typeof(BoatType), value) ? value : throw new ArgumentException(nameof(value));
            }
        }

        [FirestoreProperty]
        public double Length
        {
            get { return _length; }
            set
            {
                if (value < 0 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be above 0");
                _length = value;
            }
        }

        [FirestoreProperty]
        public int BoatId
        {
            get { return _boatId; }
            set
            {
                if (value < 0 )
                    throw new ArgumentOutOfRangeException(
                        $"{nameof(value)} must be above 0");

                _boatId=value;
            }
        }
    }
}