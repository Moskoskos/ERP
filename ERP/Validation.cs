using System;
using System.Windows.Forms;

namespace ERP
{
    public class Validation
    {
        public Validation()
        {

        }
        /// <summary>
        /// Check if amount of cups is an integer and a positive number
        /// </summary>
        /// <param name="input">Value to be tested.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Checks whether or not the inserted value is within the spesified range defined in min and max.
        /// </summary>
        /// <param name="input">Actual value to be inserted</param>
        /// <param name="min">The minimum value allowed.</param>
        /// <param name="max">The maximum value allowed.</param>
        /// <returns></returns>
        public bool CheckGramInput(string input, string min, string max)
        {
            //Check that input is an integer
            int output;
            if (Int32.TryParse(input, out output))
            {
                //Check that input is within x grams
                if (output >= Convert.ToInt32(min) && output <= Convert.ToInt32(max))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Value cannot be smaller than " + min + "g or bigger than " + max + "g");
                    return false;
                }
            }
            else
            {
                MessageBox.Show(input + " is not a valid value");
                return false;
            }

        }

        /// <summary>
        /// Checks whether or not the inserted value is within the spesified range defined in min and max.
        /// </summary>
        /// <param name="input">Actual value to be inserted</param>
        /// <param name="min">The minimum value allowed.</param>
        /// <param name="max">The maximum value allowed.</param>
        /// <returns></returns>
        public bool CheckGramInputTall(string input, string min, string max)
        {
            int output;
            if (Int32.TryParse(input, out output))
            {
                if (output >= Convert.ToInt32(min) && output <= Convert.ToInt32(max))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("Value cannot be smaller than " + min + "g or bigger than " + max + "g");
                    return false;
                }
            }
            else
            {
                MessageBox.Show(input + " is not a valid value");
                return false;
            }

        }

        /// <summary>
        /// If cup is bigger than 0, then cup needs to have fill level bigger than 0.
        /// if this is true, set validation == true
        /// HOWEVER if cup is 0 and fill level is 0, send true
        /// </summary>
        /// <param name="colorCup">Color Code for the cup</param>
        /// <param name="colorFill">Any given value [g]</param>
        /// <returns></returns>
        public bool TestVadilityOfCupInput(int colorCup, int colorFill)
        {
            if (colorCup > 0 && colorFill > 0)
            {
                return true;
            }
            if (colorCup == 0 && colorFill == 0)
            {
                return true;
            }

            //Suspect this one of being redundant
            if ((colorCup > 0 && colorFill <= 0) && (colorCup <= 0 && colorFill > 0))
            {
                return false;
            }

            else
            {
                return false;
            }
        }
    }
}
