﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Web;

namespace CompositeDesignPattern.Composite
{
    public class BookComposite : IComponent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<IComponent> Components { get; private set; } = new();

        public BookComposite(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void Add(IComponent component)
        {
            Components.Add(component);
        }

        public void Remove(IComponent component)
        {
            Components.Remove(component);
        }

        public int Count() => Components.Sum(x => x.Count());

        public string Display()
        {
            var stringBuilder = new StringBuilder();

            stringBuilder.Append($"<div class='text-primary my-1'><a href='#' class='menu'>{Name} ({Count()})</a></div>");

            if (!Components.Any()) return stringBuilder.ToString();

            stringBuilder.Append("<ul class='list-group list-group-flush ms-3'>");

            foreach (var component in Components)
            {
                stringBuilder.Append(component.Display());
            }

            stringBuilder.Append("</ul>");

            return stringBuilder.ToString();
        }

        public List<SelectListItem> GetSelectList(string space = "")
        {
            space = HttpUtility.HtmlDecode(space);

            var list = new List<SelectListItem>() { new() { Text = $"{space}{Name}", Value = Id.ToString() } };

            if (Components.Any(x => x is BookComposite))
            {
                space += "&nbsp;&nbsp;&nbsp;&nbsp;";
                Components.ToList().ForEach(x =>
                {
                    if (x is BookComposite bookComposite)
                    {
                        list.AddRange(bookComposite.GetSelectList(space));
                    }
                });
            }

            return list;
        }

    }
}
