using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.Threading;

namespace Calculator
{
    class Factorial
    {
        public event Action<int ,BigInteger> progressChanged;
        public bool isCancel;

        public void calculateFactorial(object data)
        {
            int number = (int)data;

            BigInteger result = 1;
            for (int i = 1; i <= number; ++i)
            {
                if (this.isCancel)
                    break;
                result *= i;
                Thread.Sleep(15);
                this.progressChanged(i ,result);
            }
            this.isCancel = false;
        }
    }
}
