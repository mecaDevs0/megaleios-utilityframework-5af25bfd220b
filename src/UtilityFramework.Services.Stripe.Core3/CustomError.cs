using System;

namespace UtilityFramework.Services.Stripe.Core3
{
    public class CustomError(string message) : Exception(message)
    {
    }
}