using Business.Contracts;

namespace Business.Providers
{
    public interface IPersonProvider
    {
        List<Person> GetAllPersons();

        List<Person> GetPersonsByName(string name);

        Person GetPersonById(int id);

        TransactionResponse SavePerson(Person person);        

        TransactionResponse DeletePerson(int personId);
    }
}
