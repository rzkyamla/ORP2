﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ORP_API.ViewModels
{
    public class OvertimeFormViewModels
    {
        [Required(ErrorMessage = "Data tidak boleh kosong"), MaxLength(50, ErrorMessage = "Maksimal 50 Karakter")]
        public string Name { get; set; }
        public string CustomerId { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime StartTime { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{HH:mm:ss}")]
        public DateTime EndTime { get; set; }
        [Required(ErrorMessage = "Data tidak boleh kosong"), MaxLength(100, ErrorMessage = "Maksimal 100 Karakter")]
        public string Activity { get; set; }

    }

}
