﻿using BloodDonationSystem.DAL.DBContext;
using BloodDonationSystem.DAL.Repositories.DonationRequestRepo;
using BloodDonationSystem.DAL.Repositories.Requests;
using BusinessObject.DTO;
using BusinessObject.Entities;
using BusinessObject.Entities.Enum;

namespace BloodDonationSystem.BLL.Services.DonationRequestService;

public class DonationRequestService : IDonationRequestService
{
    private readonly IDonationRequestRepo _donationRequestRepo;

    public DonationRequestService(IDonationRequestRepo donationRequestRepo)
    {
        _donationRequestRepo = donationRequestRepo ?? throw new ArgumentNullException(nameof(donationRequestRepo));
    }

    public async Task<DonationRequest> CreateDonationRequestAsync(CreateDonationRequest request)
    {
        return await _donationRequestRepo.CreateDonationRequestAsync(request);
    }

    public async Task<List<DonationRequest>> GetDonationRequestAsync()
    {
        return await _donationRequestRepo.GetDonationRequestAsync();
    }

    public async Task<DonationRequest> GetDonationRequestByIdAsync(Guid requestId)
    {
        return await _donationRequestRepo.GetDonationRequestByIdAsync(requestId);
    }

    public async Task<List<DonationRequest>> GetMyDonationRequestsAsync()
    {
        return await _donationRequestRepo.GetMyDonationRequestsAsync();
    }

    public async Task DeleteDonationRequestAsync(Guid requestId)
    {
        await _donationRequestRepo.DeleteDonationRequestAsync(requestId);
    }

    public async Task<DonationRequest> ConfirmDonationRequestAsync(Guid requestId)
    {
       return await _donationRequestRepo.ConfirmDonationRequestAsync(requestId);
    }


    public async Task<DonationRequest> CompleteDonationRequestAsync(Guid requestId, int amountBlood)
    {
        return await _donationRequestRepo.CompleteDonationRequestAsync(requestId, amountBlood);
    }

    public async Task<DonationRequest> UpdateFailedDonationRequestAsync(Guid requestId, string reason)
    {
        return await _donationRequestRepo.UpdateFailedDonationRequestAsync(requestId, reason);
    }
    public async Task<List<DonationRequest>> GetRequestsByStatusAsync(DonationRequestStatus status)
    {
        return await _donationRequestRepo.GetRequestsByStatusAsync(status);
    }

    public async Task<RequestStatusDto> GetDonationRequestsByStatusAsync()
    {
        return await _donationRequestRepo.GetDonationRequestsByStatusAsync();
    }
}