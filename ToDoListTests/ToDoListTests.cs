using Shouldly;
using ToDoListDemo;
using ToDoListTests.Builders;
using Xunit;

namespace ToDoListTests
{
    public class ToDoListTests
    {
        private readonly ToDoList _sut = new();
        private readonly ToDoItem _itemInToDoList  = new ToDoItemBuilder()
            .WithTitle("Original title")
            .Build();

        public ToDoListTests()
        {
            _sut.AddItem(_itemInToDoList);
        }
        
        [Fact]
        public void NewItemShouldBeAddedToToDoList()
        {
            var item = new ToDoItemBuilder().Build();
            
            // Act
            _sut.AddItem(item);

            // Assert
            _sut.GetAllItems().Count.ShouldBe(2);
        }

        [Fact]
        public void ItemShouldBeUpdatedOnToDoList()
        {
            const string newTitle = "new Title";
            _itemInToDoList.Title = newTitle;
            
            _sut.UpdateItem(_itemInToDoList);
            
            _sut.GetItemById(_itemInToDoList.Id)!.Title.ShouldBe(newTitle);
        }

        [Fact]
        public void ItemShouldBeRemovedFromList()
        {
            _sut.DeleteItem(_itemInToDoList.Id);
            
            _sut.GetAllItems().Count.ShouldBe(0);
        }
    }
}
