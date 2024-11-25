using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows;
using AvtoMirModel;

namespace AvtoMirClient.Extensions;

public static class AppExtensions
{
    public static void Show(this string message, string header = "Сообщение")
    {
        MessageBox.Show(message, header);
    }
    
    public static async Task<ObservableCollection<T>> GetQuery<T>(this string query)
    {
        using var client = new HttpClient();
        var response = await client.GetAsync(query);
        response.EnsureSuccessStatusCode();
        if (response.IsSuccessStatusCode)
        {
            var result = await response.Content.ReadFromJsonAsync<List<T>>();
            return result != null ? new ObservableCollection<T>(result) : new ObservableCollection<T>();
        }
        $"server error code {response.StatusCode}".Show("Error");
        return new ObservableCollection<T>();
    }

    public static async Task PostQuery<T>(this string query, T entity)
    {
        using var client = new HttpClient();
        var content = JsonContent.Create(entity);
        var response = await client.PostAsync(query, content);
        response.EnsureSuccessStatusCode();
        if (!response.IsSuccessStatusCode)
        {
            $"server error code {response.StatusCode}".Show("Error");
        }
    }
    
    public static async Task DeleteQuery(this string query)
    {
        try
        {
            using var client = new HttpClient();
            var response = await client.DeleteAsync(query);
            response.EnsureSuccessStatusCode();
            if (!response.IsSuccessStatusCode)
            {
                $"server error code {response.StatusCode}".Show("Error");
            }
        }
        catch (Exception e)
        {
            "Удаление невозможно".Show("Ошибка");
        }
    }
    
    public static async Task PatchQuery<T>(this string query, T entity)
    {
        using var client = new HttpClient();
        var content = JsonContent.Create(entity);
        var response = await client.PatchAsync(query, content);
        response.EnsureSuccessStatusCode();
        if (!response.IsSuccessStatusCode)
        {
            $"server error code {response.StatusCode}".Show("Error");
        }
    }

    public static bool SureDo(string message)
    {
        var result = MessageBox.Show(message, "Проверка", MessageBoxButton.YesNo);
        return result == MessageBoxResult.Yes;
    }

    public static T Fluent<T>(this T target, Action<T> stuff)
    {
        stuff(target);
        return target;
    }
}