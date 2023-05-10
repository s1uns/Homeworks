using System.Reflection;

namespace MapperLib

{
    public static class Mapper
    {
        public static void MapTo<TStartingObject, TDestinyObject>(this TStartingObject startingObject, TDestinyObject destinyObject)
        {
            var startingType = startingObject.GetType();
            var destinyType = destinyObject.GetType();

            var startingTypeProps = startingType.GetProperties(BindingFlags.Instance | BindingFlags.Public);
            var destinyTypeProps = destinyType.GetProperties(BindingFlags.Instance | BindingFlags.Public);

            foreach (var startingProp in startingTypeProps)
            {
                var destinyProp = destinyTypeProps.Where(x => x.Name == startingProp.Name).FirstOrDefault();

                if (destinyProp != null)
                {
                    destinyProp.SetValue(destinyObject, startingProp.GetValue(startingObject));
                }
            }
        }
    }
}