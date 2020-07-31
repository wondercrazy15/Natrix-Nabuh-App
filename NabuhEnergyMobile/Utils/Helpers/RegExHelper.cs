using System;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace NabuhEnergyMobile.Utils.Helpers
{
    public class RegExHelper
    {
        public static bool IsValidPassword(string password)
        {
            if (String.IsNullOrEmpty(password))
            {
                return false;
            }

            var hasNumberPattern = new Regex(@"[0-9]+");
            var hasUpperLowerCaseCharPattern = new Regex(@"[a-zA-Z]+");
            var hasSpecialCharactersPattern = new Regex(@"[!@#$%^&*()\[{\]}]");
            var hasMinimum8CharsPattern = new Regex(@".{8,}");

            Console.WriteLine(hasNumberPattern.IsMatch(password));
            Console.WriteLine(hasUpperLowerCaseCharPattern.IsMatch(password));
            Console.WriteLine(hasSpecialCharactersPattern.IsMatch(password));
            Console.WriteLine(hasMinimum8CharsPattern.IsMatch(password));

            return (hasMinimum8CharsPattern.IsMatch(password) &&
                    (hasUpperLowerCaseCharPattern.IsMatch(password) && hasNumberPattern.IsMatch(password) && hasSpecialCharactersPattern.IsMatch(password)));
        }

        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            if (String.IsNullOrEmpty(phoneNumber))
            {
                return false;
            }

            return Regex.Match(phoneNumber, @"^(\+44\s?7\d{3}|\(?07\d{3}\)?)\s?\d{3}\s?\d{3}$", RegexOptions.IgnoreCase).Success;  //@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");//@"^((\\+91-?)|0)?[0-9]{10}$");
        }

        public static bool IsValidEmail(string emailaddress)
        {
            if (String.IsNullOrEmpty(emailaddress))
            {
                return false;
            }

            try
            {
                MailAddress m = new MailAddress(emailaddress);

                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
    }
}
