﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTests
{
    [CollectionDefinition("Api Collection")]
    public class ApiCollectionFixture : ICollectionFixture<ApiTestFixture>
    {
    }
}