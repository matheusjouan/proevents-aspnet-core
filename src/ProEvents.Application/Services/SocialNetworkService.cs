using AutoMapper;
using Microsoft.Extensions.Logging;
using ProEvents.Application.DTOs.SocialNetworks;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Entities;
using ProEvents.Core.Interface;
using ProEvents.Core.Models;

namespace ProEvents.Application.Services;
public class SocialNetworkService : ObjectResult, ISocialNetworkService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SocialNetworkService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<CustomResult> DeleteSocialNetworkByEventAsync(long id, long eventId)
    {
        var sn = await _uow.SocialNetworkRepository.GetSocialNetworkEventByIdsAsync(eventId, id);

        if (sn == null)
        {
            AddError("SocialNetwork not exist");
            return _customResult;
        }

        _uow.SocialNetworkRepository.Delete(sn);
        await _uow.SaveChangesAsync();

        return _customResult;
    }

    public async Task<CustomResult> DeleteSocialNetworkBySpeakerAsync(long id, long speakerId)
    {
        var sn = await _uow.SocialNetworkRepository.GetSocialNetworkSpeakerByIdsAsync(speakerId, id);

        if (sn == null)
        {
            AddError("SocialNetwork not exist");
            return _customResult;
        }

        _uow.SocialNetworkRepository.Delete(sn);
        await _uow.SaveChangesAsync();

        return _customResult;

    }

    public async Task<CustomResult> GetAllSocialNetworkByEventAsync(long eventId)
    {
        var sns = await _uow.SocialNetworkRepository.GetAllSocialNetworkByEventAsync(eventId);

        AddResult(_mapper.Map<IEnumerable<SocialNetworkDto>>(sns));

        return _customResult;
    }

    public async Task<CustomResult> GetAllSocialNetworkBySpeakerAsync(long speakerId)
    {
        var sns = await _uow.SocialNetworkRepository.GetAllSocialNetworkSpeakersAsync(speakerId);

        AddResult(_mapper.Map<IEnumerable<SocialNetworkDto>>(sns));

        return _customResult;
    }

    public async Task<CustomResult> GetSocialNetworkByEventAsync(long id, long eventId)
    {
        var sn = await _uow.SocialNetworkRepository.GetSocialNetworkEventByIdsAsync(eventId, id);

        if (sn == null)
        {
            AddError("SocialNetwork not exist");
            return _customResult;
        }

        AddResult(_mapper.Map<SocialNetworkDto>(sn));
        return _customResult;
    }

    public async Task<CustomResult> GetSocialNetworkBySpeakerAsync(long id, long speakerId)
    {
        var sn = await _uow.SocialNetworkRepository.GetSocialNetworkSpeakerByIdsAsync(speakerId, id);

        if (sn == null)
        {
            AddError("SocialNetwork not exist");
            return _customResult;
        }

        AddResult(_mapper.Map<SocialNetworkDto>(sn));
        return _customResult;
    }

    public async Task<CustomResult> SaveSocialNetworkByEventAsync(long eventId, IEnumerable<SaveSocialNetworkDto> models)
    {
        var socialNetworks = await _uow.SocialNetworkRepository.GetAllSocialNetworkByEventAsync(eventId);
        
        foreach(var model in models)
        {
            model.EventId = eventId;

            if (model?.Id == null)
            {
                var sn = _mapper.Map<SocialNetwork>(model);
                _uow.SocialNetworkRepository.Add(sn);
            }
            else
            {
                var sn = socialNetworks.FirstOrDefault(s => s.Id == model.Id);
                _mapper.Map(model, sn);
                _uow.SocialNetworkRepository.Update(sn);
            }
            await _uow.SaveChangesAsync();
        }

        return _customResult;
    }

    public async Task<CustomResult> SaveSocialNetworkBySpeakerAsync(long speakerId, IEnumerable<SaveSocialNetworkDto> models)
    {
        var socialNetworks = await _uow.SocialNetworkRepository.GetAllSocialNetworkSpeakersAsync(speakerId);

        foreach (var model in models)
        {
            model.SpeakerId = speakerId;

            if (model?.Id == null)
            {
                var sn = _mapper.Map<SocialNetwork>(model);
                _uow.SocialNetworkRepository.Add(sn);
            }
            else
            {
                var sn = socialNetworks.FirstOrDefault(s => s.Id == model.Id);
                _mapper.Map(model, sn);
                _uow.SocialNetworkRepository.Update(sn);
            }
            await _uow.SaveChangesAsync();
        }

        return _customResult;
    }
}
