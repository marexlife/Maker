using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Maker.Tests;

[TestClass]
public class ConverterTester
{
    [TestMethod]
    public void Test()
    {
        var input = "ProjectName";
        var expectedOutput = "PROJECT_NAME";

        var actualOutput = Converter.ToScreamingSnakeCase(input);

        Console.WriteLine($"expected output: {expectedOutput}");
        Console.WriteLine($"actual output: {actualOutput}");

        Assert.IsTrue(expectedOutput == actualOutput);
    }
}