﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MedPark.API.Gateway.Models.MedicalPractice
{
    public class OperatingHoursDetailDTO
    {
        public TimeSpan AppointmentDuration { get; set; }
        public OperatingHoursDTO PracticeOperatingHours { get; set; }
        public List<DateTime> AppointmentTimes { get; set; }
    }
}
