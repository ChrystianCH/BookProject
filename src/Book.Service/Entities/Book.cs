using System;

namespace Book.Service.Entities {

    public class Book {

        public int id { get; set; }

        public string? BookName { get; set; }

        public string? Author { get; set; }

        public double Price { get; set; }

        public DateTimeOffset ReleaseDate { get; set; }
    }
}