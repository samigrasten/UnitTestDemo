using System.Collections.Generic;
using System.Linq;

namespace ToDoListDemo;

public class ToDoList
{
    private readonly List<ToDoItem> _items = [];

    public List<ToDoItem> GetAllItems()
    {
        return _items;
    }

    public ToDoItem? GetItemById(int id)
    {
        return _items.FirstOrDefault(item => item.Id == id);
    }

    public void AddItem(ToDoItem item)
    {
        _items.Add(item);
    }

    public bool UpdateItem(ToDoItem item)
    {
        var existingItem = _items.FirstOrDefault(i => i.Id == item.Id);
        if (existingItem == null) return false;

        existingItem.Title = item.Title;
        existingItem.Description = item.Description;
        existingItem.IsCompleted = item.IsCompleted;
        return true;
    }

    public bool DeleteItem(int id)
    {
        var item = _items.FirstOrDefault(i => i.Id == id);
        if (item == null) return false;

        _items.Remove(item);
        return true;
    }
}