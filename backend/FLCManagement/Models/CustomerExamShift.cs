﻿using System.ComponentModel.DataAnnotations;

namespace FLCManagement.Models
{
    public class CustomerExamShift
    {
        [Key] public int customeresID { get; set; }
        public float customeresScore { get; set; }

        public Customer Customer { get; set; }
        public ExamShift ExamShift { get; set; }
    }
}
