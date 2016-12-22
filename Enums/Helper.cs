namespace Wallpaper10CnC
{
    using System;
    using System.ComponentModel;
    using System.Reflection;

    public static class Helper
    {
        public static string GetDescription(this Enum arbitraryEnum)
        {
            if (arbitraryEnum == null)
            {
                return null;
            }

            var attribute = GetAttribute<DescriptionAttribute>(arbitraryEnum);

            return attribute == null ? null : arbitraryEnum.ToString();
        }


        private static T GetAttribute<T>(Enum arbitraryEnum) where T : class
        {
            Type enumType = arbitraryEnum.GetType();
            MemberInfo[] memberInfo = enumType.GetMember(arbitraryEnum.ToString());

            if (memberInfo.Length > 0)
            {
                var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
                if (attributes.Length > 0)
                {
                    return (T)attributes[0];
                }
            }

            return null;
        }
    }
}
