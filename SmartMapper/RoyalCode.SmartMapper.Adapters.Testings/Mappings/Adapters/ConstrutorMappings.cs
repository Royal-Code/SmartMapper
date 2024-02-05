﻿using RoyalCode.SmartMapper.Adapters.Options;
using RoyalCode.SmartMapper.Adapters

namespace RoyalCode.SmartMapper.Adapters.Testings.Mappings.Adapters;

public sealed class ConstrutorMappings
{
    public void Test()
    {
        var options = new AdapterOptions();
        var builder = new AdapterOptionsBuilder<Source, Target>(options);
        var constructor = builder.Constructor();
    }
}
