using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Architecture.DDD
{
    public abstract class ValueObjectBase
    {
        public static bool operator ==(ValueObjectBase a, ValueObjectBase b)
        {
            if (a is null && b is null)
                return true;

            if (a is null || b is null)
                return false;

            return a.Equals(b);
        }

        public static bool operator !=(ValueObjectBase a, ValueObjectBase b)
        {
            return !(a == b);
        }

        protected abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != GetType())
            {
                return false;
            }

            ValueObjectBase other = (ValueObjectBase)obj;
            IEnumerator<object> thisValues = GetAtomicValues().GetEnumerator();
            IEnumerator<object> otherValues = other.GetAtomicValues().GetEnumerator();

            bool thisMoveNext = thisValues.MoveNext();
            bool otherMoveNext = otherValues.MoveNext();
            while (thisMoveNext && otherMoveNext)
            {
                if (ReferenceEquals(thisValues.Current, null) ^ ReferenceEquals(otherValues.Current, null))
                {
                    return false;
                }

                if (thisValues.Current is IEnumerable enumerable)
                {
                    var thisIEnumerableEnumerator = enumerable.GetEnumerator();
                    var otherIEnumerableEnumerator = (otherValues.Current as IEnumerable).GetEnumerator();

                    var firstMoveNext = thisIEnumerableEnumerator.MoveNext();
                    var secondMoveNext = otherIEnumerableEnumerator.MoveNext();

                    if (firstMoveNext != secondMoveNext)
                        return false;

                    while (firstMoveNext && secondMoveNext)
                    {
                        if (!thisIEnumerableEnumerator.Current.Equals(otherIEnumerableEnumerator.Current))
                            return false;
                        firstMoveNext = thisIEnumerableEnumerator.MoveNext();
                        secondMoveNext = otherIEnumerableEnumerator.MoveNext();
                        if (firstMoveNext != secondMoveNext)
                            return false;
                    }
                }

                else if (thisValues.Current != null && !thisValues.Current.Equals(otherValues.Current))
                {
                    return false;
                }

                else if (thisValues.Current == null && otherValues.Current != null)
                {
                    return false;
                }

                thisMoveNext = thisValues.MoveNext();
                otherMoveNext = otherValues.MoveNext();
            }

            return thisMoveNext == otherMoveNext;
        }

        public override int GetHashCode()
        {
            return GetAtomicValues()
                .Select(x => x != null ? x.GetHashCode() : 0)
                .Aggregate((x, y) => x ^ y);
        }
    }
}
