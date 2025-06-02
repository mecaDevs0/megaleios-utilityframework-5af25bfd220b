using System;
using System.Collections.Generic;
using System.Linq;
using Stripe;
using UtilityFramework.Application.Core3;

namespace UtilityFramework.Services.Stripe.Core3
{
    public static class StripeHelper
    {
        public static string OrDefaultCurrency(this string currency)
            => (currency ?? "BRL").ToLower();
        public static string OrDefaultCountry(this string country)
            => (country ?? "BR").ToLower();

        public static string MapPhone(this string phone)
        {
            phone = phone.OnlyNumbers();

            if (string.IsNullOrEmpty(phone))
                return null;

            bool hasCountryCode = phone.StartsWith("55") && phone.Length >= 12;
            string countryCode = hasCountryCode ? phone[..2] : "55";
            string phoneNumber = hasCountryCode ? phone[2..] : phone;

            return $"+{countryCode}{phoneNumber}";
        }


        public static AddressOptions MapAddress(this StripeAddress stripeAddress)
        {
            return new()
            {
                Line1 = $"{stripeAddress.Street}, {stripeAddress.Number}".Trim().TrimEnd(','),
                Line2 = stripeAddress.Complement,
                City = stripeAddress.City,
                State = stripeAddress.State,
                PostalCode = stripeAddress.ZipCode?.OnlyNumbers(),
                Country = stripeAddress.Country.OrDefaultCountry()
            };
        }

        public static string GetRiskDetails(this ChargeOutcome outcome)
        {
            if (outcome == null)
                return null;

            var details = new List<string>();

            if (!string.IsNullOrEmpty(outcome.Type))
                details.Add($"Tipo: {outcome.Type}");

            if (outcome.RiskLevel != null)
                details.Add($"Nível de risco: {outcome.RiskLevel}");

            details.Add($"Score: {outcome.RiskScore}");

            if (!string.IsNullOrEmpty(outcome.Reason))
                details.Add($"Motivo: {outcome.Reason}");

            if (!string.IsNullOrEmpty(outcome.SellerMessage))
                details.Add($"Mensagem: {outcome.SellerMessage}");

            return details.Any() ? string.Join(" | ", details) : null;
        }

        public static AnyOf<string, EStripePaymentStatus> MapPaymentStatus(this string stripeStatus, Charge charge = null)
        {
            switch (stripeStatus.ToLower())
            {
                case "requires_payment_method":
                case "requires_confirmation":
                case "requires_action":
                case "processing":
                    return EStripePaymentStatus.Pending;

                case "succeeded":
                    if (charge != null && charge.Refunded)
                        return EStripePaymentStatus.Refunded;
                    if (charge != null && charge.AmountRefunded > 0)
                        return EStripePaymentStatus.RefundedPartailly;
                    if (charge != null && charge.Disputed)
                        return EStripePaymentStatus.Disputed;
                    return EStripePaymentStatus.Paid;

                case "requires_capture":
                    return EStripePaymentStatus.PreAuthorized;

                case "canceled":
                    return EStripePaymentStatus.Cancelled;

                case "failed":
                    return EStripePaymentStatus.Rejected;

                default:
                    return stripeStatus;
            }
        }
        public static string GetPaymentMethodDetails(this Charge charge)
        {
            if (charge.PaymentMethodDetails == null)
                return "N/A";

            switch (charge.PaymentMethodDetails.Type)
            {
                case "card":
                    var card = charge.PaymentMethodDetails.Card;
                    return $"{card.Brand} ****{card.Last4}";

                case "boleto":
                    var boleto = charge.PaymentMethodDetails.Boleto;
                    return $"Boleto {boleto.TaxId}";

                case "pix":
                    var pix = charge.PaymentMethodDetails.Pix;
                    return $"PIX (ID: {pix.BankTransactionId})";
                default:
                    return charge.PaymentMethodDetails.Type;
            }
        }

