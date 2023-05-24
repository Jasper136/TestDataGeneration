using System.Net.Mail;
using System.Reflection;
using AutoBogus;
using Bogus;

namespace TestDataGeneration;

public class Some
{
    static Some()
    {
        _defaultFaker = new Faker();
        _defaultBinder = new DefaultBinder();
    }

    private static readonly Faker _defaultFaker;
    private static DefaultBinder _defaultBinder;

    public static void CustomConfigApplied(DefaultBinder? defaultBinder = null)
    {
        if (defaultBinder != null)
        {
            _defaultBinder = defaultBinder;
        }
    }

    public static AutoFaker<TType> InstanceOf<TType>() where TType : class
    {
        return _defaultBinder.TypeRules.TryGetValue(typeof(TType), out var typeRules)
            ? AutoFakerWithRules<TType>(typeRules, _defaultBinder)
            : new AutoFaker<TType>(_defaultBinder);
    }

    public static object Generated(Type type)
    {
        try
        {
            var randomObjectMethod = typeof(Some).GetMethods()
                .Single(x =>
                    x.Name == nameof(Generated) && x.GetGenericArguments().Any() && !x.GetParameters().Any());
            var typedRandomObjectMethod = randomObjectMethod.MakeGenericMethod(type);
            var randomObject = typedRandomObjectMethod.Invoke(null, null);
            return randomObject!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public static TType Generated<TType>()
    {
        if (typeof(TType).IsValueType)
        {
            return (TType)GeneratedValueType<TType>();
        }

        return GeneratedReferenceType<TType>();
    }

    public static List<TType> Generated<TType>(int count)
    {
        return Enumerable.Range(1, count).Select(_ => Generated<TType>()).ToList();
    }

    public static List<T> Generated<T>(int min, int max) => Generated<T>(Int(min, max));

    public static TType From<TType>(params TType[] possibleValues)
    {
        if (!possibleValues.Any()) throw new ArgumentException("Possible values must be provided.");
        return possibleValues[Int(0, possibleValues.Length - 1)];
    }

    public static TType Except<TType>(params TType[] excludedValues)
    {
        if (!excludedValues.Any()) throw new ArgumentException("Excluded values must be provided.");

        var value = Generated<TType>();
        var tries = 1;
        while (excludedValues.Contains(value))
        {
            if (tries > 9)
                throw new ArgumentException(
                    "Impossible to generate a value, without using the excluded values, within a reasonable amount of tries.");
            value = Generated<TType>();
            tries++;
        }

        return value;
    }

    public static List<TEnum> UniqueValues<TEnum>(int length) where TEnum : Enum
    {
        if (Enum.GetValues(typeof(TEnum)).Length < length)
            throw new ArgumentException("Not enough possible values.");

        var enumValues = new List<TEnum>();
        for (var i = 0; i < length; i++)
        {
            var enumValue = Generated<TEnum>();
            while (enumValues.Contains(enumValue))
            {
                enumValue = Except(enumValues.ToArray());
            }

            enumValues.Add(enumValue);
        }

        return enumValues;
    }

    public static List<TEnum> UniqueValues<TEnum>(int min, int max) where TEnum : Enum =>
        UniqueValues<TEnum>(Int(min, max));

    #region Generation behavior

    public class DefaultBinder : AutoBinder
    {
        protected internal readonly Dictionary<Type, MulticastDelegate> TypeRules = new();

        public override TType CreateInstance<TType>(AutoGenerateContext context)
        {
            if (typeof(TType) == typeof(MailAddress)) return (TType)(object)MailAddress();
            if (typeof(TType) == typeof(TimeZoneInfo)) return (TType)(object)TimeZoneInfo();
            if (TypeRules.TryGetValue(typeof(TType), out var typeRules)) return GeneratedReferenceTypeWithRules<TType>(typeRules);
            return base.CreateInstance<TType>(context);
        }

        public override void PopulateInstance<TType>(object instance, AutoGenerateContext context, IEnumerable<MemberInfo>? members = null)
        {
            if (typeof(TType) == typeof(MailAddress)) return;
            if (typeof(TType) == typeof(TimeZoneInfo)) return;
            if (TypeRules.ContainsKey(typeof(TType))) return;
            base.PopulateInstance<TType>(instance, context, members);
        }
    }

    #endregion

    #region Helpers

    private static TEnum GeneratedEnum<TEnum>(Faker faker)
    {
        var pickRandomMethod = typeof(Faker).GetMethods().Where(x => x.Name == nameof(Faker.PickRandom))
            .Single(x => !x.GetParameters().Any());
        var typedPickRandomMethod = pickRandomMethod.MakeGenericMethod(typeof(TEnum));
        var enumValue = typedPickRandomMethod.Invoke(faker, null);
        return (TEnum)enumValue!;
    }

    private static object GeneratedValueType<TValueType>()
    {
        var faker = new Faker();
        var type = typeof(TValueType);

        var underlyingType = Nullable.GetUnderlyingType(type);
        if (underlyingType != null)
        {
            var methodInfo = typeof(Some)
                .GetMethod(nameof(GeneratedValueType), BindingFlags.NonPublic | BindingFlags.Static);
            return methodInfo
                .MakeGenericMethod(underlyingType).Invoke(null, null)!;
        }
        if (type.IsEnum)
        {
            return GeneratedEnum<TValueType>(faker)!;
        }

        return AutoFaker.Generate<TValueType>()!;
    }

    private static TReferenceType GeneratedReferenceType<TReferenceType>()
    {
        try
        {
            var genericRandomImplMethod = typeof(Some).GetMethod(nameof(ConstrainedGeneratedReferenceType), BindingFlags.NonPublic | BindingFlags.Static);
            var typedRandomImplMethod = genericRandomImplMethod.MakeGenericMethod(typeof(TReferenceType));
            var customRandomObject = typedRandomImplMethod.Invoke(null, null);
            return (TReferenceType)customRandomObject!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static T ConstrainedGeneratedReferenceType<T>() where T : class
    {
        try
        {
            var customFaker = InstanceOf<T>();
            var generateMethods = customFaker.GetType().GetMethods()
                .Where(y => y.Name == nameof(AutoFaker<T>.Generate));
            var generateMethod = generateMethods.Single(y => y.GetParameters().Length == 1);
            var customRandomObject = generateMethod.Invoke(customFaker, new object?[] { null });
            return (T)customRandomObject!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static TReferenceType GeneratedReferenceTypeWithRules<TReferenceType>(MulticastDelegate typeRules)
    {
        try
        {
            var genericRandomImplMethod = typeof(Some).GetMethod(nameof(ConstrainedGeneratedReferenceTypeWithRules), BindingFlags.NonPublic | BindingFlags.Static);
            var typedRandomImplMethod = genericRandomImplMethod.MakeGenericMethod(typeof(TReferenceType));
            var customRandomObject = typedRandomImplMethod.Invoke(null, new object[] { typeRules });
            return (TReferenceType)customRandomObject!;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static T ConstrainedGeneratedReferenceTypeWithRules<T>(MulticastDelegate typeRules) where T : class
    {
        return AutoFakerWithRules<T>(typeRules).Generate();
    }

    //Do not pass binder when invoked from binder to avoid StackOverflowExceptions
    private static AutoFaker<TType> AutoFakerWithRules<TType>(MulticastDelegate typeRules, IAutoBinder binder = null) where TType : class
    {
        var autoFaker = binder != null ? new AutoFaker<TType>(binder) : new AutoFaker<TType>();
        return (AutoFaker<TType>)((Func<Faker<TType>, Faker<TType>>)typeRules)(autoFaker);
    }

    #endregion

    #region System types

    /// <summary>
    /// String of specified length.
    /// When length not specified or specified length is smaller than or equal to 0, length is random between 40 and 80.
    /// </summary>
    /// <param name="length">Length of generated string</param>
    /// <returns></returns>
    public static string String(int length = 0) => length <= 0 ? String(40, 80) : new Faker().Random.AlphaNumeric(length);
    public static string String(int minLength, int maxLength) => String(Int(minLength, maxLength));

    public static MailAddress MailAddress() =>
        new Faker<MailAddress>()
            .CustomInstantiator(f => new MailAddress($"{f.Internet.Email()}"))
            .Generate();

    public static TimeZoneInfo TimeZoneInfo() =>
        new Faker().PickRandomParam(System.TimeZoneInfo.GetSystemTimeZones().ToArray());

    public static int Int(int min = int.MinValue, int max = int.MaxValue) =>
        _defaultFaker.Random.Int(min, max);

    public static bool Bool() => new Faker().Random.Bool();
    public static Guid Guid() => new Faker().Random.Guid();

    public static DateTime DateTime()
    {
        var faker = new Faker();
        return faker.Random.Bool() ? faker.Date.Past() : faker.Date.Future();
    }
    public static DateTime DateTimeAfter(DateTime refDate) => new Faker().Date.Future(refDate: refDate);
    public static DateTime DateTimeBefore(DateTime refDate) => new Faker().Date.Past(refDate: refDate);
    public static DateTime DateTimeBetween(DateTime start, DateTime end) => new Faker().Date.Between(start, end);

    #endregion

}
