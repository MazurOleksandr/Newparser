using System;
using System.Collections.Generic;
using System.Text;

namespace Newparser
{
    public sealed class ParseStringToTokenList
    {
        private string _stringNumbers = string.Empty;
        private string _stringOperators = string.Empty;
        private readonly string _listOperators = "+-/*()";
        public readonly List<string> _tokenList = new List<string>();

        private bool IsDigit(char symbol)
        {
            for (int i = 0; i < 9; i++)
                if (!char.IsDigit(symbol)) return false;
            return true;
        }

        private bool Isoperator(char symbol)
        {
            for (int i = 0; i < _listOperators.Length; i++)
                if (symbol == _listOperators[i]) return true;
            return false;
        }
        public List<string> GetTokenListFromString(string userInput)
        {
            for (int index = 0; index < userInput.Length; index++)
            {
                if (Isoperator(userInput[index]))
                {
                    _stringOperators = _stringOperators + userInput[index];
                    if (!string.IsNullOrEmpty(_stringNumbers))
                    {
                        _tokenList.Add(_stringNumbers);
                    }
                    _tokenList.Add(_stringOperators);
                    _stringNumbers = "";
                    _stringOperators = "";
                }

                if (IsDigit(userInput[index]))
                {
                    _stringNumbers = _stringNumbers + userInput[index];
                }

                if (index == userInput.Length - 1)
                {
                    if (!string.IsNullOrEmpty(_stringNumbers))
                    {
                        _tokenList.Add(_stringNumbers);
                    }
                }
            }
            return _tokenList;
        }
    }
}
