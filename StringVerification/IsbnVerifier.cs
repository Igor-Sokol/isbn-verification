using System;
using System.Text.RegularExpressions;

namespace StringVerification
{
    public static class IsbnVerifier
    {
        /// <summary>
        /// Verifies if the string representation of number is a valid ISBN-10 identification number of book.
        /// </summary>
        /// <param name="number">The string representation of book's number.</param>
        /// <returns>true if number is a valid ISBN-10 identification number of book, false otherwise.</returns>
        /// <exception cref="ArgumentException">Thrown if number is null or empty or whitespace.</exception>
        public static bool IsValid(string number)
        {
            if (string.IsNullOrWhiteSpace(number))
            {
                throw new ArgumentException("Thrown if number is null or empty or whitespace.", nameof(number));
            }

            string regexPattern = @"^\d{1}-?\d{3}-?\d{5}-?[0-9\|X]{1}$";
            char[] isbn;
            int[] isbnInt = new int[10];

            if (Regex.IsMatch(number, regexPattern, RegexOptions.IgnoreCase))
            {
                isbn = Regex.Replace(number, @"-", string.Empty, RegexOptions.IgnoreCase).ToCharArray();

                for (int i = 0; i < isbn.Length - 1; i++)
                {
                    isbnInt[i] = (int)char.GetNumericValue(isbn[i]);
                }

                if (isbn[^1] == 'X')
                {
                    isbnInt[^1] = 10;
                }
                else
                {
                    isbnInt[^1] = (int)char.GetNumericValue(isbn[^1]);
                }

                return CheckSumISBN(isbnInt);
            }

            return false;
        }

        /// <summary>
        /// Checks math valid isbn.
        /// </summary>
        /// <param name="numbers">Source numbers.</param>
        /// <returns>true if number is a math valid ISBN-10 identification number of book, false otherwise.</returns>
        private static bool CheckSumISBN(int[] numbers)
        {
            int result = 0;
            for (int i = 10; i > 0; i--)
            {
                result += numbers[^i] * i;
            }

            if (result % 11 == 0)
            {
                return true;
            }

            return false;
        }
    }
}
