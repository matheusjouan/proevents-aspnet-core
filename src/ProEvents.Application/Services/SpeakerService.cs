using AutoMapper;
using ProEvents.Application.DTOs.Speakers;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Entities;
using ProEvents.Core.Interface;
using ProEvents.Core.Models;

namespace ProEvents.Application.Services;
public class SpeakerService : ObjectResult, ISpeakerService
{
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;

    public SpeakerService(IUnitOfWork uow, IMapper mapper)
    {
        _uow = uow;
        _mapper = mapper;
    }

    public async Task<CustomResult> AddSpeakerAsync(long userId, CreateSpeakerDto model)
    {
        var speaker = _mapper.Map<Speaker>(model);
        speaker.UserId = userId;

        _uow.SpeakerRepository.Add(speaker);
        await _uow.SaveChangesAsync();

        return _customResult;
    }

    public async Task<CustomResult> GetAllSpeakersAsync(PageParams pageParams, bool includeEvents = false)
    {
        var speakers = await _uow.SpeakerRepository.GetAllSpeakersAsync(pageParams, includeEvents);

        var result = _mapper.Map<PageList<SpeakerDto>>(speakers);

        result.CurrentPage = speakers.CurrentPage;
        result.TotalCount = speakers.TotalCount;
        result.PageSize = speakers.PageSize;
        result.TotalPages = speakers.TotalPages;

        AddResult(result);
        return _customResult;
    }

    public async Task<CustomResult> GetSpeakerByUserIdAsync(long userId, bool includeEvents = false)
    {
        var speaker = await _uow.SpeakerRepository.GetSpeakeByIdAsync(userId);

        if (speaker == null)
        {
            AddError("Speaker not found");
            return _customResult;
        }
            
        AddResult(_mapper.Map<SpeakerDto>(speaker));
        return _customResult;
    }

    public async Task<CustomResult> UpdateSpeakerAsync(long userId, UpdateSpeakerDto model)
    {
        var speaker = await _uow.SpeakerRepository.GetSpeakeByIdAsync(userId);

        if (speaker == null)
        {
            AddError("Speaker not found");
            return _customResult;
        }
            

        model.Id = speaker.Id;
        model.UserId = userId;

        _mapper.Map(model, speaker);

        _uow.SpeakerRepository.Update(speaker);
        await _uow.SaveChangesAsync();

        return _customResult;
    }
}
