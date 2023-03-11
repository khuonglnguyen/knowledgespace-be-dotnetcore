using KnowledgeSpace.Backend.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace KnowledgeSpace.Backend.UnitTest
{
    public class InMemoryDbContextFactory
    {
        public ApplicationDBContext GetApplicationDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDBContext>()
                       .UseInMemoryDatabase(databaseName: "InMemoryApplicationDatabase")
                       .Options;
            var dbContext = new ApplicationDBContext(options);

            return dbContext;
        }
    }
}