        public static readonly Dictionary<string, StripeAccountRequirement> RequirementMap = new(StringComparer.OrdinalIgnoreCase)
        {
            // Mapeamentos originais mantidos
            { "external_account", StripeAccountRequirement.BankAccount },
            { "individual.dob.day", StripeAccountRequirement.BirthDate },
            { "individual.dob.month", StripeAccountRequirement.BirthDate },
            { "individual.dob.year", StripeAccountRequirement.BirthDate },
            { "dob.day", StripeAccountRequirement.BirthDate },
            { "dob.month", StripeAccountRequirement.BirthDate },
            { "dob.year", StripeAccountRequirement.BirthDate },
            { "individual.verification.document", StripeAccountRequirement.IdentityDocument },
            { "individual.verification.additional_document", StripeAccountRequirement.AdditionalVerificationDocument },
            { "business.verification.document", StripeAccountRequirement.BusinessVerificationDocument },
            { "business_profile.product_description", StripeAccountRequirement.BusinessProductDescription },
            { "business_profile.support_phone", StripeAccountRequirement.BusinessSupportPhone },
            { "business_profile.url", StripeAccountRequirement.BusinessUrl },
            { "individual.address.line1", StripeAccountRequirement.Address },
            { "individual.address.city", StripeAccountRequirement.Address },
            { "individual.address.postal_code", StripeAccountRequirement.Address },
            { "individual.address.state", StripeAccountRequirement.Address },
            { "individual.email", StripeAccountRequirement.Email },
            { "individual.phone", StripeAccountRequirement.Phone },
            { "business.tax_id", StripeAccountRequirement.BusinessTaxId },
            { "individual.tax_id", StripeAccountRequirement.TaxId },
            { "individual.id_number", StripeAccountRequirement.TaxId },
            { "individual.ssn_last_4", StripeAccountRequirement.SsnLast4 },
            { "tos_acceptance.date", StripeAccountRequirement.AcceptTerms },
            { "tos_acceptance.ip", StripeAccountRequirement.AcceptTerms },

            // Novos mapeamentos adicionados
            { "business_profile.mcc", StripeAccountRequirement.MerchantCategoryCode },
            { "company.name", StripeAccountRequirement.CompanyName },
            { "company.address.line1", StripeAccountRequirement.BusinessAddress },
            { "company.address.postal_code", StripeAccountRequirement.BusinessAddress },
            { "company.address.city", StripeAccountRequirement.BusinessAddress },
            { "company.address.state", StripeAccountRequirement.BusinessAddress },
            { "company.phone", StripeAccountRequirement.CompanyPhone },
            { "company.tax_id", StripeAccountRequirement.BusinessTaxId },
            { "company.directors_provided", StripeAccountRequirement.DirectorsProvided },
            { "company.owners_provided", StripeAccountRequirement.OwnersProvided },
            { "company.executives_provided", StripeAccountRequirement.ExecutivesProvided },
            { "representative.first_name", StripeAccountRequirement.LegalRepresentativeName },
            { "representative.last_name", StripeAccountRequirement.LegalRepresentativeName },
            { "representative.dob.day", StripeAccountRequirement.LegalRepresentativeBirthDate },
            { "representative.dob.month", StripeAccountRequirement.LegalRepresentativeBirthDate },
            { "representative.dob.year", StripeAccountRequirement.LegalRepresentativeBirthDate },
            { "representative.address.line1", StripeAccountRequirement.LegalRepresentativeAddress },
            { "representative.address.postal_code", StripeAccountRequirement.LegalRepresentativeAddress },
            { "representative.address.city", StripeAccountRequirement.LegalRepresentativeAddress },
            { "representative.address.state", StripeAccountRequirement.LegalRepresentativeAddress },
            { "representative.email", StripeAccountRequirement.LegalRepresentativeEmail },
            { "representative.phone", StripeAccountRequirement.LegalRepresentativePhone },
            { "representative.political_exposure", StripeAccountRequirement.PoliticalExposure },
            { "representative.id_number", StripeAccountRequirement.TaxId },
            { "representative.relationship.title", StripeAccountRequirement.JobTitle },
            { "representative.relationship.executive", StripeAccountRequirement.ExecutiveConfirmation },
            { "directors.first_name", StripeAccountRequirement.DirectorName },
            { "directors.last_name", StripeAccountRequirement.DirectorName },
            { "directors.dob.day", StripeAccountRequirement.DirectorBirthDate },
            { "directors.dob.month", StripeAccountRequirement.DirectorBirthDate },
            { "directors.dob.year", StripeAccountRequirement.DirectorBirthDate },
            { "directors.address.line1", StripeAccountRequirement.DirectorAddress },
            { "directors.address.postal_code", StripeAccountRequirement.DirectorAddress },
            { "directors.address.city", StripeAccountRequirement.DirectorAddress },
            { "directors.address.state", StripeAccountRequirement.DirectorAddress },
            { "directors.email", StripeAccountRequirement.DirectorEmail },
            { "directors.political_exposure", StripeAccountRequirement.PoliticalExposure },
            { "directors.id_number", StripeAccountRequirement.TaxId },
            { "directors.relationship.title", StripeAccountRequirement.JobTitle },
            { "owners.first_name", StripeAccountRequirement.OwnerName },
            { "owners.last_name", StripeAccountRequirement.OwnerName },
            { "owners.dob.day", StripeAccountRequirement.OwnerBirthDate },
            { "owners.dob.month", StripeAccountRequirement.OwnerBirthDate },
            { "owners.dob.year", StripeAccountRequirement.OwnerBirthDate },
            { "owners.address.line1", StripeAccountRequirement.OwnerAddress },
            { "owners.address.postal_code", StripeAccountRequirement.OwnerAddress },
            { "owners.address.city", StripeAccountRequirement.OwnerAddress },
            { "owners.address.state", StripeAccountRequirement.OwnerAddress },
            { "owners.political_exposure", StripeAccountRequirement.PoliticalExposure },
            { "owners.id_number", StripeAccountRequirement.TaxId },
            { "owners.relationship.percent_ownership", StripeAccountRequirement.OwnershipPercentage },
            { "executives.first_name", StripeAccountRequirement.ExecutiveName },
            { "executives.last_name", StripeAccountRequirement.ExecutiveName },
            { "executives.dob.day", StripeAccountRequirement.ExecutiveBirthDate },
            { "executives.dob.month", StripeAccountRequirement.ExecutiveBirthDate },
            { "executives.dob.year", StripeAccountRequirement.ExecutiveBirthDate },
            { "executives.address.line1", StripeAccountRequirement.ExecutiveAddress },
            { "executives.address.postal_code", StripeAccountRequirement.ExecutiveAddress },
            { "executives.address.city", StripeAccountRequirement.ExecutiveAddress },
            { "executives.address.state", StripeAccountRequirement.ExecutiveAddress },
            { "executives.email", StripeAccountRequirement.ExecutiveEmail },
            { "executives.political_exposure", StripeAccountRequirement.PoliticalExposure },
            { "executives.id_number", StripeAccountRequirement.TaxId },
            { "executives.relationship.title", StripeAccountRequirement.JobTitle },
            { "individual.first_name", StripeAccountRequirement.IndividualName },
            { "individual.last_name", StripeAccountRequirement.IndividualName }
        };


