using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using ualax.shared.Common;

namespace ualax.test.Shared.Common
{
    public class CursorTest
    {
        [Fact]
        public void ShouldCreateBase64String()
        {
            // arrange
            var date = DateTime.UtcNow;
            var id = 1;

            // act
            var cursor = Cursor.ToCursor(date, id);

            // assert
            cursor.Should().NotBeNullOrWhiteSpace();
            Convert.FromBase64String(cursor).Should().NotBeNull();
        }

        [Fact]
        public void ShouldCreateValidCursor()
        {
            // arrange
            var cursor = new Cursor { Date = DateTime.UtcNow, Id = 256 };
            var base64 = Cursor.ToCursor(cursor.Date, cursor.Id);

            // act
            var result = Cursor.FromCursor(base64);

            // assert
            result.Should().BeEquivalentTo(cursor);
        }

        [Fact]
        public void ShouldReturnNull_WhenInvalidBase64String()
        {
            // arrange
            var base64 = "invalid_base64_string";

            // act
            var result = Cursor.FromCursor(base64);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnNull_WhenInvalidJson()
        {

            // arrange
            var base64 = Convert.ToBase64String(Encoding.UTF8.GetBytes("{invalid}"));

            // act
            var result = Cursor.FromCursor(base64);

            // assert
            result.Should().BeNull();
        }

        [Fact]
        public void ShouldReturnNull_WhenEmptyString()
        {
            // arrange
            var base64 = string.Empty;

            // act
            var result = Cursor.FromCursor(base64);

            // assert
            result.Should().BeNull();
        }
    }
}


