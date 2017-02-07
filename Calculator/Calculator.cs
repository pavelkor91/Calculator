using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace Calculator
{
    public enum Operator
    {
        EMPTY,
        PLUS,
        MINUS,
        MULTIPLY,
        DIVIDE,
        EQUAL,
        PERCENT
    }

    class Calculator
    {

        public double leftOprand {set;get;}
        private Operator curentOperation;
        public int counterEqual { set; get; }
        public string makeOperation(Operator operation, string value)
        {
         

            if (counterEqual > 0)
            {
                counterEqual = 0;
                curentOperation = Operator.EMPTY;
                value = "";
            }

            if (curentOperation != Operator.EMPTY)
            {
                this.calculateOperation(value);
                if (operation == Operator.PERCENT)
                    return leftOprand.ToString();
            }

            else
                if (value != null && value != "")
                    leftOprand = double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture);
           
            curentOperation = operation;
            return leftOprand.ToString();
        }

        public string equalOperation(string value)
        {
            counterEqual++;
            this.calculateOperation(value);
            return leftOprand.ToString();
        }

        public void clear()
        {
            leftOprand = 0.0d;
            curentOperation = Operator.EMPTY;
            counterEqual = 0;
        }

        public string percentOperation(string value, string operand)
        {
            if (operand.Equals("left"))
            {
                leftOprand = leftOprand / 100.0;
                return leftOprand.ToString();
            }
            else
            {
                if (leftOprand == 0)
                {
                    double rightOperand = double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture) / 100.0;
                    return rightOperand.ToString();
                }
                else
                {
                    double rightOperand = leftOprand / 100.0 * double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture);
                    return rightOperand.ToString();
                }
            }
        }

         public string changeSign(string value,string operand)
         {
           
             if (operand.Equals("left"))
             {
                 leftOprand = leftOprand * -1;
                 return leftOprand.ToString();
             }
             else
             {
                 double rightOperand = double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture) * -1;
                 return rightOperand.ToString();
             }
         }
       
         private void calculateOperation(string value)
        {
            if (value != "")
               switch (curentOperation)
              {
                    case Operator.PLUS:
                      leftOprand = leftOprand + double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture);
                        break;
                    case Operator.MINUS:
                        leftOprand = leftOprand - double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture);
                        break;

                   case Operator.MULTIPLY:
                        leftOprand = leftOprand * double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture);
                         break;

                   case Operator.DIVIDE:
                         if (double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture) != 0)
                             leftOprand = leftOprand / double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture);
                         else
                         {
                             leftOprand = 0.0d;
                             curentOperation = Operator.EMPTY;
                             MessageBox.Show("Division by zero","Error",MessageBoxButtons.OK,MessageBoxIcon.Error);
                         }
                         break; 
                   case Operator.PERCENT:
                         leftOprand = leftOprand / 100.0;
                         break;
     
                   case Operator.EMPTY:
                         leftOprand = double.Parse(value, System.Globalization.CultureInfo.InstalledUICulture);
                         break;
            } 
        }
    }
}
