using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Filmuthyrning.Model.BLL
{
    public class Movie
    {
        public int FilmID{get;set;}
        public string Titel{get;set;}
        public int År { get; set; }
        public string Genre { get; set; }
        public int PrisgruppID { get; set; }
        public int Hyrtid { get; set; }
        public int Antal { get; set; }
    }
}