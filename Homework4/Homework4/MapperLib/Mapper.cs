using System.Reflection;

namespace MapperLib

{
    public class Mapper
    {
        public void MapTo<TStartingObject, TDestinyObject>(TStartingObject startingObject, TDestinyObject destinyObject)
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