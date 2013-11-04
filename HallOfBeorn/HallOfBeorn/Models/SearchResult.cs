﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

using HallOfBeorn;

namespace HallOfBeorn.Models
{
    public class SearchResult
    {
        [Display(Name="Search")]
        public string Query { get; set; }
        
        [Display(Name="Advanced Search")]
        public bool IsAdvancedSearch { get; set; }
        
        [Display(Name="Card Type")]
        public CardType CardType { get; set; }

        [Display(Name="Results")]
        public List<Card> Cards { get; set; }

        public static IEnumerable<SelectListItem> CardTypes
        {
            get { return typeof(CardType).GetSelectListItems(); }
        }
    }
}