using System;
using System.Collections.Generic;
using System.Text;

namespace DrumStats.Helpers
{
    public class Pair<T, U> :ObservableObject
    {
        public Pair()
        {
        }

        public Pair(T first, U second)
        {
            this.First = first;
            this.Second = second;
        }

        private T first;
        public T First
        {
            get { return first; }
            set { SetProperty(ref first, value); }
        }

        private U second;
        public U Second
        {
            get { return second; }
            set { SetProperty(ref second, value); }
        }
    };
}
