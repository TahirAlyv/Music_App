﻿using System.ComponentModel.DataAnnotations;

namespace MusicService.Models
{
    public class Music
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        public TimeSpan? Duration { get; set; }
        public string? Genre { get; set; }
        public string? Album { get; set; }
        public string? Artist { get; set; }
        public string? CoverImagePath { get; set; }
        public string FilePath { get; set; }

        public string UploadedByUserId { get; set; }

        public DateTime UploadedAt { get; set; } = DateTime.UtcNow;
    }
}
