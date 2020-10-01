using System;
using Google.Cloud.Firestore;
using EnumBoatTypes;

namespace Model
{
    [FirestoreData]
    class Boat {
        private BoatTypes _boatType;
        private double _length;
        private int _boatId;

        [FirestoreProperty (ConverterType = typeof(FirestoreEnumNameConverter<BoatTypes>))]
        public BoatTypes Type
        {
            get => _boatType;
            set
            {
                _boatType = Enum.IsDefined(typeof(BoatTypes), value) ? value : throw new ArgumentException(nameof(value));
            }
        }

        [FirestoreProperty]
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

        [FirestoreProperty]
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
    }
}