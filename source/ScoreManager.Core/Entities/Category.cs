﻿namespace ScoreManager.Entities
{
    public class Category : Entity
    {
        public string? Name { get; set; }
        public int Order { get; set; }
        //public virtual IEnumerable<Perform>? Performs { get; set; }
    }
}