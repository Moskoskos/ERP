using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ERP
{
    public class Validation
    {
        public Validation()
        {

        }
        //Check if amount of cups is an integer and a positive number
        public bool TestIntegerInput(string input)
        {
            int output;
            if (Int32.TryParse(input, out output))
            {
                if (output >= 0 && output <= Int32.MaxValue)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public bool CheckGramInput(string input, string min, string max)
        {
            //Check that input is an integer
            double output;
            if (Double.TryParse(input, out output))
            {
                //Check that input is within x grams
                if (output >= Convert.ToDouble(min) && output <= Convert.ToDouble(max))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        //Checks if the input is an integer and also if the content is between certain values
        public bool CheckGramInputTall(string input, string min, string max)
        {
            double output;
            if (Double.TryParse(input, out output))
            {
                if (output >= Convert.ToDouble(min) && output <= Convert.ToDouble(max))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }

        }
    }
}
