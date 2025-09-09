using System.Runtime.InteropServices.Marshalling;
namespace Nexai.Kaonashi.Gui
{
    using Nexai.Kaonashi.Core.Models.Corpus;
    public static class DataService
    {

        private static List<Entity> _entities = new List<Entity>();

        public static List<Entity> EntitiesSearch(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return _entities;
            }

            return _entities.Where(e =>
                e.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                e.ShortDescription.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                e.Type.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static void EntityAdd(Entity entity)
        {
            _entities.Add(entity);
        }

        public static void EntityUpdate(Entity entity)
        {
            // For simplicity, we'll just re-add and remove to simulate an update.
            // In a real app, you'd find and modify the existing object.
            var existing = _entities.FirstOrDefault(e => e.Name == entity.Name); // Assuming Name is unique
            if (existing != null)
            {
                _entities.Remove(existing);
                _entities.Add(entity);
            }
        }

        public static void EntityDelete(Entity entity)
        {
            _entities.Remove(entity);
        }

        // A method to populate some sample data
        public static void PopulateSampleData()
        {
            if (_entities.Count == 0)
            {
                _entities.Add(new Entity
                {
                    Name = "The Eiffel Tower",
                    Type = "LOCATION",
                    ShortDescription = "A famous landmark in Paris.",
                    Joy = 8,
                    RelatedEntities = new List<string> { "Paris", "France" }
                });
                _entities.Add(new Entity
                {
                    Name = "Chez Raoult Bittenbois",
                    Type = "LOCATION",
                    ShortDescription = "A famous bistrot in Paris.",
                    Joy = 8,
                    RelatedEntities = new List<string> { "Paris", "France" }
                });
                _entities.Add(new Entity
                {
                    Name = "Albert Einstein",
                    Type = "PERSON",
                    ShortDescription = "Physicist who developed the theory of relativity.",
                    Sadness = 2,
                    RelatedEntities = new List<string> { "physics", "relativity" }
                });
                _entities.Add(new Entity
                {
                    Name = "World War II",
                    Type = "EVENT",
                    ShortDescription = "A global conflict from 1939 to 1945.",
                    Anger = 10,
                    RelatedEntities = new List<string> { "Germany", "Japan", "United States" }
                });
            }
        }
    }
}