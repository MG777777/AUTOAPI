using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AUTOAPI
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);
    public class CustomField
    {
        public string? name { get; set; }
        public string? value { get; set; }
    }
    public class ClientcompanyPerson
    {
        public int id { get; set; }
        public DateTime? created_at { get; set; }
        public string? uuid { get; set; }
        public string? locale { get; set; }
        public int account_type { get; set; }
        public int activation_behaviour { get; set; }
        public int account_status { get; set; }
        public bool is_manager { get; set; }
        public DateTime? hire_date { get; set; }
        public PersonalData? personal_data { get; set; }
        public JobInformation? job_information { get; set; }
    }
    public class Meta
    {
        public int limit { get; set; }
        public int offset { get; set; }
        public int count { get; set; }
        public int total { get; set; }
    }
    public class PersonalData
    {
        public string? uuid { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public int gender { get; set; }
        public string? personal_id_number { get; set; }
        public string? work_email { get; set; }
        public DateTime? hire_date { get; set; }
        public string? work_phone { get; set; }
        public string? mobile_phone { get; set; }
        public DateTime? birth_date { get; set; }
        public string? location_country { get; set; }
        public string? location_city { get; set; }
        public string? location_street { get; set; }
        public string? location_zip { get; set; }
        public string? mobile_work_phone { get; set; }
        public List<CustomField>? custom_fields { get; set; }
        public string? personal_email { get; set; }
        public string? citizenship { get; set; }
        public string? location_region { get; set; }
        public string? pin_country { get; set; }
        public string? home_phone { get; set; }
    }
    public class BusinessUnit
    {
        public string? uuid { get; set; }
        public string? name { get; set; }
        public int type { get; set; }
    }
    public class Employer
    {
        public string? uuid { get; set; }
        public string? name { get; set; }
        public string? description { get; set; }
        public string? vat_number { get; set; }
        public string? org_number { get; set; }
        public string? phone { get; set; }
        public string? url { get; set; }
        public string? locale { get; set; }
        public string? date_format { get; set; }
        public string? time_zone { get; set; }
        public string? time_format { get; set; }
        public string? currency_code { get; set; }
        public string? country_code { get; set; }
        public Location? location { get; set; }
    }
    public class Position
    {
        public string? uuid { get; set; }
        public string? name { get; set; }
    }
    public class Location
    {
        public string? uuid { get; set; }
        public string? name { get; set; }
        public string? city { get; set; }
        public string? country_code { get; set; }
    }
    public class Department
    {
        public string? uuid { get; set; }
        public string? name { get; set; }
        public string? color { get; set; }
    }
    public class Manager
    {
        public int id { get; set; }
        public DateTime? created_at { get; set; }
        public string? uuid { get; set; }
        public int status { get; set; }
        public string? locale { get; set; }
        public int account_type { get; set; }
        public int activation_behaviour { get; set; }
        public bool ats_hire { get; set; }
        public int account_status { get; set; }
        public bool is_manager { get; set; }
        public DateTime? hire_date { get; set; }
        public string? personal_id_number { get; set; }
        public string? first_name { get; set; }
        public string? last_name { get; set; }
        public string? work_email { get; set; }
        public string? work_phone { get; set; }
    }
    public class JobInformation
    {
        public string? uuid { get; set; }
        public string? employee_id { get; set; }
        public DateTime? effective_date { get; set; }
        public bool is_current { get; set; }
        public Position? position { get; set; }
        public BusinessUnit? business_unit { get; set; }
        public Department? department { get; set; }
        public List<Team>? teams { get; set; }
        public Location? location { get; set; }
        public Manager? manager { get; set; }
        public Employer? employer { get; set; }
    }
    public class Team
    {
        public string? uuid { get; set; }
        public string? name { get; set; }
    }
    public class Root
    {
        public Meta? meta { get; set; }
        public List<ClientcompanyPerson>? data { get; set; }
    }
}
