﻿namespace TechInterviewTask.Entities;

public class Transaction
{
    public int Id { get; set; }
    public int FromUserId { get; set; }
    public int ToUserId { get; set; }
    public decimal Amount { get; set; }
    public required string Currency { get; set; }
    public User? FromUser { get; set; }
    public User? ToUser { get; set; }
}