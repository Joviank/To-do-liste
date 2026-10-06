namespace Calculator.Tests;

public class CalculatorTester
{
    [Fact]
    public void Add_TwoNumbers_ReturnsCorrectSum()
    {
        // Arrange: Sette opp testdata
        var calculator = new Calculator();

        // Act: Handlingen vi tester ut
        var result = calculator.Add(2, 2);

        // Assert: Sjekker resultatet
        Assert.Equal(4, result);

    }
    [Fact]
    public void Subtract_TwoNumbers_ReturnsCorrectSum()
    {
        // Arrange: Vi lager en kalkulator
        var calculator = new Calculator();

        // Act: Vi tar minus to tall
        var result = calculator.Subtract(2, 2);

        // Assert: Resultatet skal gi 0 i dette tilfellet
        Assert.Equal(0, result);
    }
    [Fact]
    public void Divide_TwoNumbers_ReturnsCorrectSum()
    {
        // Arrange
        var calculator = new Calculator();

        // Act
        var result = calculator.Divide(16, 2);

        // Assert
        Assert.Equal(8, result);
    }
    /*[Fact]
    public void Divide_ByZero_ThrowsException()
    {
        // Arrange
        var calculator = new Calculator();

        // Act & Assert
        Assert.Throws<DivideByZeroException>(() => calculator.Divide(56, 0));
    }*/
}
