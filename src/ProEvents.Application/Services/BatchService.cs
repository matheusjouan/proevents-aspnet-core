using AutoMapper;
using Microsoft.Extensions.FileSystemGlobbing;
using ProEvents.Application.DTOs.Batchs;
using ProEvents.Application.Interfaces;
using ProEvents.Core.Entities;
using ProEvents.Core.Exceptions.Batch;
using ProEvents.Core.Interface;
using ProEvents.Core.Models;

namespace ProEvents.Application.Services;
public class BatchService : ObjectResult, IBatchService
{
    private readonly IMapper _mapper;
    private readonly IUnitOfWork _uow;

    public BatchService(IMapper mapper, IUnitOfWork uow)
    {
        _mapper = mapper;
        _uow = uow;
    }

    public async Task<CustomResult> GetAllBatchesByEventIdAsync(long eventId)
    {
        var batches = await _uow.BatchRepository.GetAllBatchesByEventIdAsync(eventId);

        AddResult(_mapper.Map<IEnumerable<BatchDto>>(batches));

        return _customResult;
    }

    public async Task<CustomResult> GetBatchByIdsAsync(long eventId, long id)
    {
        var result = await GetResultBatchByIdAsync(eventId, id);
        Batch batch = (Batch)result.Result;

        AddResult(_mapper.Map<BatchDto>(batch));

        return _customResult;
    }

    public async Task<CustomResult> SaveBatchesAsync(long eventId, SaveBatchDto[] batchesDto)
    {
        var batches = await _uow.BatchRepository.GetAllBatchesByEventIdAsync(eventId);

        foreach(var batchDto in batchesDto)
        {
            batchDto.EventId = eventId;
            if (batchDto.Id == null)
            {
                var batch = _mapper.Map<Batch>(batchDto);
                _uow.BatchRepository.Add(batch);
            }
            else
            {
                var batch =  batches.FirstOrDefault(b => b.Id == batchDto.Id);
                _mapper.Map(batchDto, batch);
                _uow.BatchRepository.Update(batch);
            }

            await _uow.SaveChangesAsync();
        }
        return _customResult;
    }
    public async Task<CustomResult> DeleteBatchAsync(long eventId, long id)
    {
        var result = await GetResultBatchByIdAsync(eventId, id);
        Batch batch = (Batch)result.Result;

        _uow.BatchRepository.Delete(batch);
        await _uow.SaveChangesAsync();

        return _customResult;
    }

    private async Task<CustomResult> GetResultBatchByIdAsync(long eventId, long id)
    {
        var batch = await _uow.BatchRepository.GetBatchByIdsAsync(eventId, id);

        if (batch == null)
        {
            AddError("Batch not exist");
            return _customResult;
        }
            
        AddResult(batch);
        return _customResult;
    }

}
