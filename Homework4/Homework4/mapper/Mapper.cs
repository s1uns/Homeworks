using System.Reflection;

namespace MapperLib

{
    public class Mapper
    {
        public void MapTo<TStartingObject, TDestinyObject>(TStartingObject startingObject, TDestinyObject destinyObject)
        {   if(startingObject == null)
            {
                throw new Exception($"{nameof(TStartingObject)} is null");
            }

            if (destinyObject == null)
            {
                throw new Exception($"{nameof(TDestinyObject)} is null");
            }

            var startingType = startingObject.GetType();
            var destinyType = destinyObject.GetType();

            var startingTypeProps = startingType.GetProperties(BindingFlags.Public | BindingFlags.Public);
            var destinyTypeProps = destinyType.GetProperties(BindingFlags.Public | BindingFlags.Public);

            foreach ( var startingProp in startingTypeProps )
            {
                var destinyProp = destinyTypeProps.Where(x => x.Name == startingProp.Name).First();

                if ( destinyProp != null )
                {
                    destinyProp.SetValue(destinyObject, startingProp.GetValue(startingObject));
                }
            }
        }
    }
}