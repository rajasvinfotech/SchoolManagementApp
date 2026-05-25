using System.Text.Json;

namespace SchoolManagementApp.Services
{
    public class LocalStorageService<T> : IDataService<T> where T : class
    {
        private readonly string _fileName;
        private readonly string _folderPath;
        private readonly string _filePath;

        public LocalStorageService(string fileName)
        {
            _fileName = fileName;
            _folderPath = Path.Combine(FileSystem.AppDataDirectory, "SchoolData");
            _filePath = Path.Combine(_folderPath, _fileName);

            // Ensure directory exists
            if (!Directory.Exists(_folderPath))
            {
                Directory.CreateDirectory(_folderPath);
            }
        }

        public async Task<List<T>> GetAllAsync()
        {
            try
            {
                if (!File.Exists(_filePath))
                {
                    return new List<T>();
                }

                var json = await File.ReadAllTextAsync(_filePath);
                var data = JsonSerializer.Deserialize<List<T>>(json);
                return data ?? new List<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading data: {ex.Message}");
                return new List<T>();
            }
        }

        public async Task<T?> GetByIdAsync(string id)
        {
            var items = await GetAllAsync();
            var idProperty = typeof(T).GetProperty("Id");

            if (idProperty == null)
                return null;

            return items.FirstOrDefault(item =>
                idProperty.GetValue(item)?.ToString() == id);
        }

        public async Task<bool> AddAsync(T item)
        {
            try
            {
                var items = await GetAllAsync();
                items.Add(item);
                await SaveAllAsync(items);
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding data: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateAsync(T item)
        {
            try
            {
                var items = await GetAllAsync();
                var idProperty = typeof(T).GetProperty("Id");

                if (idProperty == null)
                    return false;

                var itemId = idProperty.GetValue(item)?.ToString();
                var index = items.FindIndex(i =>
                    idProperty.GetValue(i)?.ToString() == itemId);

                if (index >= 0)
                {
                    items[index] = item;
                    await SaveAllAsync(items);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating data: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteAsync(string id)
        {
            try
            {
                var items = await GetAllAsync();
                var idProperty = typeof(T).GetProperty("Id");

                if (idProperty == null)
                    return false;

                var itemToRemove = items.FirstOrDefault(i =>
                    idProperty.GetValue(i)?.ToString() == id);

                if (itemToRemove != null)
                {
                    items.Remove(itemToRemove);
                    await SaveAllAsync(items);
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error deleting data: {ex.Message}");
                return false;
            }
        }

        private async Task SaveAllAsync(List<T> items)
        {
            var json = JsonSerializer.Serialize(items, new JsonSerializerOptions
            {
                WriteIndented = true
            });
            await File.WriteAllTextAsync(_filePath, json);
        }

        public string GetDataPath()
        {
            return _filePath;
        }
    }
}