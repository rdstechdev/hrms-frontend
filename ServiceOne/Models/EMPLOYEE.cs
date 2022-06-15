using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceOne.Models
{
    public partial class EMPLOYEE
    {
        public int id { get; set; }
        public string employee_no { get; set; }
        public string title { get; set; }
        public string full_name { get; set; }
        public string family_name { get; set; }
        public string nick_name { get; set; }
        public string birth_place { get; set; }
        public Nullable<System.DateTime> birth_date { get; set; }
        public string gender { get; set; }
        public string marital_status { get; set; }
        public string blood_type { get; set; }
        public string religion_code { get; set; }
        public string nationality_code { get; set; }
        public string race_code { get; set; }
        public string personal_mobile_phone { get; set; }
        public string personal_email_address { get; set; }
        public string company_email_address { get; set; }
        public Nullable<bool> flag_expatriate { get; set; }
        public string expatriate_register_no { get; set; }
        public Nullable<System.DateTime> expatriate_register_start_date { get; set; }
        public Nullable<System.DateTime> expatriate_register_end_date { get; set; }
        public string note { get; set; }
        public Nullable<int> active_status { get; set; }
        public Nullable<bool> flag_has_prepared { get; set; }
        public string global_id { get; set; }
        public string absenteeism_no { get; set; }
        public Nullable<System.DateTime> join_date { get; set; }
        public string employment_status { get; set; }
        public Nullable<System.DateTime> probation_end_date { get; set; }
        public Nullable<System.DateTime> start_contract_date { get; set; }
        public Nullable<System.DateTime> end_contract_date { get; set; }
        public Nullable<int> contract_sequence_no { get; set; }
        public string first_join_employee_no { get; set; }
        public Nullable<System.DateTime> resign_date { get; set; }
        public string resign_code { get; set; }
        public string resign_remark { get; set; }
        public string grade_code { get; set; }
        public string position_code { get; set; }
        public string rank_code { get; set; }
        public string level1_code { get; set; }
        public string level2_code { get; set; }
        public string level3_code { get; set; }
        public string level4_code { get; set; }
        public string level5code { get; set; }
        public string work_nature_code { get; set; }
        public string work_group_code { get; set; }
        public string work_group_authorize_code { get; set; }
        public string employee_occupation_note { get; set; }
        public string entity { get; set; }
        public string tax_registered_no { get; set; }
        public Nullable<System.DateTime> tax_registered_date { get; set; }
        public string tax_marital_status { get; set; }
        public string tax_marital_status_next_year { get; set; }
        public Nullable<int> tax_calculation_method { get; set; }
        public string general_tax_payer { get; set; }
        public string non_tax_registered_tax_payer { get; set; }
        public string jamsostek_no { get; set; }
        public string jamsostek_marital_status { get; set; }
        public Nullable<System.DateTime> tax_join_date { get; set; }
        public Nullable<System.DateTime> tax_resign_date { get; set; }
        public Nullable<System.DateTime> payroll_join_date { get; set; }
        public Nullable<System.DateTime> payroll_resign_date { get; set; }
        public Nullable<System.DateTime> jamsostek_join_date { get; set; }
        public Nullable<System.DateTime> jamsostek_resign_date { get; set; }
        public Nullable<System.DateTime> active_jamsostek_date { get; set; }
        public string jamsostek_calculation_method { get; set; }
        public Nullable<decimal> employee_pension_percentage { get; set; }
        public Nullable<decimal> company_pension_percentage { get; set; }
        public Nullable<decimal> accident_insurance_percentage { get; set; }
        public Nullable<decimal> death_insurance_percentage { get; set; }
        public Nullable<decimal> health_insurance_single_percentage { get; set; }
        public Nullable<decimal> health_insurance_married_percentage { get; set; }
        public Nullable<decimal> employee_health_insurance_percentage { get; set; }
        public Nullable<decimal> company_health_insurance_percentage { get; set; }
        public Nullable<decimal> employee_pension_security_percentage { get; set; }
        public Nullable<decimal> company_pension_security_percentage { get; set; }
        public Nullable<int> health_insuran_cemax { get; set; }
        public Nullable<int> pension_security_max { get; set; }
        public Nullable<int> pension_security_max_age { get; set; }
        public string health_insurance_no { get; set; }
        public string jamsostek_insurance_no { get; set; }
        public Nullable<bool> flags_how_resign_onreport_jamsostek_1c { get; set; }
        public Nullable<bool> flags_how_new_employee_on_report_jamsostek_1a { get; set; }
        public Nullable<bool> flag_tax_return_to_employee_according_pmk1 { get; set; }
        public Nullable<bool> flag_tax_return_to_employee_according_pmk2 { get; set; }
        public string employee_tax_jamsostek_note { get; set; }
        public string last_modified_by { get; set; }
        public Nullable<System.DateTime> last_modified_date { get; set; }
    }
}