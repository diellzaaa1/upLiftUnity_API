﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace upLiftUnity_API.Models
{
    public class Rules
    {
        [Key]
        public int RuleId {  get; set; }

        public string RuleName { get; set; }

        public string RuleDesc {  get; set; }

        public int UserId {  get; set; }

        [ForeignKey("UserId")]
        public User User { get; set; }
    }   
}