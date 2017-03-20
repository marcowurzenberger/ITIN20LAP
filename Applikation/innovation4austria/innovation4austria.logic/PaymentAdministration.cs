using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace innovation4austria.logic
{
    public class PaymentAdministration
    {
        /// <summary>
        /// Check creditcardnumber with luhn algorithm
        /// </summary>
        /// <param name="data">creditcardnumber</param>
        /// <returns>true if valid, false if not valid</returns>
        public static bool CheckLuhn(string data)
        {
            int sum = 0;
            int len = data.Length;
            for (int i = 0; i < len; i++)
            {
                int add = (data[i] - '0') * (2 - (i + len) % 2);
                add -= add > 9 ? 9 : 0;
                sum += add;
            }
            return sum % 10 == 0;
        }
    }
}
