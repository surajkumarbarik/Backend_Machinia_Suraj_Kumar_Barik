//using System.Text.RegularExpressions;  //^\+?\d{10,12}$");
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
using MongoDB.Driver;
using Microsoft.Extensions.FileSystemGlobbing;
using MongoDB.Bson;
using System.Collections;
using System.Reflection.Metadata;
using center_of_school.data;

namespace twyn_machinia.validator
{
    public class Validator

    {
        private static readonly Regex PhoneRegex = new Regex(@"(\d{10})$");

        // phone number valid check [logic part]
        public static bool IsValid(string phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(phoneNumber))
            {
                return false;
            }

            return PhoneRegex.IsMatch(phoneNumber);
        }


        // phone number valid check [logic part]
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Regex pattern to match a 10-digit phone number
            string pattern = @"^\d{10}$";

            // Check if the phone number matches the pattern
            if (Regex.IsMatch(phoneNumber, pattern))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // email valid check [logic part]
        public static bool IsValidEmail(string email)
        {
            string pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[cC][oO][mM]$";
            Regex regex = new Regex(pattern);

            if (string.IsNullOrWhiteSpace(email))
                return false;
            else if (!regex.IsMatch(email))
                return false;
            else
                return true;

        }



        // centername valid check [logic part]
        public static bool LengthCheck(string CenterName)
        {
            if (Regex.IsMatch(CenterName, "^.{0,39}$"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        // courseoffered data valid check [logic part]
        public static bool ListSizeCheck(List<string> CoursesOffered)
        {
            if (CoursesOffered == null || CoursesOffered.Count == 0)
            {
                return false;
            }
            else if (CoursesOffered.Any(x => string.IsNullOrWhiteSpace(x)))
            { return false; }

            else if (CoursesOffered.Count == CoursesOffered.Where(s => !string.IsNullOrEmpty(s)).Count() == true)
            {
                return true;
            }

            else
            {
                return true;
            }
        }

        // centercode valid check [logic part]
        public static bool AlphaNumericCheck(string CenterCode)
        {

            //bool isAlphanumeric = Regex.IsMatch(CenterCode, "^[a-zA-Z0-9]*$"); // if only alpha or numeric then also it will work

            bool isAlphanumeric = Regex.IsMatch(CenterCode, "^(?=.*[a-zA-Z])(?=.*\\d)[a-zA-Z\\d]+$"); // must mix


            if (!isAlphanumeric)
            {
                return false;
            }
            else if (CenterCode.Length != 12)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

    }
}
