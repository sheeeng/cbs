/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2017 International Federation of Red Cross. All rights reserved.
 *  Licensed under the MIT License. See LICENSE in the project root for license information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using doLittle.Read;
using Domain.StaffUser.Registering;
using Domain.StaffUser.Roles;
using Microsoft.AspNetCore.Mvc;
using Infrastructure.AspNet;
using MongoDB.Driver;
using Read.DataCollectors;
using Read.StaffUsers;
using Read.StaffUsers.Models;
using Admin = Read.StaffUsers.Models.Admin;
using DataConsumer = Read.StaffUsers.Models.DataConsumer;
using DataCoordinator = Read.StaffUsers.Models.DataCoordinator;
using DataOwner = Read.StaffUsers.Models.DataOwner;
using DataVerifier = Read.StaffUsers.Models.DataVerifier;
using SystemConfigurator = Read.StaffUsers.Models.SystemConfigurator;

namespace Web.Controllers
{
    [Route("api/staffusers")]
    public class StaffUserController : BaseController
    {
        private readonly IMongoDatabase _database;

        private readonly IRegisteringCommandHandlers _staffUserCommandHandler;

        private readonly IQueryCoordinator _queryCoordinator;

        public StaffUserController (
            IMongoDatabase database,
            IQueryCoordinator queryCoordinator,
            IRegisteringCommandHandlers stafffUserCommandHandler
            )
        {
            _database = database;
            _queryCoordinator = queryCoordinator;
            _staffUserCommandHandler = stafffUserCommandHandler;
        }

        #region Get All Users

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            return GetAllStaffUsers<BaseUser>();
        }

        [HttpGet("admin")]
        public IActionResult GetAllAdmins()
        {
            return GetAllStaffUsers<Admin>();
        }

        [HttpGet("dataconsumer")]
        public IActionResult GetAllDataConsumers()
        {
            return GetAllStaffUsers<DataConsumer>();
        }

        [HttpGet("datacoordinator")]
        public IActionResult GetAllDataCoordinator()
        {
            return GetAllStaffUsers<DataCoordinator>();
        }

        [HttpGet("dataowner")]
        public IActionResult GetAllDataOwners()
        {
            return GetAllStaffUsers<DataOwner>();
        }

        [HttpGet("dataverifier")]
        public IActionResult GetAllDataVerifiers()
        {
            return GetAllStaffUsers<DataVerifier>();
        }

        [HttpGet("systemconfigurator")]
        public IActionResult GetAllSystemConfigurators()
        {
            return GetAllStaffUsers<SystemConfigurator>();
        }

        #endregion

