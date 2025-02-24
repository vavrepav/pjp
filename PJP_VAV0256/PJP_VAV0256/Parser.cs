namespace PJP_VAV0256;

internal class Parser
{
    private readonly string _text;
    private int _pos;
    private char _currentChar;

    public Parser(string text)
    {
        _text = text.Replace(" ", "").Replace("\t", "");
        _pos = 0;
        _currentChar = _pos < _text.Length ? _text[_pos] : '\0';
    }

    private void Advance()
    {
        _pos++;
        _currentChar = _pos < _text.Length ? _text[_pos] : '\0';
    }

    private void SkipWhitespace()
    {
        while (_currentChar is ' ' or '\t')
            Advance();
    }
    
    private int ParseNumber()
    {
        var start = _pos;
        while (char.IsDigit(_currentChar))
            Advance();
        if (start == _pos)
            throw new Exception("Invalid number");
        var numberStr = _text.Substring(start, _pos - start);
        return int.Parse(numberStr);
    }
    
    private int ParseFactor()
    {
        if (_currentChar == '(')
        {
            Advance();
            var result = ParseExpression();
            if (_currentChar != ')')
                throw new Exception("Missing closing parenthesis");
            Advance();
            return result;
        }

        if (char.IsDigit(_currentChar))
        {
            return ParseNumber();
        }

        throw new Exception("Unexpected character in factor");
    }
    
    private int ParseTerm()
    {
        var result = ParseFactor();
        while (_currentChar is '*' or '/')
        {
            var op = _currentChar;
            Advance();
            var right = ParseFactor();
            if (op == '*')
                result *= right;
            else
            {
                if (right == 0)
                    throw new Exception("Division by zero");
                result /= right;
            }
        }
        return result;
    }
    
    public int ParseExpression()
    {
        var result = ParseTerm();
        while (_currentChar is '+' or '-')
        {
            var op = _currentChar;
            Advance();
            var right = ParseTerm();
            if (op == '+')
                result += right;
            else
                result -= right;
        }
        return result;
    }
    
    public void ExpectEnd()
    {
        if (_pos < _text.Length)
            throw new Exception("Extra characters at the end");
    }
}