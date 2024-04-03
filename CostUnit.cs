using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOAPI
{
    //this as an model example of list Host company to check and compare
    public class HostCostUnit
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<List<Root>>(myJsonResponse);

        public string? UID { get; set; }
        public string? ID { get; set; }
        public DateTime LastModified { get; set; }
        public string? Description { get; set; }
        public string? AccessRuleUID { get; set; }
        public string? AccessRule { get; set; }
        public string? OwnerUID { get; set; }
        public string? Owner { get; set; }
        public string? VarioDescription { get; set; }
        public VarioTexts? VarioTexts { get; set; }
        public int ObjectIndex { get; set; }
        public string? CompanyUID { get; set; }
        public string? Company { get; set; }
        public string? Shadow1UID { get; set; }
        public string? Shadow2UID { get; set; }
        public string? Shadow3UID { get; set; }
        public string? Shadow4UID { get; set; }
        public string? Shadow5UID { get; set; }
        public string? Shadow6UID { get; set; }
        public string? Shadow7UID { get; set; }
        public string? Shadow8UID { get; set; }
        public string? Shadow1 { get; set; }
        public string? Shadow2 { get; set; }
        public string? Shadow3 { get; set; }
        public string? Shadow4 { get; set; }
        public string? Shadow5 { get; set; }
        public string? Shadow6 { get; set; }
        public string? Shadow7 { get; set; }
        public string? Shadow8 { get; set; }
        public int Level { get; set; }
        public string? Usability { get; set; }
        public string? Object0 { get; set; }
        public string? Object1 { get; set; }
        public string? Object2 { get; set; }
        public string? Object3 { get; set; }
        public string? Object4 { get; set; }
        public string? Object5 { get; set; }
        public string? Object6 { get; set; }
        public string? Object7 { get; set; }
        public string? Object8 { get; set; }
        public DateTime ValidFrom { get; set; }
        public DateTime ValidTo { get; set; }
        public DateTime Date { get; set; }
        public bool Best { get; set; }
        public string? Selection { get; set; }
        public string? State { get; set; }
        public string? Value { get; set; }
        public string? Query { get; set; }
        public string? RP { get; set; }
        public string? QueryColumns { get; set; }
        public int MaxDynamicRows { get; set; }
        public bool Overwrite { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string? CreatedBy { get; set; }
        public string? SelectionSettings { get; set; }
    }

    public class VarioTexts
    {
    }

}
