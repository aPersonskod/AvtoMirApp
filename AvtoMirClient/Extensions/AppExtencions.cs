using System;
using System.Windows;

namespace AvtoMirClient.Extensions;

public static class AppExtensions
{
    public static void Show(this string message, string header = "Сообщение")
    {
        MessageBox.Show(message, header);
    }
    
    public static T Fluent<T>(this T target, Action<T> stuff)
    {
        stuff(target);
        return target;
    }
}