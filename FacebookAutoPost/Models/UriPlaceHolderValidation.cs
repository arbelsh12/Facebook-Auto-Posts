﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace FacebookAutoPost.Models
{
    public class UriPlaceHolderValidation
    {
        public UriPlaceHolderValidation()
        { }

        public async Task<int> CountParamsUri(string uri)
        {
            bool isValid = await CheckValidUri(uri);

            if (!isValid) { return -1; }

            List<string> betweenBrackets = await GetItemsBetweenBrackets(uri);
            isValid = await CheckValidBetweenBrackets(betweenBrackets);

            if (isValid)
            {
                return betweenBrackets.Count;
            }
            else
            {
                return -1;
            }
        }

        public async Task<bool> CheckValidUri(string uri)
        {
            Stack stack = new Stack();

            foreach (char ch in uri)
            {
                if (ch == '{')
                {
                    stack.Push(ch);
                }
                else if (ch == '}')
                {
                    if (stack.Count > 0)
                    {
                        stack.Pop();
                    }
                    else
                    {
                        return false;
                    }
                }
            }

            bool isValid = stack.Count > 0 ? false : true;
            return isValid;
        }

        private async Task<string> reverseString(string myStr)
        {
            char[] myArr = myStr.ToCharArray();
            Array.Reverse(myArr);
            return new string(myArr);
        }

        public async Task<List<string>> GetItemsBetweenBrackets(string uri)
        {
            Stack stack = new Stack();
            StringBuilder betweenBracketsItem = new StringBuilder();
            List<string> betweenBrackets = new List<string>();
            bool flagPush = false;

            foreach (char ch in uri)
            {
                if (ch == '}')
                {
                    flagPush = false;
                    char chStack = (char)stack.Pop();

                    while (stack.Count != 0 && chStack != '{')
                    {
                        betweenBracketsItem.Append(chStack);
                        chStack = (char)stack.Pop();
                    }

                    string reversBtwnBrcktItem = await reverseString(betweenBracketsItem.ToString());

                    betweenBrackets.Add(reversBtwnBrcktItem);
                    betweenBracketsItem.Clear();
                    if(stack.Count > 0) { flagPush = true; }
                }
                else if (flagPush)
                {
                    stack.Push(ch);
                }
                else if (ch == '{')
                {
                    stack.Push(ch);
                    flagPush = true;
                }
            }

            return betweenBrackets;
        }

        public async Task<bool> CheckValidBetweenBrackets(List<string> betweenBrackets)
        {
            foreach (string betweenBracketsItem in betweenBrackets)
            {
                bool isNumeric = int.TryParse(betweenBracketsItem, out _);

                if (!isNumeric)
                {
                     return false;
                }
            }

            return true;
        }
    }
}
