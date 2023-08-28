using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework1
{
    public struct Table
    {
        private int _index;
        private int _numbOfSeats;

        public int Index { 
            get => _index;
            set => _index = value;
        }
        public int NumOfSeats
        {
            get => _numbOfSeats;
            set => _numbOfSeats = value;
        }

        public Table(int index, int numbOfSeats)
        {
            _index = index;
            _numbOfSeats = numbOfSeats;
        }
    }
}
