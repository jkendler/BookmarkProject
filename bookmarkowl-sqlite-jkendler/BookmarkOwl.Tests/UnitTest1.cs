using BookmarkOwl.Core.Interfaces;
using BookmarkOwl.Core.Models;
using BookmarkOwl.Core.Services;
using BookmarkOwl.Core.ViewModels;
using Moq;

namespace BookmarkOwl.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            Assert.Pass();
        }

        [Test]
        public async Task Test_Add_Entry_To_SqliteRepositoryAsync()
        {
            // Arrange
            string path = Path.GetTempPath();
            IRepository repository = new SqliteRepository(path);
            LinkEntry entry = new LinkEntry("HAK", "https://www.hakzell.at");

            // Act
            var res = await repository.Add(entry);

            // Assert
            Assert.IsTrue(res);
        }


        [Test]
        public async Task Test_Add_Entry_VM()
        {
            // Arrange
            string path = Path.GetTempPath();
            string dbPath = Path.Combine(path, "bookmarkowl.db3");

            if (File.Exists(dbPath))
            {
                File.Delete(dbPath);
            }

            IRepository repository = new SqliteRepository(path);
            LinkEntry entry = new LinkEntry("HAK", "https://www.hakzell.at");

            var mockAlert = new Mock<IAlertService>();
            var mockBrowser = new Mock<IBrowserService>();

            var viewModel = new LinkListViewModel(repository, mockAlert.Object, mockBrowser.Object);

            // Act
            viewModel.LinkTitle = "Google";
            viewModel.LinkDestination = "https://www.google.at";

            viewModel.CreateCommand.Execute(null);
            viewModel.LoadDataCommand.Execute(null);
            

            var entries = viewModel.Entries;


            Assert.Multiple(() =>
            {
                Assert.That(entries.Count, Is.EqualTo(1));
                Assert.That(entries.FirstOrDefault()?.Title, Is.EqualTo("Google"));
            });


        }
    }
}