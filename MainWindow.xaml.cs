using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CacheLibrary;

namespace DemoCacheLib
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly Cache<string, object> _cacheLRU;
        private readonly Cache<string, object> _cacheMRU;
        public MainWindow()
        {
            InitializeComponent();
            // Создаем кеш с вместимостью 5 и алгоритмом замены LRU
            _cacheLRU = new Cache<string, object>(5, new LruReplacementAlgorithm<string>());
            // Создаем кеш с вместимостью 5 и алгоритмом замены MRU
            _cacheMRU = new Cache<string, object>(5, new MruReplacementAlgorithm<string>());

            // Добавляем некоторые значения в кеш для демонстрации
            _cacheLRU.Add("ключ1", "значение1LRU");
            _cacheLRU.Add("ключ2", "значение2LRU");
            _cacheLRU.Add("ключ3", "значение3LRU");

            _cacheMRU.Add("ключ1", "значение1MRU");
            _cacheMRU.Add("ключ2", "значение2MRU");
            _cacheMRU.Add("ключ3", "значение3MRU");
        }

        

        private void LRU(object sender, RoutedEventArgs e)
        {
            try
            {
                var key = tbkeys.Text;

                // Получаем значение из кеша
                var value = _cacheLRU.Get(key);

                // Отображаем значение в listbox
                keyresults.Items.Add(value?.ToString() ?? "Значение не найдено(LRU)");
            }
            catch
            {
                MessageBox.Show("Ошибка,проверьте ключ");
            }
        }

        private void MRU(object sender, RoutedEventArgs e)
        {
            try
            {
                var key = tbkeys.Text;

                // Получаем значение из кеша
                var value = _cacheMRU.Get(key);

                // Отображаем значение в listbox
                keyresults.Items.Add(value?.ToString() ?? "Значение не найдено(MRU)");
            }
            catch
            {
                MessageBox.Show("Ошибка,проверьте ключ");
            }
        }

        private void addMRU(object sender, RoutedEventArgs e)
        {
            // Генерируем новый ключ и значение
            var key = "ключ" + (_cacheMRU.Count + 1).ToString();
            var value = "значение" + (_cacheMRU.Count + 1).ToString() + "MRU";

            // Добавляем новое значение в кеш
            _cacheMRU.Add(key, value);

            // Отображаем новый ключ и значение в listbox 
            CachelistMRU.Items.Add($"Добавлено новое значение: {key} = {value}");

            CachelistMRU.Items.Clear();

            foreach (var cacheKey in _cacheMRU.GetKeys())
            {
                CachelistMRU.Items.Add(cacheKey);
            }
        }

        private void addLRU(object sender, RoutedEventArgs e)
        {
            // Генерируем новый ключ и значение
            var key = "ключ" + (_cacheLRU.Count + 1).ToString();
            var value = "значение" + (_cacheLRU.Count + 1).ToString() + "MRU";

            // Добавляем новое значение в кеш
            _cacheLRU.Add(key, value);

            // Отображаем новый ключ и значение в listbox
            CachelistLRU.Items.Add($"Добавлено новое значение: {key} = {value}");

            CachelistLRU.Items.Clear();

            foreach (var cacheKey in _cacheLRU.GetKeys())
            {
                CachelistLRU.Items.Add(cacheKey);
            }
        }
    }
}
