using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using SharpArtileri.Data;

namespace SharpArtileri.Services
{
    public class ManagementProvider
    {
        private readonly ManagementDataContext context;        

        public ManagementProvider(ManagementDataContext context)
        {
            this.context = context;            
        }

        public string GetConnectionString(string companyCode)
        {
            var company = context.Companies.SingleOrDefault(comp => comp.Code == companyCode);
            return company == null ? String.Empty : company.ConnectionString;
        }

        public Company GetCompany(string companyCode)
        {
            return context.Companies.SingleOrDefault(comp => comp.Code == companyCode);
        }

        public IEnumerable<Company> GetCompanies()
        {
            return context.Companies;
        }

        public void CreateCompany(string code, string name, string server, string userid, string password, string database)
        {
            // Data Source=.;Initial Catalog=sharpartileri;Integrated Security=True
            const string connectionString = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
            var company = new Company();
            company.Code = code;
            company.Name = name;
            company.ConnectionString = String.Format(connectionString, server, database, userid, password);
            context.Companies.InsertOnSubmit(company);
            context.SubmitChanges();

            using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ManagementConnectionString"].ConnectionString))
            {
                conn.Open();
                using (var cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "CREATE DATABASE " + database;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandTimeout = 0;
                    cmd.ExecuteNonQuery();

                }
            }
            
        }

        public void UpdateCompany(string code, string name, string server, string userid, string password, string database)
        {
            // Data Source=.;Initial Catalog=sharpartileri;Integrated Security=True
            const string connectionString = @"Data Source={0};Initial Catalog={1};User ID={2};Password={3}";
            var company = context.Companies.SingleOrDefault(comp => comp.Code == code);
            company.Code = code;
            company.Name = name;
            company.ConnectionString = String.Format(connectionString, server, database, userid, password);            
            context.SubmitChanges();
        }

        public void DeleteCompany(string code)
        {
            var company = context.Companies.SingleOrDefault(comp => comp.Code == code);
            if (company != null)
            {                
                string connectionString = company.ConnectionString;
                string[] connectionStringElements = connectionString.Split(';');
                string database = connectionStringElements[1].Split('=')[1];
                using (var conn = new SqlConnection(ConfigurationManager.ConnectionStrings["ManagementConnectionString"].ConnectionString))
                {
                    conn.Open();
                    using (var cmd = conn.CreateCommand())
                    {                        
                        cmd.CommandText = "DROP DATABASE " + database;
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandTimeout = 0;
                        cmd.ExecuteNonQuery();

                    }
                }


                context.Companies.DeleteOnSubmit(company);
                context.SubmitChanges();
            }

            
        }

        public void UpdateCompanyInformation(string code,
                                             string name,
                                             string address1,
                                             string address2,
                                             string poApproverName)
        {
            var company = GetCompany(code);
            company.Name = name;
            company.Address1 = address1;
            company.Address2 = address2;
            company.POApproverName = poApproverName;
            context.SubmitChanges();
        }
    }
}
