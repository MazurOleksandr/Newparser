using System;
using System.Collections.Generic;
using System.Text;

namespace Newparser
{
    public sealed class RecursiveDescentParser
    {
        private readonly List<string> _tokens;
        private int _indexCurrentElement = 0; // індекс поточного токена
        private const string PLUS_OPERATOR = "+";
        private const string MINUS_OPERATOR = "-";
        private const string MUL_OPERATOR = "*";
        private const string DIV_OPERATOR = "/";
        public RecursiveDescentParser(List<string> tokens)
        {
            this._tokens = tokens;
        }

        public double ParseTokenList()
        {
            double result = Expression();
            if (_indexCurrentElement != _tokens.Count)
            {
                throw new Exception("Error in expression at " + _tokens[_indexCurrentElement]);
            }
            return result;
        }
        // E -> T±T±T±T± ... ±T
        private double Expression()
        {
            // знаходимо перший доданок
            double first = Term();
            while (_indexCurrentElement < _tokens.Count)
            {
                string operators = _tokens[_indexCurrentElement];
                if (!operators.Equals(PLUS_OPERATOR) && !operators.Equals(MINUS_OPERATOR))
                {
                    break;
                }
                else
                {
                    _indexCurrentElement++;
                }
                // знаходимо другий доданок (від'ємник)
                double second = Term();
                if (operators.Equals(PLUS_OPERATOR))
                {
                    first += second;
                }
                else
                {
                    first -= second;
                }
            }
            return first;
        }
        // T -> F*/F*/F*/*/ ... */F
        private double Term()
        {
            // знаходимо перший множник
            double first = Factor();
            while (_indexCurrentElement < _tokens.Count)
            {
                string operators = _tokens[_indexCurrentElement];
                if (!operators.Equals(MUL_OPERATOR) && !operators.Equals(DIV_OPERATOR))
                {
                    break;
                }
                else
                {
                    _indexCurrentElement++;
                }
                // знаходимо другий множник (дільник)
                double second = Factor();
                if (operators.Equals(MUL_OPERATOR))
                {
                    first *= second;
                }
                else
                {
                    first /= second;
                }
            }
            return first;
        }
        // F -> N | (E)
        private double Factor()
        {
            string next = _tokens[_indexCurrentElement];
            double result;
            if (next.Equals("("))
            {
                _indexCurrentElement++;
                // якщо вираз в дужках, то рекурсивно переходим на обробку підвиразу типу Е
                result = Expression();
                string closingBracket;
                if (_indexCurrentElement < _tokens.Count)
                {
                    closingBracket = _tokens[_indexCurrentElement];
                }
                else
                {
                    throw new Exception("Unexpected end of expression");
                }

                if (_indexCurrentElement < _tokens.Count && closingBracket.Equals(")"))
                {
                    _indexCurrentElement++;
                    return result;
                }

                throw new Exception("')' expected but " + closingBracket);
            }
            _indexCurrentElement++;
            // інакше токен має бути числом
            return double.Parse(next);
        }
    }
}

