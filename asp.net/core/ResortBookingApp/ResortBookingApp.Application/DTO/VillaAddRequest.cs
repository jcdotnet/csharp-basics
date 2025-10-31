﻿using ResortBookingApp.Domain.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Reflection;

namespace ResortBookingApp.Application.DTO
{
    public class VillaAddRequest
    {
        [MaxLength(50)]
        public required string Name { get; set; }

        public string? Description { get; set; }

        [Range(1000, 1000000)]
        public double Price { get; set; }

        [Display(Name = "Square Meters")]
        public int SquareMeters { get; set; }

        [Range(1, 10)]
        public int Occupancy { get; set; }

        [Display(Name = "Image Url")]
        public string? ImageUrl { get; set; }

        public Villa ToVilla()
        {
            return new Villa()
            {
                Name = Name,
                Description = Description,
                Price = Price,
                SquareMeters = SquareMeters,
                Occupancy = Occupancy,
                ImageUrl = ImageUrl
            };
        }
    }
}
