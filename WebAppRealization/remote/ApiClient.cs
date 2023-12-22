using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using WebAppIImpl.remote;
using WebAppIImpl.remote.models;

public class ApiClient
{
    private readonly HttpClient _httpClient;

    public ApiClient()
    {
        _httpClient = new HttpClient();
        
        _httpClient.BaseAddress = new Uri("https://localhost:7276/api/");
    }

    public async Task<string?>? LoginUserAsync(string username, string password)
    {
       
        var authData = new { Username = username, Password = password };

        var json = JsonConvert.SerializeObject(authData);
        
        var response = await _httpClient.PostAsync("authentication/login", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<TokenModel>(jsonResult);
            return authResult?.Token;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<string?> RegistrationUserAsync(RegistrationModel registrationModel)
    {
        var json = JsonConvert.SerializeObject(registrationModel);
        
        var response = await _httpClient.PostAsync("authentication", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<TokenModel>(jsonResult);
            return authResult?.Token;
        }
        else
        {
            return null;
        }
    }

    public async Task<ObservableCollection<CapitanModel>?> GetCapitansAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync("capitans");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<CapitanModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<ObservableCollection<CompanyModel>?> GetCompaniesAsync()
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync("companies");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<CompanyModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }

    public async Task<ObservableCollection<BoatModel>?> GetBoatsByCapitanAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync($"capitans/{id}/boats");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<BoatModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<ObservableCollection<EmployeeModel>?> GetEmployeesByCompanyAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        HttpResponseMessage response = await _httpClient.GetAsync($"companies/{id}/employees");

        if (response.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            var items = JsonConvert.DeserializeObject<ObservableCollection<EmployeeModel>>(json);
            return items;
        }
        else
        {
            return null;
        }
    }
    
    public async Task<CapitanModel?> PostCreateCapitanAsync(CapitanCreationModel capitanCreationModel)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        var json = JsonConvert.SerializeObject(capitanCreationModel);
        
        HttpResponseMessage response = await _httpClient.PostAsync($"capitans", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<CapitanModel>(jsonResult);
            return authResult;
        }

        return null;
    }

    public async Task<BoatModel?> PostBoatForCapitanAsync(BoatCreationModel boatCreationModel, Guid capitanId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        var json = JsonConvert.SerializeObject(boatCreationModel);

        HttpResponseMessage response = await _httpClient.PostAsync($"capitans/{capitanId}/boats", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<BoatModel>(jsonResult);
            return authResult;
        }

        return null;
    }

    public async Task DeleteCapitandAsync(Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        await _httpClient.DeleteAsync($"capitans/{id}");
    }
    
    public async Task<CapitanModel?> PutUpdateCapitandAsync(CapitanCreationModel boatBrandCreationModel, Guid id)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);
        
        var json = JsonConvert.SerializeObject(boatBrandCreationModel);
        
        HttpResponseMessage response = await _httpClient.PutAsync($"capitans/{id}", new StringContent(json, System.Text.Encoding.UTF8, "application/json"));

        if (response.IsSuccessStatusCode)
        {
            var jsonResult = await response.Content.ReadAsStringAsync();
            var authResult = JsonConvert.DeserializeObject<CapitanModel>(jsonResult);
            return authResult;
        }

        return null;
    }
    
    public async Task DeleteBoatAsync(Guid id, Guid capitanId)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", TokenManager.Token);

        await _httpClient.DeleteAsync($"capitans/{capitanId}/boats/{id}");
    }
}