using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOAPI
{
    public class HostCompany
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);
        public string? UID { get; set; }
        public string? ID { get; set; }
        public DateTime? LastModified { get; set; }
        public string? AccessRuleUID { get; set; }
        public string? AccessRule { get; set; }
        public string? Description { get; set; }
        public string? Shadow1UID { get; set; }
        public string? Shadow2UID { get; set; }
        public string? Shadow3UID { get; set; }
        public string? Shadow4UID { get; set; }
        public string? Shadow5UID { get; set; }
        public string? Shadow6UID { get; set; }
        public string? Shadow7UID { get; set; }
        public string? Shadow8UID { get; set; }
        public string? Shadow9UID { get; set; }
        public string? Shadow10UID { get; set; }
        public string? Shadow1 { get; set; }
        public string? Shadow2 { get; set; }
        public string? Shadow3 { get; set; }
        public string? Shadow4 { get; set; }
        public string? Shadow5 { get; set; }
        public string? Shadow6 { get; set; }
        public string? Shadow7 { get; set; }
        public string? Shadow8 { get; set; }
        public string? Shadow9 { get; set; }
        public string? Shadow10 { get; set; }
        public List<Address>? Addresses { get; set; }
        public List<Bank>? Banks { get; set; }
        public Notes? Notes1 { get; set; }
        public VarioTexts? VarioTexts1 { get; set; }
        public string? VarioDescription { get; set; }
        public string? YearGroupUID { get; set; }
        public string? YearGroup { get; set; }
        public bool? CreateYearGroup { get; set; }
        public string? SourceCurrency { get; set; }
        public bool? DistinctAccounts { get; set; }
        public string? Selection { get; set; }
        public string? VatHandler { get; set; }
        public string? PublicID { get; set; }
        public string? Query { get; set; }
        public string? Value { get; set; }
        public string? VatPeriodReporting { get; set; }
        public string? VatRegistration { get; set; }
        public string? InvoiceMail { get; set; }
        public string? InvoiceLayoutProjectUID { get; set; }
        public string? InvoiceLayoutArticleUID { get; set; }
        public string? InvoiceLayoutReminderUID { get; set; }
        public string? InvoiceLayoutInterestUID { get; set; }
        public bool? UseOCR { get; set; }
        public string? Culture { get; set; }
        public string? ChecksumUID { get; set; }
        public string? Checksum { get; set; }
        public string? RP { get; set; }
        public string? QueryColumns { get; set; }
        public int? MaxDynamicRows { get; set; }
        public DateTime? TransactionDate { get; set; }
        public bool? Overwrite { get; set; }
        public string? State { get; set; }
        public string? PeriodSchemaUID { get; set; }
        public string? CountryCode { get; set; }
        public string? Year { get; set; }
        public string? Method { get; set; }
        public DateTime? Date { get; set; }
        public string? SelectionSettings { get; set; }
        public string? OurReference { get; set; }
        public class Address
        {
            public string? UID { get; set; }
            public string? OwnerUID { get; set; }
            public string? ShadowUID { get; set; }
            public string? Type { get; set; }
            public string? ID { get; set; }
            public string? Address1 { get; set; }
            public string? Address2 { get; set; }
            public string? ZipCode { get; set; }
            public string? City { get; set; }
            public string? Country { get; set; }
            public string? State { get; set; }
            public string? Phone { get; set; }
            public string? Fax { get; set; }
            public string? Mail { get; set; }
            public string? HomePage { get; set; }
            public bool? IsPrimary { get; set; }
            public string? VarioID { get; set; }
            public string? InvoiceLanguage { get; set; }
            public string? InvoiceMail { get; set; }
            public string? Address3 { get; set; }
            public string? Address4 { get; set; }
            public string? GLN { get; set; }
            public string? ElectronicID { get; set; }
            public string? Selection { get; set; }
            public string? SelectionSettings { get; set; }
            public string? ClaimMail { get; set; }
            public string? YourReference { get; set; }
            public string? OurReference { get; set; }
        }

        public class Bank
        {
            public string? UID { get; set; }
            public string? ID { get; set; }
            public DateTime? LastModified { get; set; }
            public string? OwnerUID { get; set; }
            public string? OwnerType { get; set; }
            public string? BankTypeUID { get; set; }
            public string? BankType { get; set; }
            public string? Name { get; set; }
            public string? ClearingNumber { get; set; }
            public string? AccountNumber { get; set; }
            public string? BIC { get; set; }
            public string? IBAN { get; set; }
            public string? Shadow1UID { get; set; }
            public string? Shadow2UID { get; set; }
            public string? Shadow3UID { get; set; }
            public string? Shadow4UID { get; set; }
            public string? Shadow5UID { get; set; }
            public string? Shadow6UID { get; set; }
            public string? Shadow1 { get; set; }
            public string? Shadow2 { get; set; }
            public string? Shadow3 { get; set; }
            public string? Shadow4 { get; set; }
            public string? Shadow5 { get; set; }
            public string? Shadow6 { get; set; }
            public string? AccountUID { get; set; }
            public string? Account { get; set; }
            public bool? Default { get; set; }
            public List<Address>? Addresses { get; set; }
            public string? MatchType { get; set; }
            public string? ChecksumUID { get; set; }
            public string? Checksum { get; set; }
            public DateTime? Cancelled { get; set; }
            public string? CancelledBy { get; set; }
            public string? BankTypeDescription { get; set; }
        }
        public class Notes
        {
        }
        public class VarioTexts
        {
        }


    }
}
