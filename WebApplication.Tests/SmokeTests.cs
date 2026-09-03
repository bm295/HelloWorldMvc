using MilkCoPOS.Models;
using Xunit;

namespace WebApplication.Tests;

public class SmokeTests
{
    [Fact]
    public void ProcessPaymentRequest_has_expected_defaults()
    {
        var request = new ProcessPaymentRequest();

        Assert.Equal(0m, request.Amount);
        Assert.Equal("Cash", request.Method);
    }
}
