﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MedPark.API.Gateway.Services;
using MedPark.Common.RabbitMq;
using Microsoft.AspNetCore.Mvc;

namespace MedPark.API.Gateway.Controllers
{
    [Route("api/specialist")]
    [ApiController]
    public class MedicalPracticeController : ControllerBase
    {
        private readonly IBusPublisher _busPublisher;
        private readonly IMedicalPracticeService _specialistService;

        public MedicalPracticeController(IBusPublisher busPublisher, IMedicalPracticeService specialistService)
        {
            _busPublisher = busPublisher;
            _specialistService = specialistService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetSpecialistDetails(Guid id)
        {
            var specialist = await _specialistService.GetSpecialist(id);

            return Ok(specialist);
        }

        [HttpGet("getregistrationotp/{otp}")]
        public async Task<IActionResult> GetRegistrationOtp(Int32 otp)
        {
            var otpDetails = await _specialistService.GetRegistrationOTP(otp);

            return Ok(otpDetails);
        }

        [HttpGet("getpractice/{id}")]
        public async Task<IActionResult> GetPractice(Guid id)
        {
            var practice = await _specialistService.GetPracticeById(id);

            return Ok(practice);
        }

        [HttpGet("getpracticedetails/{id}")]
        public async Task<IActionResult> GetPracticeDetails(Guid id)
        {
            var practiceDetails = await _specialistService.GetPracticeDetailById(id);

            return Ok(practiceDetails);
        }

        [HttpGet("getacceptedschemes/{practiceId}")]
        public async Task<IActionResult> GetAcceptedSchemes(Guid practiceId)
        {
            var acceptedSchemesByPractice = await _specialistService.GetAcceptedSchemesByPracticeId(practiceId);

            return Ok(acceptedSchemesByPractice);
        }

        [HttpGet("getinstitute/{id}")]
        public async Task<IActionResult> GetInstitute(Guid id)
        {
            var institute = await _specialistService.GetInstituteById(id);

            return Ok(institute);
        }

        [HttpGet("getmedicalscheme/{schemeid}")]
        public async Task<IActionResult> GetMedicalScheme(Guid schemeid)
        {
            var scheme = await _specialistService.GetInstituteById(schemeid);

            return Ok(scheme);
        }

        [HttpGet("getpracticeoperatinghours/{practiceid}")]
        public async Task<IActionResult> GetOperatingHours(Guid practiceid)
        {
            var operatingHours = await _specialistService.GetOperatingHoursByPracticeId(practiceid);

            return Ok(operatingHours);
        }

        [HttpGet("specialistqualifications/{specialistid}")]
        public async Task<IActionResult> GetSpecialistQualifications(Guid specialistid)
        {
            var qualifications = await _specialistService.GetSpecialistQualifications(specialistid);

            return Ok(qualifications);
        }
    }
}