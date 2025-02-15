using ToDoListDemo;

namespace ToDoListTests.Builders;

public class ToDoItemBuilder
{
    private ToDoItem _item;

    public ToDoItemBuilder()
    {
        _item = new ToDoItem
        {
            Id = 1, 
            Title = "Test Item", 
            Description = "This is a test item", 
            IsCompleted = false
        };
    }

    public ToDoItemBuilder WithId(int id)
    {
        _item.Id = id;
        return this;
    }

    public ToDoItemBuilder WithTitle(string title)
    {
        _item.Title = title;
        return this;
    }

    public ToDoItemBuilder WithDescriptio(string description)
    {
        _item.Description = description;
        return this;
    }
    
    public ToDoItemBuilder IsCompleted(bool isCompleted)
    {
        _item.IsCompleted = isCompleted;
        return this;
    }

    public ToDoItem Build() => _item;
}