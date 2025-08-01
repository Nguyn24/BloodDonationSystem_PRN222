﻿using BusinessObject.Entities;

namespace BloodDonationSystem.BLL.Services.BloodTypeRepoService;

public interface IBloodTypeService
{
    Task<List<BloodType>> GetBloodTypeAsync();
    Task<BloodType?> GetBloodTypeByIDAsync(Guid id);
}