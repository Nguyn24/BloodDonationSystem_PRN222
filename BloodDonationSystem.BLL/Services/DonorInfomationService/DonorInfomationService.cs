﻿using BloodDonationSystem.DAL.Repositories.DonorInformationRepo;
using BloodDonationSystem.DAL.Repositories.Requests;
using BloodDonationSystem.DAL.Repositories.Responses;
using DonorInformation = BusinessObject.Entities.DonorInformation;

namespace BloodDonationSystem.BLL.Services.DonorInfomationService;

public class DonorInfomationService : IDonorInfomationService
{
    private readonly IDonorInformationRepo _donorInformationRepo;

    public async Task<List<DonorInformation>> GetDonorInfoAsync()
    {
        return await _donorInformationRepo.GetDonorInfoAsync();
    }

    public async Task UpdateDonorAsync(UpdateDonorRequest request)
    {
        await _donorInformationRepo.UpdateDonorAsync(request);
    }

    public async Task CreateDonorAsync(CreateDonorRequest request)
    {
        await _donorInformationRepo.CreateDonorAsync(request);
    }

    public async Task DeleteDonorAsync(DonorInformation requestId)
    {
        await _donorInformationRepo.DeleteDonorAsync(requestId);
    }
}