﻿namespace LAB13_TINOCO_DAEA.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public bool Active { get; set; } = true;
    }
}
