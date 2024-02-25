using System.Text;

namespace CompositeDesignPattern.Composite
{
    public class BookComposite : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }

        private List<IComponent> components;

        public BookComposite(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Add(IComponent component)
        {
            components.Add(component);
        }

        public void Remove(IComponent component)
        {
            components.Remove(component);
        }

        public int Count() => components.Sum(x => x.Count());

        public string Display()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"<div class='text-primary my-1'><a href='#' class='menu'>{Name} ({Count})</a></div>");

            if (!components.Any()) return stringBuilder.ToString();

            stringBuilder.Append("<ul class='list-group list-group-flush ms-3'>");

            foreach (var component in components)
            {
                stringBuilder.Append(component.Display());
            }

            stringBuilder.Append("</ul>");

            return stringBuilder.ToString();
        }
    }
}
