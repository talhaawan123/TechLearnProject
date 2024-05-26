using Xunit;
using Moq;
using FluentAssertions;
using Myshowroom.Business_logic.Concrete;
using Myshowroom.DataContext;
using Myshowroom.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks.Sources;

namespace TechLearn.Business_logic.Tests
{
    public class NotesRepositoryUnitTests
    {
        [Fact]
        public async Task CreateAsync_AddsNewNoteToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<dataContext>()
                .UseInMemoryDatabase(databaseName: "TestDatabase")
                .Options;

            using (var context = new dataContext(options))
            {
                var notesBusinessLogic = new NotesBusinessLogic(context);

                var note = new Notes
                {
                    Title = "Test Note",
                    Subject = "Test Subject",
                    Body = "Test Body"
                };

                // Act
                await notesBusinessLogic.CreateAsync(note);

                // Assert
                var result = context.LearningNotes.FirstOrDefault(n => n.Title == "Test Note");
                Xunit.Assert.NotNull(result);
                Xunit.Assert.Equal("Test Note", result.Title);
                Xunit.Assert.Equal("Test Subject", result.Subject);
                Xunit.Assert.Equal("Test Body", result.Body);
            }
        }
    }
}