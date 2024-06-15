using RoyalCode.SmartMapper.Mapping.Builders;

namespace RoyalCode.SmartMapper.Examples;

public static class ConfigureSample
{
    public static void Configure(IMappingBuilder builder)
    {
        builder.Adapter<MyDto, MyEntity>(b =>
        {
            b.Map(d => d.Id).To(e => e.Id);
        });

        builder.Adapter<MyDto, MyEntity>(b =>
        {
            b.MapToMethod(e => e.DoSomething)
                .Parameters(b2 =>
                {
                    b2.Parameter(e => e.Id);
                });

            b.Map(d => d.ValueObject).ToMethod(e => e.DoSomething);

            b.Constructor().Parameters(b2 =>
            {
                b2.Parameter(e => e.Id);
            });

            b.Constructor().Parameters(p => p.InnerProperties(
                d => d.ValueObject, dp => dp.Parameter(e => e.Value)));
        });
    }

    public class MyEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public MyEntity() { }

        public MyEntity(Guid id)
        {
            Id = id;
        }

        public void DoSomething(string value)
        {
            Console.WriteLine(value);
        }
    }

    public class MyDto
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public SomeValueObject ValueObject { get; set; }
    }

    public class SomeValueObject
    {
        public string Value { get; set; }
    }
}
