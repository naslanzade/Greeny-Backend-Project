﻿using Greeny.Models;

namespace Greeny.ViewModels
{
    public class BrandVM
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public string Image { get; set; }

        public BgImage BgImage { get; set; }

        public List<Brand> Brands { get; set; }
    }
}
