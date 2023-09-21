using Business.Contracts;
using Business.Helpers;
using DataAccess;
using System.Data;
using System.Data.SqlClient;

namespace Business.Providers
{
    public class PersonProvider : IPersonProvider
    {
        private readonly IDatabaseManager _dataBaseManager;
        private readonly IGenericHelper _genericHelper;

        public PersonProvider(IDatabaseManager dataBaseManager, IGenericHelper genericHelper)
        {
            _dataBaseManager = dataBaseManager;
            _genericHelper = genericHelper;
        }

        public TransactionResponse DeletePerson(int personId)
        {
            var transactionResponse = new TransactionResponse();
            
            var sqlParams = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@PersonId",
                    Value = personId
                }
            };

            try
            {
                var dbresult = _dataBaseManager.ExecuteNonQuery("spDeletePersonById", sqlParams);
                if (!string.IsNullOrEmpty(dbresult)) transactionResponse.Errors.Add(dbresult);

                transactionResponse.IsSuccessful = transactionResponse.Errors.Count == 0;
                return transactionResponse;
            }
            catch (Exception)
            {
                return transactionResponse;
            }
        }

        public List<Person> GetAllPersons()
        {
            var dtResult = _dataBaseManager.ExecuteQuery("spGetAllPersons", new List<SqlParameter>());
            return _genericHelper.ConvertToGenericList<Person>(dtResult);
        }

        public List<Person> GetPersonsByName(string name)
        {
            var sqlParams = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@Name",
                    Value = name
                }
            };

            var dtResult = _dataBaseManager.ExecuteQuery("spGetPersonsByName", sqlParams);
            return _genericHelper.ConvertToGenericList<Person>(dtResult);            
        }

        public Person? GetPersonById(int id)
        {
            var sqlParams = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "id",
                    Value = id
                }
            };
            var dtResult = _dataBaseManager.ExecuteQuery("spGetPersonById", sqlParams);
            if(dtResult.Rows.Count > 0)
            {
                return _genericHelper.ConvertToGenericList<Person>(dtResult).FirstOrDefault();                
            }

            return null;
        }

        public TransactionResponse SavePerson(Person Person)
        {
            TransactionResponse transactionResponse;
            if(!Validate(Person, out transactionResponse)) return transactionResponse;

            var listofPersons = new List<Person> { Person };
            var sqlParams = new List<SqlParameter>
            {
                new SqlParameter
                {
                    ParameterName = "@Persons",
                    Value = _genericHelper.ConvertToDataTable(listofPersons),
                    SqlDbType = SqlDbType.Structured
                }
            };

            try
            {
                var dbresult = _dataBaseManager.ExecuteNonQuery("spMergePersons", sqlParams);
                if (!string.IsNullOrEmpty(dbresult)) transactionResponse.Errors.Add(dbresult);

                transactionResponse.IsSuccessful = transactionResponse.Errors.Count == 0;
                return transactionResponse;
            }catch (Exception)
            {
                return transactionResponse;
            }
        }        

        private bool Validate(Person Person, out TransactionResponse response)
        {            
            response = new TransactionResponse()
            {
                IsSuccessful = false,
                Errors = new List<string>()
            };

            if (Person == null) response.Errors.Add("The Person's data is empty");
            else
            {
                if (string.IsNullOrEmpty(Person.Name)) response.Errors.Add("The Person's name is required");
                if (Person.Name.Length > 150) response.Errors.Add("The Person's name cannot be longer than 150 characters");
                if (string.IsNullOrEmpty(Person.Address)) response.Errors.Add("The Person's address is required");
                if (Person.Address.Length > 150) response.Errors.Add("The Person's address cannot be longer than 150 characters");
            }

            response.IsSuccessful = response.Errors?.Count == 0;

            return response.IsSuccessful;
        }
    }
}
