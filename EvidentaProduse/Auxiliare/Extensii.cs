﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EvidentaProduse.Auxiliare
{
    public static class Extensii
    {

        public static bool inRange(this DateTime date, DateTime? startDate, DateTime? endDate)
        {
            DateTime UTCdate = date.ToUniversalTime();

            DateTime UTCstartDate;
            DateTime UTCendDate;

            bool isStartDateNull = false;
            bool isEndDateNull = false;

            if (startDate == null)
            {
                isStartDateNull = true;
            }
            else
            {
                UTCstartDate = Convert.ToDateTime(startDate).ToUniversalTime();
            }

            if (endDate == null)
            {
                isEndDateNull = true;
            }
            else
            {
                UTCendDate = Convert.ToDateTime(endDate).ToUniversalTime();
            }

            if(isEndDateNull && isStartDateNull)
            {
                return false;
            }    

            if(isEndDateNull)
            {
                return (date >= startDate);
            }
            else if(isStartDateNull)
            {
                return (date <= endDate);
            }

            return ((date >= startDate && date <= endDate));
        }


        public static string ToRoman(this int number)
        {
            if ((number < 0) || (number > 3999)) throw new ArgumentOutOfRangeException("insert value betwheen 1 and 3999");
            if (number < 1) return string.Empty;
            if (number >= 1000) return "M" + ToRoman(number - 1000);
            if (number >= 900) return "CM" + ToRoman(number - 900);
            if (number >= 500) return "D" + ToRoman(number - 500);
            if (number >= 400) return "CD" + ToRoman(number - 400);
            if (number >= 100) return "C" + ToRoman(number - 100);
            if (number >= 90) return "XC" + ToRoman(number - 90);
            if (number >= 50) return "L" + ToRoman(number - 50);
            if (number >= 40) return "XL" + ToRoman(number - 40);
            if (number >= 10) return "X" + ToRoman(number - 10);
            if (number >= 9) return "IX" + ToRoman(number - 9);
            if (number >= 5) return "V" + ToRoman(number - 5);
            if (number >= 4) return "IV" + ToRoman(number - 4);
            if (number >= 1) return "I" + ToRoman(number - 1);
            throw new ArgumentOutOfRangeException("something bad happened");
        }

    }
}