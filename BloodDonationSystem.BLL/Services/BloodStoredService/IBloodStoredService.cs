﻿using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.BloodStoredService;

public interface IBloodStoredService
{
    public Task<List<BloodStored>> GetBloodStoredTypes();
    public Task<BloodStored> GetBloodStoredTypeById(Guid id);
    public Task UpdateBloodStoredType(UpdateBloodStoredRequest request);
}