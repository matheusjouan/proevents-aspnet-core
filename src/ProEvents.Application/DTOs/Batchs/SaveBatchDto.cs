﻿namespace ProEvents.Application.DTOs.Batchs;
public class SaveBatchDto
{
    public long? Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public DateTime? StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public int Amount { get; set; }
    public long EventId { get; set; }
}
