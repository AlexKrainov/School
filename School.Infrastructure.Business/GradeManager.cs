using School.Domain.Core;
using School.Infrastructure.Business.Extensions;
using System;

namespace School.Infrastructure.Business
{
    public class GradeManager
    {
        public bool isValid(string value, ref Grade grade)
        {
            if (value.Length == 2)
            {
                int number = 0;
                if (int.TryParse(value[0].ToString(), out number) == true
                    && number > 0
                    && number < 12)
                {
                    string symbol = value[1].ToString().ToUpper();
                    if (symbol.ContainsRusSymbol() == true)
                    {
                        grade = new Grade { Number = number, Symbol = symbol };
                        return true;
                    }
                }
                else
                {
                    return false;
                }
            }
            return false;
        }


    }
    
}
