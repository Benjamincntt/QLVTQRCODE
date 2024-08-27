using System.Collections.Concurrent;
using System.Reflection;

namespace ESPlatform.QRCode.IMS.Library.Utils.Mappers;

public static class SimpleMapper {
	private static readonly ConcurrentDictionary<string, IEnumerable<PropertyMap>> ObjMapDict = new();

	public static IEnumerable<PropertyInfo> Map(object source, object target) {
		var sourceType = source.GetType();
		var targetType = target.GetType();

		var propMaps = GetMatchingProperties(sourceType, targetType).ToList();

		foreach (var propMap in propMaps) {
			var sourceValue = propMap.SourceProperty.GetValue(source, null);
			propMap.TargetProperty.SetValue(target, sourceValue, null);
		}

		return propMaps.Select(x => x.TargetProperty).ToList();
	}

	private static IEnumerable<PropertyMap> GetMatchingProperties(Type sourceType, Type targetType) {
		var key = $"Map @@ {sourceType.AssemblyQualifiedName} >> {targetType.AssemblyQualifiedName}";
		if (ObjMapDict.TryGetValue(key, out var maps)) {
			return maps;
		}

		var sourceProperties = sourceType.GetProperties().Where(pi => !Attribute.IsDefined(pi, typeof(SimpleMapperIgnore)));
		var targetProperties = targetType.GetProperties().Where(pi => !Attribute.IsDefined(pi, typeof(SimpleMapperIgnore)));

		maps = (from s in sourceProperties
				from t in targetProperties
				where s.CanRead && t.CanWrite && s.Name == t.Name && AreTypeEqual(s.PropertyType, t.PropertyType)
				select new PropertyMap { SourceProperty = s, TargetProperty = t })
			.ToList();

		ObjMapDict.TryAdd(key, maps);

		return maps;
	}

	private static bool AreTypeEqual(Type type1, Type type2) {
		if (type1.IsGenericType && type1.GetGenericTypeDefinition() == typeof(Nullable<>)) {
			type1 = type1.GetGenericArguments()[0];
		}

		if (type2.IsGenericType && type2.GetGenericTypeDefinition() == typeof(Nullable<>)) {
			type2 = type2.GetGenericArguments()[0];
		}

		return type1 == type2;
	}
}

public class PropertyMap {
	public PropertyInfo SourceProperty { get; init; } = null!;

	public PropertyInfo TargetProperty { get; init; } = null!;
}
