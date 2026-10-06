namespace Calculator;

public class Calculator
{
    public int Add(int a, int b)
    {
        return a+b;
    }
    public int Subtract(int a, int b)
    {
        return a-b;
    }
    public int Divide(int a, int b)
    {
        // I C# .NET 10, så kastes DivideByZeroException automatisk ut uten at vi trenger en if-statment.
        /*if(b == 0)
        {
            throw new DivideByZeroException();
        }*/
        return a/b;
    }
}
