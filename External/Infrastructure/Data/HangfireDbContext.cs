﻿using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class HangfireDbContext : DbContext
{
    public HangfireDbContext(DbContextOptions<HangfireDbContext> options) : base(options) { }
}