        public static List<StripeAccountRequirement> MapRequirements(this List<string> stripeRequirements)
        {
            var mapped = new HashSet<StripeAccountRequirement>();

            for (int i = 0; i < stripeRequirements.Count; i++)
            {
                var requirement = stripeRequirements[i];

                if (requirement.ContainsIgnoreCase("person"))
                {
                    var firstDotIndex = requirement.IndexOf('.');
                    requirement = string.Concat("individual", requirement.AsSpan(firstDotIndex));
                }

                if (RequirementMap.TryGetValue(requirement, out var mappedRequirement))
                {
                    mapped.Add(mappedRequirement);
                    continue;
                }

                mappedRequirement = requirement switch
                {
                    var s when s.Contains(".dob", StringComparison.OrdinalIgnoreCase)
                        => StripeAccountRequirement.BirthDate,

                    var s when s.Contains(".verification.document", StringComparison.OrdinalIgnoreCase)
                        => s.StartsWith("business.", StringComparison.OrdinalIgnoreCase)
                            ? StripeAccountRequirement.BusinessVerificationDocument
                            : StripeAccountRequirement.IdentityDocument,

                    var s when s.Contains(".address", StringComparison.OrdinalIgnoreCase)
                        => StripeAccountRequirement.Address,

                    var s when s.Contains(".tax_id", StringComparison.OrdinalIgnoreCase) ||
                                s.Contains(".id_number", StringComparison.OrdinalIgnoreCase)
                        => s.StartsWith("business.", StringComparison.OrdinalIgnoreCase)
                            ? StripeAccountRequirement.BusinessTaxId
                            : StripeAccountRequirement.TaxId,

                    var s when s.Contains(".additional_document", StringComparison.OrdinalIgnoreCase)
                        => StripeAccountRequirement.AdditionalVerificationDocument,

                    var s when s.Contains(".political_exposure", StringComparison.OrdinalIgnoreCase)
                        => StripeAccountRequirement.PoliticalExposure,

                    _ => StripeAccountRequirement.Unknown
                };

                mapped.Add(mappedRequirement);
            }

            return [.. mapped.OrderBy(x => x)];
        }

        public static (bool skipUploadDocumentCompany, bool skipUploadDocumentIndividual) GetSkipDocuments(this List<string> requirements)
        {
            var individualOrPerson = new HashSet<string>() { "person", "individual" };
            var company = new HashSet<string>() { "company", "business" };

            var skipUploadDocumentIndividual = !requirements.Any(requirement => individualOrPerson.Any(option => requirement.ContainsIgnoreCase(option)) && requirement.ContainsIgnoreCase("verification"));
            var skipUploadDocumentCompany = !requirements.Any(requirement => company.Any(option => requirement.ContainsIgnoreCase(option)) && requirement.ContainsIgnoreCase("verification"));

            return (skipUploadDocumentCompany, skipUploadDocumentIndividual);
        }

    }
}