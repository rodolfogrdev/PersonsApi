using Business.Contracts;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;

namespace ApiTests
{
    public class PersonTests
    {
        private readonly HttpClient _httpClient;
        public PersonTests()
        {
            var webApplicationFactory = new WebApplicationFactory<Program>();
            _httpClient = webApplicationFactory.CreateDefaultClient();
        }

        [Fact]
        public async Task PersonGet_ReturnsListOfPersons()
        {
            var response = await _httpClient.GetAsync("person");
            var result = await response.Content.ReadAsStringAsync();
            var persons = JsonConvert.DeserializeObject<List<Person>>(result);
            Assert.True(persons?.Count > 0);
        }

        [Fact]
        public async Task PersonGetById_ReturnsSinglePerson()
        {
            var response = await _httpClient.GetAsync("person/1");
            var result = await response.Content.ReadAsStringAsync();
            var person = JsonConvert.DeserializeObject<Person>(result);
            Assert.True(person != null);
        }

        [Fact]
        public async Task PersonSave_AddsANewPerson()
        {
            var response = await _httpClient.GetAsync("person");
            var result = await response.Content.ReadAsStringAsync();
            var persons = JsonConvert.DeserializeObject<List<Person>>(result);
            var numberOfPersonsBeforeSaving = persons?.Count;

            var jsonPerson = JsonConvert.SerializeObject(new Person { Name = "Test", Address = "testAddress", PhoneNumber= "1234565789", EmailAddress = "mail@domain.com" });

            var httpContent = new StringContent(jsonPerson, System.Text.Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("person", httpContent);

            var afterSaveResponse = await _httpClient.GetAsync("person");
            var afterSaveResult = await afterSaveResponse.Content.ReadAsStringAsync();
            var afterSavePersons = JsonConvert.DeserializeObject<List<Person>>(afterSaveResult);
            var numberOfPersonsAfterSaving = afterSavePersons?.Count;

            Assert.True(numberOfPersonsAfterSaving -1 == numberOfPersonsBeforeSaving);
        }

        [Fact]
        public async Task PersonUpdate_UpdatesAPerson()
        {
            var response = await _httpClient.GetAsync("person");
            var result = await response.Content.ReadAsStringAsync();
            var persons = JsonConvert.DeserializeObject<List<Person>>(result);
            var numberOfPersonsBeforeSaving = persons?.Count;

            var jsonPerson = JsonConvert.SerializeObject(new Person { Name = "Test", Address = "testAddress", PhoneNumber = "1234565789", EmailAddress = "mail@domain.com" });

            var httpContent = new StringContent(jsonPerson, System.Text.Encoding.UTF8, "application/json");

            await _httpClient.PostAsync("person", httpContent);

            var afterSaveResponse = await _httpClient.GetAsync("person");
            var afterSaveResult = await afterSaveResponse.Content.ReadAsStringAsync();
            var afterSavePersons = JsonConvert.DeserializeObject<List<Person>>(afterSaveResult);

            var newlyAddedPersonId = (int)(afterSavePersons?.FirstOrDefault().Id);
            var jsonUpdatePerson = JsonConvert.SerializeObject(new Person {Id = newlyAddedPersonId, Name = "Test2" });
            var httpUpdateContent = new StringContent(jsonUpdatePerson, System.Text.Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("person", httpUpdateContent);
            var responseAfterUpdate = await _httpClient.GetAsync("person/" + newlyAddedPersonId);
            var resultAfterUpdate = await responseAfterUpdate.Content.ReadAsStringAsync();
            var newPerson = JsonConvert.DeserializeObject<Person>(resultAfterUpdate);

            Assert.True(newPerson?.Name == "test2");
        }

        [Fact]
        public async Task PersonSave_ValidateNameLongerThanOneHundredAndFiftyCharacters()
        {
            var jsonPerson = JsonConvert.SerializeObject(
                new Person
                {
                    Name = new string('r', 151)
                });

            var httpContent = new StringContent(jsonPerson, System.Text.Encoding.UTF8, "application/json");

            var saveResponse = await _httpClient.PostAsync("person", httpContent);

            var result = await saveResponse.Content.ReadAsStringAsync();

            Assert.Contains("cannot be longer than 150 characters", result);
        }
        
    }
}