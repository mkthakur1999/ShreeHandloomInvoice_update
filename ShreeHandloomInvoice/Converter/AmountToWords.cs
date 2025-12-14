using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShreeHandloomInvoice.Converter
{
    public static class AmountToWords
    {
        private static readonly string[] Units =
        {
            "Zero","One","Two","Three","Four","Five","Six","Seven","Eight","Nine","Ten",
            "Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen","Seventeen","Eighteen","Nineteen"
        };

        private static readonly string[] Tens =
        {
            "Zero","Ten","Twenty","Thirty","Forty","Fifty","Sixty","Seventy","Eighty","Ninety"
        };

        public static string Convert(double number)
        {
            if (number == 0)
                return "Zero Rupees Only";

            long n = (long)number;
            return ConvertNumber(n) + " Rupees Only";
        }

        private static string ConvertNumber(long n)
        {
            if (n < 20) return Units[n];
            if (n < 100) return Tens[n / 10] + " " + Units[n % 10];
            if (n < 1000) return Units[n / 100] + " Hundred " + ConvertNumber(n % 100);
            if (n < 100000) return ConvertNumber(n / 1000) + " Thousand " + ConvertNumber(n % 1000);
            if (n < 10000000) return ConvertNumber(n / 100000) + " Lakh " + ConvertNumber(n % 100000);

            return ConvertNumber(n / 10000000) + " Crore " + ConvertNumber(n % 10000000);
        }
    }

}