        #region Get By Id

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            return GetStaffUserById<BaseUser>(id);
        }

        [HttpGet("admin/{id}")]
        public IActionResult GetAdminById(Guid id)
        {
            return GetStaffUserById<Admin>(id);
        }

        [HttpGet("dataconsumer/{id}")]
        public IActionResult GetDataConsumerById(Guid id)
        {
            return GetStaffUserById<DataConsumer>(id);
        }

        [HttpGet("datacoordinator/{id}")]
        public IActionResult GetDataCoordinatorById(Guid id)
        {
            return GetStaffUserById<DataCoordinator>(id);
        }

        [HttpGet("dataowner/{id}")]
        public IActionResult GetDataOwnerById(Guid id)
        {
            return GetStaffUserById<DataOwner>(id);
        }

        [HttpGet("dataverifier/{id}")]
        public IActionResult GetDataVerifierById(Guid id)
        {
            return GetStaffUserById<DataVerifier>(id);
        }

        [HttpGet("systemconfigurator/{id}")]
        public IActionResult GetSystemConfiguratorById(Guid id)
        {
            return GetStaffUserById<SystemConfigurator>(id);
        }

        #endregion

        #region Register

        //TODO: Woksin (10/04/18): Instead of relying on the commands being sent is as parameters, maybe
        // we should retrieve a StaffUserRole instead
        
        //TODO: Everything here is functional, but in my opinion it gives too much responsibility to the frontend.

        [HttpPost("admin")]
        public IActionResult RegisterAdmin([FromBody] Read.StaffUsers.Models.Admin admin)
        {
            var command = new RegisterNewAdminUser
            {
                IsNewRegistration = true,
                RegisteredAt = DateTimeOffset.UtcNow,
                Role =
                {
                    StaffUserId = Guid.NewGuid(),
                    FullName = admin.FullName,
                    DisplayName = admin.DisplayName,
                    Email = admin.Email
                },
            };

            RegisterStaffUser<RegisterNewAdminUser, Domain.StaffUser.Roles.Admin>(command);

            return Ok();
        }

        [HttpPost("systemconfigurator")]
        public IActionResult RegisterSystemConfigurator([FromBody] Read.StaffUsers.Models.SystemConfigurator systemConfigurator)
        {
            var command = new RegisterNewSystemConfigurator()
            {
                IsNewRegistration = true,
                RegisteredAt = DateTimeOffset.UtcNow,
                Role =
                {
                    StaffUserId = Guid.NewGuid(),
                    FullName = systemConfigurator.FullName,
                    DisplayName = systemConfigurator.DisplayName,
                    Email = systemConfigurator.Email,
                    PhoneNumbers = systemConfigurator.PhoneNumbers.Select(p => p.Value),
                    PreferredLanguage = systemConfigurator.PreferredLanguage,
                    NationalSociety = systemConfigurator.NationalSociety,
                    Sex = systemConfigurator.Sex,
                    AssignedNationalSocieties = systemConfigurator.AssignedNationalSocieties,
                    BirthYear = systemConfigurator.BirthYear
                },
            };
            RegisterStaffUser<RegisterNewSystemConfigurator, Domain.StaffUser.Roles.SystemConfigurator>(command);

            return Ok();
        }

        [HttpPost("datacordinator")]
        public IActionResult RegisterDatacordinator([FromBody] Read.StaffUsers.Models.DataCoordinator dataCoordinator)
        {
            var command = new RegisterNewDataCoordinator()
            {
                IsNewRegistration = true,
                RegisteredAt = DateTimeOffset.UtcNow,
                Role =
                {
                    StaffUserId = Guid.NewGuid(),
                    FullName = dataCoordinator.FullName,
                    DisplayName = dataCoordinator.DisplayName,
                    Email = dataCoordinator.Email,
                    PhoneNumbers = dataCoordinator.PhoneNumbers.Select(p => p.Value),
                    PreferredLanguage = dataCoordinator.PreferredLanguage,
                    NationalSociety = dataCoordinator.NationalSociety,
                    Sex = dataCoordinator.Sex,
                    AssignedNationalSocieties = dataCoordinator.AssignedNationalSocieties,
                    BirthYear = dataCoordinator.BirthYear
                }
            };
            RegisterStaffUser<RegisterNewDataCoordinator, Domain.StaffUser.Roles.DataCoordinator>(command);

            return Ok();
        }

        [HttpPost("dataowner")]
        public IActionResult RegisterDataOwner([FromBody] Read.StaffUsers.Models.DataOwner dataOwner)
        {
            var command = new RegisterNewDataOwner()
            {
                IsNewRegistration = true,
                RegisteredAt = DateTimeOffset.UtcNow,
                Role =
                {
                    StaffUserId = Guid.NewGuid(),
                    FullName = dataOwner.FullName,
                    DisplayName = dataOwner.DisplayName,
                    Email = dataOwner.Email,
                    PhoneNumbers = dataOwner.PhoneNumbers.Select(p => p.Value),
                    PreferredLanguage = dataOwner.PreferredLanguage,
                    NationalSociety = dataOwner.NationalSociety,
                    Sex = dataOwner.Sex,
                    BirthYear = dataOwner.BirthYear,
                    DutyStation = dataOwner.DutyStation,
                    Position = dataOwner.Position
                }
            };

            RegisterStaffUser<RegisterNewDataOwner, Domain.StaffUser.Roles.DataOwner>(command);

            return Ok();
        }

        [HttpPost("staffdataconsumer")]
        public IActionResult RegisterDataConsumer([FromBody] RegisterNewStaffDataConsumer command)
        {
            command.Role.StaffUserId = Guid.NewGuid();
            command.IsNewRegistration = true;
            RegisterStaffUser<RegisterNewStaffDataConsumer, Domain.StaffUser.Roles.DataConsumer>(command);

            return Ok();
        }

        [HttpPost("staffdataverifier")]
        public IActionResult RegisterDataVerifier([FromBody] RegisterNewStaffDataVerifier command)
        {
            command.Role.StaffUserId = Guid.NewGuid();
            
            RegisterStaffUser<RegisterNewStaffDataVerifier, Domain.StaffUser.Roles.DataVerifier>(command);

            return Ok();
        }

        #endregion

        #region Update Methods

        [HttpPut("admin")]
        public IActionResult UpdateAdmin([FromBody] RegisterNewAdminUser command)
        {
            command.IsNewRegistration = false;
            _staffUserCommandHandler.Handle(command);

            return Ok();
        }

        [HttpPut("systemconfigurator")]
        public IActionResult UpdateSystemConfigurator([FromBody] RegisterNewSystemConfigurator command)
        {
            command.IsNewRegistration = false;
            _staffUserCommandHandler.Handle(command);

            return Ok();
        }

        [HttpPut("datacordinator")]
        public IActionResult UpdaterDataCordinator([FromBody] RegisterNewDataCoordinator command)
        {
            command.IsNewRegistration = false;
            _staffUserCommandHandler.Handle(command);

            return Ok();
        }

        [HttpPut("dataowner")]
        public IActionResult UpdateDataOwner([FromBody] RegisterNewDataOwner command)
        {
            command.IsNewRegistration = false;
            _staffUserCommandHandler.Handle(command);

            return Ok();
        }

        [HttpPut("staffdataconsumer")]
        public IActionResult UpdateDataConsumer([FromBody] RegisterNewStaffDataConsumer command)
        {
            command.IsNewRegistration = false;
            _staffUserCommandHandler.Handle(command);

            return Ok();
        }

        [HttpPut("staffdataverifier")]
        public IActionResult UpdateDataVerifier([FromBody] RegisterNewStaffDataVerifier command)
        {
            command.IsNewRegistration = false;
            _staffUserCommandHandler.Handle(command);

            return Ok();
        }

        #endregion

        #region Private Methods

        private IActionResult GetAllStaffUsers<T>()
            where T : BaseUser
        {
            var result = _queryCoordinator.Execute(new AllStaffUsers<T>(_database), new PagingInfo());

            if (result.Success)
            {
                return Ok(result.Items);
            }

            return new NotFoundResult();
        }

        private IActionResult GetStaffUserById<T>(Guid id)
            where T : BaseUser
        {
            var result = _queryCoordinator.Execute(new StaffUserById<T>(_database, id), new PagingInfo());

            if (result.Success)
            {
                return Ok(result.Items);
            }

            return new NotFoundResult();
        }

        private void RegisterStaffUser<TRegistration, TRole>(TRegistration command)
            where TRegistration : NewStaffRegistration<TRole>
            where TRole : StaffRole
        {
            command.Role.StaffUserId = Guid.NewGuid();
            command.IsNewRegistration = true;
            //TODO: Hmm, a working workaround this is... Though, I think it would be better to do this in AggregateRoot 
            command.RegisteredAt = DateTimeOffset.UtcNow;
            _staffUserCommandHandler.Handle(command as dynamic);
        }

        private void UpdateStaffUser<TRegistration, TRole>(TRegistration command)
            where TRegistration : NewStaffRegistration<TRole>
            where TRole : StaffRole
        {
            command.IsNewRegistration = false;
            _staffUserCommandHandler.Handle(command as dynamic);
        }

        #endregion
    }
}