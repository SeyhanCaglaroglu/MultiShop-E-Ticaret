﻿using MultiShop.DtoLayer.CatalogDtos.ContactDtos;

namespace MultiShop.WebUI.Services.CatalogServices.ContactServices
{
    public class ContactService : IContactService
    {
        private readonly HttpClient _httpClient;

        public ContactService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task CreateContactAsync(CreateContactDto createContactDto)
        {
            await _httpClient.PostAsJsonAsync<CreateContactDto>("Contacts", createContactDto);
        }

        public async Task DeleteContactAsync(string id)
        {
            await _httpClient.DeleteAsync("Contacts?id=" + id);
        }

        public async Task<List<ResultContactDto>> GetAllContactsAsync()
        {
            var responseMessage = await _httpClient.GetAsync("Contacts");
            var values = await responseMessage.Content.ReadFromJsonAsync<List<ResultContactDto>>();
            return values;
        }

        public async Task<GetByIdContactDto> GetByIdContactAsync(string id)
        {
            var responseMessage = await _httpClient.GetAsync("Contacts/" + id);
            var value = await responseMessage.Content.ReadFromJsonAsync<GetByIdContactDto>();
            return value;
        }

        public async Task UpdateContactAsync(UpdateContactDto updateContactDto)
        {
            await _httpClient.PutAsJsonAsync<UpdateContactDto>("Contacts", updateContactDto);
        }
    }
}
